using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates object to always face camera
/// </summary>
public class RotateTowardsObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    private void Start()
    {
        if(targetTransform == null)
        {
            Debug.LogError("Target transform not assigned for RotateTowardsObject on : " + gameObject.name + ". Removing.");
            GetComponent<RotateTowardsObject>().enabled = false;
        }
    }

    private void Update()
    {
        transform.LookAt(targetTransform);
    }
}
