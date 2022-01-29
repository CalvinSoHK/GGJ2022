using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

[RequireComponent(typeof(Outline))]
public class SetOutlineColor : MonoBehaviour
{
    [SerializeField]
    ColorScriptableObject color;

    private void Awake()
    {
        GetComponent<Outline>().OutlineColor = color.SavedColor;
    }
}
