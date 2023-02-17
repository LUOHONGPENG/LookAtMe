using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum TransitionIconType
{
    Cheer,
    Dress,
    Love,
    Shape
}
public class EffectTransitionIcon : MonoBehaviour
{


    public CanvasGroup canvasGroupAll;
    public Image imgBg;
    public Image imgIcon;
    public Animator aniIcon;

    public void Init(TransitionIconType type)
    {
        imgBg.DOFade(0, 0);
        imgIcon.DOFade(0, 0);
        switch (type)
        {
            case TransitionIconType.Cheer:
                aniIcon.Play("Cheer", 0, -1);
                break;
        }
        canvasGroupAll.blocksRaycasts = true;
        StartCoroutine(IE_Ani());
    }

    public IEnumerator IE_Ani()
    {
        imgBg.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        imgIcon.DOFade(1, 0.5f);
        yield return new WaitForSeconds(3f);
        imgIcon.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        canvasGroupAll.blocksRaycasts = false;
        imgBg.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

}
