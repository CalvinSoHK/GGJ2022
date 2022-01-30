using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image targetImg;

    [SerializeField]
    private TextMeshProUGUI targetText;

    [SerializeField]
    private Sprite defaultSprite;

    [SerializeField]
    private Sprite hoverSprite;

    [SerializeField]
    private ColorScriptableObject defaultTextColor;

    [SerializeField]
    private ColorScriptableObject hoverTextColor;

    void Start()
    {
        if(targetText != null)
        {
            targetText.faceColor = defaultTextColor.SavedColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetImg.sprite = hoverSprite;
        if(targetText != null)
        {
            targetText.faceColor = hoverTextColor.SavedColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetSprite();
    }

    public void ResetSprite()
    {
        targetImg.sprite = defaultSprite;
        if (targetText != null)
        {
            targetText.faceColor = defaultTextColor.SavedColor;
        }
    }
}
