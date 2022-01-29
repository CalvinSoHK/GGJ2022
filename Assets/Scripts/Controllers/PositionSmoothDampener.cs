using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Smooth damps this object to target position and rotation on request
/// </summary>
public class PositionSmoothDampener : MonoBehaviour
{
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

    /// <summary>
    /// Breaks existing coroutines when set to true.
    /// </summary>
    private bool breakCoroutine = false;

    /// <summary>
    /// Begins smoothing this object to target position
    /// </summary>
    public void SmoothToPosition()
    {
        StartCoroutine(SmoothToPositionCoroutine());
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
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref refPosVelocity, positionSmoothTime);

            float delta = Quaternion.Angle(transform.rotation, targetRot);
            if (delta > 0f)
            {
                float t = Mathf.SmoothDampAngle(delta, 0.0f, ref refRotVelocity, rotationSmoothTime);
                t = 1.0f - (t / delta);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, t);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
