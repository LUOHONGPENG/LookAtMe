using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDress : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,IDropHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasgroup;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasgroup = GetComponent<CanvasGroup>();

    }
    //drag the dresses
    public void OnBeginDrag(PointerEventData eventData) {
        canvasgroup.blocksRaycasts = false;
        canvasgroup.alpha = 0.2f;
        //Debug.Log("onBeginDrag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("onDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasgroup.blocksRaycasts = true;
        canvasgroup.alpha = 1f;
        //Debug.Log("onEndDrag");
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
