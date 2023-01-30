using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThoughtsSlot : CommonImageDragSlot
{
    private LevelFirstDebate parent;

    public void Init(LevelFirstDebate parent)
    {
        this.parent = parent;
    }

    //character wares the dress
    public override void DropDeal(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            //
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            parent.DragGoalFinish();
        }
        Debug.Log("OnDrop");
    }
}
