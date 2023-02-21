using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EffectWarning : MonoBehaviour
{
    public CanvasGroup canvasGroupContent;

    public Image imgWarning;

    public Button btnOk;

    public void Init()
    {
        canvasGroupContent.alpha = 0;

        btnOk.interactable = true;
        btnOk.onClick.RemoveAllListeners();
        btnOk.onClick.AddListener(delegate ()
        {
            btnOk.interactable = false;
            StartCoroutine(IE_Close());
        });

        StartCoroutine(IE_InitAni());
    }

    public IEnumerator IE_InitAni()
    {
        yield return new WaitForSeconds(1f);
        canvasGroupContent.DOFade(1f, 1f);
        yield return new WaitForSeconds(5f);

    }

    public IEnumerator IE_Close()
    {
        canvasGroupContent.DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        imgWarning.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        GameManager.Instance.effectManager.ClearWarning();
    }
}
