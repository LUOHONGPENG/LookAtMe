using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DragThoughts : CommonImageDrag
{
    public RectTransform rectTransform;
    public Image imgContent;
    //数据：记住这是个什么图形
    [HideInInspector]
    public ThoughtType thoughtType;
    public List<Sprite> listSpThought;

    private Vector2 posStart;
    private LevelFirstDebate parent;


    public void Init(ThoughtType type, LevelFirstDebate parent)
    {
        this.parent = parent;
        this.thoughtType = type;
        this.posStart = this.transform.position;

        switch (type)
        {
            case ThoughtType.Square:
                imgContent.sprite = listSpThought[0];
                break;
            case ThoughtType.Circle:
                imgContent.sprite = listSpThought[1];
                break;
            case ThoughtType.Triangle:
                imgContent.sprite = listSpThought[2];
                break;
        }
    }


    #region Drag
    //drag the dresses
    public override void BeginDragDeal(PointerEventData eventData)
    {
        imgContent.raycastTarget = false;
        imgContent.DOFade(0.2f, 0);
        //Remember the start position
        this.posStart = this.transform.position;

        parent.SetCurrentDragging(thoughtType);
    }

    public override void DragDeal(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public override void EndDragDeal(PointerEventData eventData)
    {
        imgContent.raycastTarget = true;
        imgContent.DOFade(1f, 0);
        this.transform.DOMove(posStart, 0.5f);

        parent.ReleaseDragging();
    }

    #endregion



}
