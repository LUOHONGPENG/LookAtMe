using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragClock : CommonImageDrag
{
    public Image imgPointer;
    public Transform tfPointer;
    private LevelClock parent;
    private bool isInit = false;

    public void Init(LevelClock parent)
    {
        this.parent = parent; 
        isInit = true;
    }

    public override void DragDeal(PointerEventData eventData)
    {
        base.DragDeal(eventData);
    }


    public void SetRotation()
    {

    }
}
