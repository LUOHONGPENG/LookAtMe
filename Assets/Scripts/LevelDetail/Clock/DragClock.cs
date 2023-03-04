using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragClock : CommonImageDrag
{
    public Image imgPointer;
    private Transform tfPointer;
    private LevelClock parent;
    private bool isInit = false;

    private float LastMouseAngle;
    private float LastClockAngle;
    //private float currentMouseAngle;
    private float currentClockAngle;

    private int countRound = 0;

    public void Init(LevelClock parent)
    {
        this.parent = parent;
        tfPointer = parent.tfPointer;
        isInit = true;

        currentClockAngle = 0;
        SetRotation(currentClockAngle);
    }


    public override void BeginDragDeal(PointerEventData eventData)
    {
        base.BeginDragDeal(eventData);
        if (!isInit)
        {
            return;
        }
        LastMouseAngle = GetAngleMouse();
        LastClockAngle = currentClockAngle;
    }

    public override void DragDeal(PointerEventData eventData)
    {
        base.DragDeal(eventData);
        if (!isInit)
        {
            return;
        }
        float currentMouseAngle = GetAngleMouse();
        float angleDiff = currentMouseAngle - LastMouseAngle;
        if (currentMouseAngle > LastMouseAngle + 180f)
        {
            //countRound++;
            angleDiff -= 360f;
        }

        if (angleDiff < 0 && Mathf.Abs(angleDiff)<30f && ConvertAngleToRate()<1f)
        {
            currentClockAngle = LastClockAngle + angleDiff;
            SetRotation(currentClockAngle);
            LastClockAngle = currentClockAngle;
        }
        LastMouseAngle = currentMouseAngle;

/*        //CalculateDiff
        float angleDiff = currentMouseAngle - LastMouseAngle;
        if (angleDiffLastFrame < 0 && angleDiff > 0)
        {
            countRound++;
        }
        angleDiffLastFrame = angleDiff;

        currentClockAngle = initClockAngle + (angleDiff - 360f * countRound);
        
        SetRotation(currentClockAngle);
        calculateAngle = currentClockAngle;

        Debug.Log(currentClockAngle);*/
    }


    public float GetAngleMouse()
    {
        Vector2 vecPoint = PublicTool.GetMousePosition2D() - new Vector2(tfPointer.position.x, tfPointer.position.y);
        Vector2 vecInit = new Vector2(0, 1);
        float angleMouse = PublicTool.CalculateAngle(vecPoint, vecInit);
        return angleMouse;
    }


    /*    public override void DragDeal(PointerEventData eventData)
        {
            base.DragDeal(eventData);

            if (!isInit)
            {
                return;
            }

            if (!parent.canDrag)
            {
                return;
            }
            //Vector_MouseToCenter
            Vector2 vecPoint = PublicTool.GetMousePosition2D() - new Vector2(tfPointer.position.x,tfPointer.position.y);
            //Vector_Up
            Vector2 vecInit = new Vector2(0, 1);

            Vector2 vecCurrent = new Vector2(-Mathf.Sin(currentAngle * Mathf.Deg2Rad), Mathf.Cos(currentAngle * Mathf.Deg2Rad));

            float angleDiff = PublicTool.CalculateAngle(vecPoint, vecCurrent);
            float angleMouse = PublicTool.CalculateAngle(vecPoint, vecInit);

            if (angleDiff < 0 && Mathf.Abs(angleDiff)<30f)
            {
                if (angleMouse > 0 && currentAngle < 0)
                {
                    countRound++;
                }
                currentAngle = angleMouse;
                calculateAngle = currentAngle - 360f * countRound;
                SetRotation(currentAngle);
                //Debug.Log(calculateAngle);
            }
        }*/


    public void SetRotation(float angle)
    {
        imgPointer.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public float ConvertAngleToRate()
    {
        if (isInit)
        {
            return GameGlobal.rateClock_oneRound * Mathf.Abs(currentClockAngle) /360f;
        }
        else
        {
            return 0;
        }
    }
}
