using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Smooth damps this object to target position and rotation on request
/// </summary>
public class PositionSmoothDampener : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    /// <summary>
    /// Target this will be manipulating
    /// </summary>
    public Transform Target
    {
        get
        {
            return targetTransform;
        }
    }

    [SerializeField]
    private Vector3 targetPos = Vector3.zero;
    [SerializeField]
    private Quaternion targetRot = Quaternion.identity;

    private Vector3 refPosVelocity = Vector3.zero;
    private float refRotVelocity = 0f;

    [SerializeField]
    private float positionSmoothTime = 0.2f;

    [SerializeField]
    private float rotationSmoothTime = 0.2f;

    [SerializeField]
    private UnityEvent OnFinishEvent = new UnityEvent();

    /// <summary>
    /// Breaks existing coroutines when set to true.
    /// </summary>
    private bool breakCoroutine = false;

    /// <summary>
    /// Begins smoothing this object to target position
    /// </summary>
    public void SmoothToPosition()
    {
        if(targetTransform == null)
        {
            targetTransform = transform;
        }

        StartCoroutine(SmoothToPositionCoroutine());
    }

    /// <summary>
    /// Sets new target pos and rot at runtime
    /// </summary>
    /// <param name="_targetPos"></param>
    /// <param name="_targetRot"></param>
    public void SetTargetPositionAndRot(Vector3 _targetPos, Quaternion _targetRot)
    {
        targetPos = _targetPos;
        targetRot = _targetRot;
    }

    /// <summary>
    /// Sets our pos and rotation smooth time for smooth damp at runtime
    /// </summary>
    /// <param name="_posTime"></param>
    /// <param name="_rotTime"></param>
    public void SetPosAndRotSmoothTime(float _posTime, float _rotTime)
    {
        positionSmoothTime = _posTime;
        rotationSmoothTime = _rotTime;
    }

    /// <summary>
    /// Smooths to target position
    /// </summary>
    /// <returns></returns>
    private IEnumerator SmoothToPositionCoroutine()
    {
        //Break out of other existing coroutine
        breakCoroutine = true;
        yield return new WaitForEndOfFrame();
        breakCoroutine = false;

        //Move to target position until broken or we reach the position
        while (!breakCoroutine)
        {
            targetTransform.position = Vector3.SmoothDamp(targetTransform.position, targetPos, ref refPosVelocity, positionSmoothTime);

            float delta = Quaternion.Angle(targetTransform.rotation, targetRot);
            if (delta > 0f)
            {
                float t = Mathf.SmoothDampAngle(delta, 0.0f, ref refRotVelocity, rotationSmoothTime);
                t = 1.0f - (t / delta);
                targetTransform.rotation = Quaternion.Slerp(targetTransform.rotation, targetRot, t);
            }

            if(Vector3.Distance(targetTransform.position, targetPos) <= 0.01f && delta < 0.1f)
            {
                OnFinishEvent?.Invoke();
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        //Set to position so we are at pos and rot in case of small differences
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }
}
