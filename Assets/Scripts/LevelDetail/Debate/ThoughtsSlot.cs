using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThoughtsSlot : CommonImageDragSlot
{
    private LevelBasic parent;

    public void Init(LevelBasic parent)
    {
        this.parent = parent;
    }

    //character wares the dress
    public override void DropDeal(PointerEventData eventData)
    {
        parent.DragFinishCheck();
    }
}
