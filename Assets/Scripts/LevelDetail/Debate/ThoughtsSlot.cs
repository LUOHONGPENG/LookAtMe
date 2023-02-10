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
        if (parent.currentRound != LevelFirstDebate.LevelRound.Cheers)
        {
            StartCoroutine(parent.IE_DragGoalFinish());
        }
    }
}
