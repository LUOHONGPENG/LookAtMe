using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DragHosEye : CommonImageDrag
{
    public enum EyeType
    {
        Top,
        Bottom
    }

    public EyeType eyeType;
    public RectTransform rtThis;
    [HideInInspector]
    public bool isOpenEye = false;
    private float lastFrameMousePosY;

    public float absPosY
    {
        get
        {
            return Mathf.Abs(rtThis.anchoredPosition.y);
        }
    }

    public override void BeginDragDeal(PointerEventData eventData)
    {
        base.BeginDragDeal(eventData);
        lastFrameMousePosY = PublicTool.GetMousePosition2D().y;
    }

    public override void DragDeal(PointerEventData eventData)
    {
        base.DragDeal(eventData);

        if (isOpenEye)
        {
            return;
        }


        Vector2 mousePosition = PublicTool.GetMousePosition2D();
        float deltaY = (mousePosition.y - lastFrameMousePosY);
        lastFrameMousePosY = mousePosition.y;
        Debug.Log(deltaY);
        float targetY = this.transform.position.y + deltaY * GameGlobal.rateHos_dragSpeed;
        if (eyeType == EyeType.Top && this.transform.position.y < targetY)
        {
            this.transform.position = new Vector2(0, targetY);
        }
        else if(eyeType == EyeType.Bottom && this.transform.position.y > targetY)
        {
            this.transform.position = new Vector2(0, targetY);
        }
    }

    private void Update()
    {
        if(eyeType == EyeType.Top)
        {
            if (!isOpenEye)
            {
                rtThis.anchoredPosition = new Vector2(0, absPosY - GameGlobal.rateHos_eyeBack * Time.deltaTime);
            }

            if (rtThis.anchoredPosition.y < 0)
            {
                rtThis.anchoredPosition = new Vector2(0, 0);
            }
        }
        else if(eyeType == EyeType.Bottom)
        {
            if (!isOpenEye)
            {
                rtThis.anchoredPosition = new Vector2(0, -absPosY + GameGlobal.rateHos_eyeBack * Time.deltaTime);
            }

            if (rtThis.anchoredPosition.y > 0)
            {
                rtThis.anchoredPosition = new Vector2(0, 0);
            }
        }
    }

    public void OpenEye()
    {
        isOpenEye = true;
        switch (eyeType)
        {
            case EyeType.Top:
                DOTween.To(() => { return rtThis.anchoredPosition; }, v => { rtThis.anchoredPosition = v; }, new Vector2(0, 700f), 2.5f);
                break;
            case EyeType.Bottom:
                DOTween.To(() => { return rtThis.anchoredPosition; }, v => { rtThis.anchoredPosition = v; }, new Vector2(0, -700f), 2.5f);
                break;
        }
        
    }
}
