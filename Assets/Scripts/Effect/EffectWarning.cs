using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EffectWarning : MonoBehaviour
{
    public CanvasGroup canvasGroupContent;

    public Image imgWarning;

    public void Init()
    {
        canvasGroupContent.alpha = 0;
        StartCoroutine(InitAni());
    }

    public IEnumerator InitAni()
    {
        yield return new WaitForSeconds(1f);
        canvasGroupContent.DOFade(1f, 1f);
        yield return new WaitForSeconds(5f);
        canvasGroupContent.DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        imgWarning.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        GameManager.Instance.effectManager.ClearContent();
    }
}
