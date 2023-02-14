using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragLipstick : MonoBehaviour
{
    public Transform tfDraw;
    public SpriteRenderer srLipstick;

    private float dragInitStartPosX;
    private float dragInitStartPosY;
    private float dragStartPosX;
    private float dragStartPosY;
    public bool isBeingHeld = false;
    private bool isInit = false;
    public bool canDrag = false;
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
            CheckDrag();
        }
    }

    private void CheckDrag()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos = PublicTool.GetMousePosition2D();
            this.gameObject.transform.position = new Vector3(mousePos.x - dragStartPosX, mousePos.y - dragStartPosY, 0);
        }
    }

    public void OnMouseDown()
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
    }

    public void MoveBack()
    {
        this.transform.DOMove(new Vector2(dragInitStartPosX, dragInitStartPosY), 0.5f);
    }

    #region View

    public void Hide(float time)
    {
        srLipstick.DOFade(0, time);
    }
    #endregion

}
