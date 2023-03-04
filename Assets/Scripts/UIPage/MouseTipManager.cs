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
    private Coroutine coShowTip;
    
    public void Init()
    {
        tfAni.DOScale(0, 0);
        imgTip.DOFade(0, 0);
    }

    public void ShowTip(TipType type,float time = 0)
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
        coShowTip = StartCoroutine(IE_ShowTip(time));
    }

    public IEnumerator IE_ShowTip(float time)
    {
        yield return new WaitForSeconds(time);
        tfAni.DOScale(0.2F, 0.5F);
        imgTip.DOFade(1F, 0.5F);
    }

    public void HideTip()
    {
        if (coShowTip != null)
        {
            StopCoroutine(coShowTip);
        }
        tfAni.DOScale(0, 0.5f);
        imgTip.DOFade(0, 0.5f);
    }
}
