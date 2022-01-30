using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownCharacter : MonoBehaviour
{
    [SerializeField]
    private PositionSmoothDampener posDampener;

    [Tooltip("How far away to drop down from.")]
    [SerializeField]
    private float heightDrop = 20f;

    [Range(0f, 10f)]
    [SerializeField]
    private float dropSmoothTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //If posDampener is null grab it
        if (posDampener == null)
        {
            posDampener = GetComponent<PositionSmoothDampener>();
            if (posDampener == null)
            {
                Debug.LogError("DropdownCharacter Error: No assigned PositionSmoothDampener and not attached to an object with PositionSmoothDampener.");
            }
        }

        //Save our position to dampener
        posDampener.SetTargetPositionAndRot(posDampener.Target.position, posDampener.Target.rotation);

        //Set our springSmooth time to both Pos and Rot smooth time
        posDampener.SetPosAndRotSmoothTime(dropSmoothTime, dropSmoothTime);

        //Push the object into the sky
        posDampener.Target.transform.position += Vector3.up * heightDrop;
    }
}
