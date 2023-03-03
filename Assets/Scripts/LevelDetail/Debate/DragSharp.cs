using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSharp : CommonImageDrag
{
    public Transform tfParent;
    private LevelSecondDebate parent;

    public void Init(Transform tfParent,LevelSecondDebate parent)
    {
        this.parent = parent;
        this.tfParent = tfParent;
        this.transform.localScale = Vector2.zero;
    }

    public override void DragDeal(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition += eventData.delta;
        if (parent != null && parent.canDragSharp)
        {
            tfParent.position = PublicTool.GetMousePosition2D();
        }
    }
}
