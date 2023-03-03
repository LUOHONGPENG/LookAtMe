using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class CommonHoverUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Transform tfTarget;
    public bool isEnabled = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isEnabled)
        {
            tfTarget.localScale = new Vector2(1.2f, 1.2f);
        }
        else
        {
            tfTarget.localScale = Vector2.one;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tfTarget.localScale = Vector2.one;
    }

    public void ResetOne()
    {
        tfTarget.localScale = Vector2.one;
    }
}
