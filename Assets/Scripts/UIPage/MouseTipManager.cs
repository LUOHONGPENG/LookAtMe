using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum TipType
{
    Click,
    Drag
}

public class MouseTipManager : MonoBehaviour
{



    public Transform tfAni;
    public Animator aniTip;
    public Image imgTip;

    public void Init()
    {
        tfAni.DOScale(0, 0);
        imgTip.DOFade(0, 0);
    }

    public void ShowTip(TipType type)
    {
        switch (type)
        {
            case TipType.Click:
                aniTip.Play("Click");
                break;
            case TipType.Drag:
                aniTip.Play("Drag");
                break;
        }
        tfAni.DOScale(0.2F, 0.5F);
        imgTip.DOFade(1F, 0.5F);
    }

    public void HideTip()
    {
        tfAni.DOScale(0, 0.5f);
        imgTip.DOFade(0, 0.5f);
    }
}
