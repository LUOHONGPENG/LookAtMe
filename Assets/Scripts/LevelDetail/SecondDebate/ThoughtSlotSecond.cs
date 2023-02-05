using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThoughtSlotSecond : CommonImageDragSlot
{
    private LevelSecondDebate parent;


    public void Init(LevelSecondDebate parent)
    {
        this.parent = parent;   
    }

    public override void DropDeal(PointerEventData eventData)
    {
        parent.DragGoalFinish();

    }
}
