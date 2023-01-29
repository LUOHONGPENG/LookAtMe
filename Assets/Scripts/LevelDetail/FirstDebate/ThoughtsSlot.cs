using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThoughtsSlot : MonoBehaviour, IDropHandler
{
    public bool isDressed = false;

    //character wares the dress
    public void OnDrop(PointerEventData eventData)

    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            isDressed = true;
        }
        Debug.Log("OnDrop");
    }
}
