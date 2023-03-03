using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DragSharp : CommonImageDrag
{
    public Transform tfParent;
    public Rigidbody2D thisBody;
    private LevelSecondDebate parent;
    private bool isDragging = false;

    public void Init(Transform tfParent,LevelSecondDebate parent)
    {
        this.parent = parent;
        this.tfParent = tfParent;
        thisBody = tfParent.GetComponent<Rigidbody2D>();
        this.transform.localScale = Vector2.zero;

        isDragging = false;
    }

    public void FixedUpdate()
    {
        if (isDragging)
        {
            if (parent != null && parent.canDragSharp && thisBody != null)
            {
                Vector2 vectorSpeed = PublicTool.GetMousePosition2D() - new Vector2(this.transform.position.x, this.transform.position.y);
                thisBody.velocity = vectorSpeed * 10f;

                //tfParent.DOMove(PublicTool.GetMousePosition2D(),0.2F);
            }
        }
        else
        {
            thisBody.velocity = Vector2.zero;
        }
    }

    public override void BeginDragDeal(PointerEventData eventData)
    {
        base.BeginDragDeal(eventData);
        isDragging = true;
    }

    public override void DragDeal(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition += eventData.delta;

    }

    public override void EndDragDeal(PointerEventData eventData)
    {
        base.EndDragDeal(eventData);

        isDragging = false;

        if (parent != null && parent.canDragSharp && thisBody != null)
        {
            thisBody.velocity = Vector2.zero;

            //tfParent.DOMove(PublicTool.GetMousePosition2D(),0.2F);
        }
    }
}
