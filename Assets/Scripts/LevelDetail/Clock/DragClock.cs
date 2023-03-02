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

    private float currentAngle;
    private float calculateAngle;
    private int countRound = 0;

    public void Init(LevelClock parent)
    {
        this.parent = parent;
        tfPointer = parent.tfPointer;
        isInit = true;

        currentAngle = 0;
        SetRotation(currentAngle);

    }

    public override void DragDeal(PointerEventData eventData)
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

        Vector2 vecPoint = PublicTool.GetMousePosition2D() - new Vector2(tfPointer.position.x,tfPointer.position.y);
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
            Debug.Log(calculateAngle);
        }

    }


    public void SetRotation(float angle)
    {
        imgPointer.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public float ConvertAngleToRate()
    {
        if (isInit)
        {
            return 0.1f * Mathf.Abs(calculateAngle)/360f;
        }
        else
        {
            return 0;
        }
    }
}
