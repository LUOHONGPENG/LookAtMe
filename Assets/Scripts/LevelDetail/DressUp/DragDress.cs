using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DragDress : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,IDropHandler
{

    private LevelDressUp parent;
    public DressType dresstype;//send dragging type
    public Image imgDress;
    public RectTransform rtDress;

    public void Init(DressType type,LevelDressUp parent)
    {
        this.parent = parent;
       // this.dresstype = type;

    }

    //drag the dresses
    public void OnBeginDrag(PointerEventData eventData) {
       /*
        if(gameObject.name == "clothes1")
        {
            dresstype = DressType.dress1;
        }else if (gameObject.name == "clothes2") {
            dresstype = DressType.dress2;
        }
        else { }
        */
        
        imgDress.raycastTarget = false;
        imgDress.DOFade(0.8f, 0);
        parent.SetCurrentDress(dresstype);
      

    }
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("onDrag");
        rtDress.anchoredPosition += eventData.delta;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        imgDress.raycastTarget = true;
        imgDress.DOFade(0f, 0);


    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("onPointerDone");
    }
    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException(); //put dress on character
    }

    


}
