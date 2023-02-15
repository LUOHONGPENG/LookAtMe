using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EffectColor : MonoBehaviour
{
    public enum EffectColorType
    {
        Red
    }
    public Image imgColor;

    public void Init(EffectColorType type)
    {
        if(type == EffectColorType.Red)
        {
            imgColor.color = new Color(1f, 0, 0, 0);
        }

        StartCoroutine(IE_ColorAni());
    }

    public IEnumerator IE_ColorAni()
    {
        imgColor.DOFade(0.7f, 0.5f);
        yield return new WaitForSeconds(1f);
        imgColor.DOFade(0f, 0.5f);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
