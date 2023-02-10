using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DressSlot : CommonImageDragSlot
{
    private LevelDressUp parent;

    public void Init(LevelDressUp parent)
    {
        this.parent = parent;
    }

    //character wares the dress
    public override void DropDeal(PointerEventData eventData)
    {
        StartCoroutine(parent.IE_DragGoalFinish());
    }
}
