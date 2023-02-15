using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommonImageDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    protected float dragStartPosX;
    protected float dragStartPosY;
    #region DragFunction
    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDragDeal(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragDeal(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        DropDeal(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragDeal(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
    #endregion

    #region AddFunction
    //Will Invoke when the player begin to drag
    public virtual void BeginDragDeal(PointerEventData eventData)
    {
        Vector3 mousePos = PublicTool.GetMousePosition2D();
        dragStartPosX = mousePos.x - this.transform.position.x;
        dragStartPosY = mousePos.y - this.transform.position.y;
    }

    //Will Invoke when the player is dragging
    public virtual void DragDeal(PointerEventData eventData)
    {

    }

    //Will Invoke when the player end dragging
    public virtual void EndDragDeal(PointerEventData eventData)
    {

    }

    public virtual void DropDeal(PointerEventData eventData)
    {

    }
    #endregion
}
