using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates object to always face camera
/// </summary>
public class RotateTowardsCamera : MonoBehaviour
{
    private Camera targetCamera;

    private void Start()
    {
        targetCamera = Singleton.Instance.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        transform.LookAt(targetCamera.transform);
    }
}
