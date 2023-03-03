using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DragHurtBasic : MonoBehaviour
{
    public Transform tfDraw;
    public SpriteRenderer srItem;
    public BoxCollider2D colHit;

    protected float dragInitStartPosX;
    protected float dragInitStartPosY;
    protected float dragStartPosX;
    protected float dragStartPosY;

    public bool isBeingHeld = false;
    public bool canDrag = false;
    protected bool isInit = false;

    public void Init()
    {
        dragInitStartPosX = this.transform.position.x;
        dragInitStartPosY = this.transform.position.y;
        isInit = true;
        canDrag = true;
    }

    private void Update()
    {
        if (isInit)
        {
            CheckWhetherDrag();
            CheckDrag();
        }
    }

    protected virtual void CheckDrag()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos = PublicTool.GetMousePosition2D();
            this.gameObject.transform.position = new Vector3(mousePos.x - dragStartPosX, mousePos.y - dragStartPosY, 0);
        }
    }


    public void CheckWhetherDrag()
    {
        if (Input.GetMouseButtonDown(0)&& canDrag)
        {
            Vector3 mousePosNew = Input.mousePosition;
            mousePosNew.z = 10;
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePosNew);
            RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

            if(hit.collider != null)
            {
                if (hit.collider.tag == "CanDrag")
                {
                    Vector3 mousePos = PublicTool.GetMousePosition2D();
                    dragStartPosX = mousePos.x - this.transform.position.x;
                    dragStartPosY = mousePos.y - this.transform.position.y;
                    isBeingHeld = true;
                    return;
                }
            }


        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isBeingHeld)
            {
                isBeingHeld = false;
                MoveBack();
            }
        }
    }


    /*    public void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0) && canDrag)
            {
                Vector3 mousePos = PublicTool.GetMousePosition2D();
                dragStartPosX = mousePos.x - this.transform.position.x;
                dragStartPosY = mousePos.y - this.transform.position.y;

                if (!isBeingHeld)
                {
                    isBeingHeld = true;
                }
            }
        }

        public void OnMouseUp()
        {
            if (isBeingHeld)
            {
                isBeingHeld = false;
                MoveBack();
            }
        }*/

    public void MoveBack()
    {
        this.transform.DOMove(new Vector2(dragInitStartPosX, dragInitStartPosY), 0.5f);
    }

    #region View

    public void Hide(float time)
    {
        srItem.DOFade(0, time);
    }
    #endregion
}
