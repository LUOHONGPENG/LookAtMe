using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCheer : CommonImageDrag
{
    public RectTransform rectTransform;
    public bool canDrag = false;

    public void Init()
    {
        canDrag = true;
    }

    public override void DragDeal(PointerEventData eventData)
    {
        base.DragDeal(eventData);

        if (canDrag)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

}
