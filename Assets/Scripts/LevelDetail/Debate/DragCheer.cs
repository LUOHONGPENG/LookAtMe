using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCheer : CommonImageDrag
{
    private LevelFirstDebate parent;
    public Collider2D triggerCheer;
    public RectTransform rectTransform;
    public bool canDrag = false;
    private bool isInit = false;

    public void Init(LevelFirstDebate parent)
    {
        this.parent = parent;
        canDrag = true;
        isInit = true;
    }

    private void Update()
    {
        if (isInit)
        {
            CheckCheer();
        }
    }

    public override void DragDeal(PointerEventData eventData)
    {
        base.DragDeal(eventData);

        if (canDrag)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

    public void CheckCheer()
    {
        if (parent.currentRound == LevelFirstDebate.LevelRound.Cheers && !parent.isCheer)
        {
            ContactFilter2D filter = new ContactFilter2D().NoFilter();
            List<Collider2D> results = new List<Collider2D>();
            triggerCheer.OverlapCollider(filter, results);
            foreach (BoxCollider2D col in results)
            {
                if (col.tag == "ColDetect")
                {
                    parent.isCheer = true;
                    StartCoroutine(parent.IE_CheerGoalFinish());
                }
            }
        }
    }
}
