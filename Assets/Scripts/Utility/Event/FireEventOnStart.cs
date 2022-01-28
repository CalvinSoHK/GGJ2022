using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Lets us fire an event on start.
/// </summary>
public class FireEventOnStart : MonoBehaviour
{
    [SerializeField]
    UnityEvent EventToFire;

    private void Start()
    {
        EventToFire?.Invoke();
    }
}
