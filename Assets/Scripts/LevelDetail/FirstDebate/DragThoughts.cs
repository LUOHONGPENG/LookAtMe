using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    private CanvasGroup canvasgroup;
    private void Awake()
    {
        canvasgroup = GetComponent<CanvasGroup>();
    }

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
        canvasgroup.blocksRaycasts = false;
        canvasgroup.alpha = 0.2f;
        //Debug.Log("onBeginDrag");

        parent.SetCurrentDragging(thoughtType);
    }

    public override void DragDeal(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public override void EndDragDeal(PointerEventData eventData)
    {
        canvasgroup.blocksRaycasts = true;
        canvasgroup.alpha = 1f;

        parent.ReleaseDragging();
    }

    public override void DropDeal(PointerEventData eventData)
    {
        
    }


    #endregion



}
