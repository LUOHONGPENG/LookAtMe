using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DragDress : CommonImageDrag
{
    public RectTransform rtDress;

    [HideInInspector]
    public DressType dressType;//send dragging type
    public Image imgDress;
    public List<Sprite> listSpDress = new List<Sprite>();

    private Vector2 posStart;
    private LevelDressUp parent;


    public void Init(DressType type,LevelDressUp parent)
    {
        this.parent = parent;
        this.dressType = type;
        //Remember the start position
        this.posStart = this.transform.position;
        switch (type)
        {
            case DressType.Duck:
                imgDress.sprite = listSpDress[0];
                break;
            case DressType.Flower:
                imgDress.sprite = listSpDress[1];
                break;
        }
        imgDress.SetNativeSize();
    }

    //Important: This should be called before Init()
    public void InitPosition(Vector2 pos)
    {
        rtDress.anchoredPosition = pos;
    }

    //Will be call after dragged into character
    public void SuddenHide()
    {
        imgDress.gameObject.SetActive(false);
    }


    #region Drag
    //drag the dresses
    public override void BeginDragDeal(PointerEventData eventData)
    {
        imgDress.raycastTarget = false;
        imgDress.DOFade(0.2f, 0);
        //Remember the start position
        this.posStart = this.transform.position;

        parent.SetCurrentDragging(dressType);
    }

    public override void DragDeal(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition += eventData.delta;
        this.transform.position = PublicTool.GetMousePosition2D();
    }

    public override void EndDragDeal(PointerEventData eventData)
    {
        imgDress.raycastTarget = true;
        imgDress.DOFade(1f, 0);
        this.transform.DOMove(posStart, 0.5f);

        parent.ReleaseDragging();
    }

    #endregion





}
