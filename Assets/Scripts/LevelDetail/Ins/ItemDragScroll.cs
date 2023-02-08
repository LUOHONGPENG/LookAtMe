using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragScroll : CommonImageDrag
{
    public bool isEndDrag;

    public bool isDrag;

    public override void DragDeal(PointerEventData eventData)
    {
        isDrag = true;
        isEndDrag = false;
    }

    public override void EndDragDeal(PointerEventData eventData)
    {
        isDrag = false;
        isEndDrag = true;
    }
}
