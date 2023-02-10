using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DragDress : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,IDropHandler
{
    public Image imgDress;
    public RectTransform rtDress;
    //drag the dresses
    public void OnBeginDrag(PointerEventData eventData) {
        imgDress.raycastTarget = false;
        imgDress.DOFade(0.2f, 0);
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("onDrag");
        rtDress.anchoredPosition += eventData.delta;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        imgDress.raycastTarget = true;
        imgDress.DOFade(1f, 0);
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
