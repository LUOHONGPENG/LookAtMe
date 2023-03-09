using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EffectColor : MonoBehaviour
{
    public enum EffectColorType
    {
        Red,
        WhiteChapter,
        White
    }
    public Image imgColor;

    public void Init(EffectColorType type,float time = 0)
    {
        if(type == EffectColorType.Red)
        {
            imgColor.color = new Color(1f, 0, 0, 0);
            StartCoroutine(IE_ColorAniBlood());
        }
        else if(type == EffectColorType.WhiteChapter)
        {
            imgColor.color = new Color(1f, 1f, 1f, 0);
            imgColor.DOFade(1f, 0.2f);

            Destroy(this.gameObject, 0.5f);
        }
        else if(type == EffectColorType.White)
        {
            imgColor.color = new Color(1f, 1f, 1f, 0);
            StartCoroutine(IE_ColorAniWhite(time));
        }
    }

    public IEnumerator IE_ColorAniBlood()
    {
        imgColor.DOFade(0.7f, 0.4f);
        yield return new WaitForSeconds(0.5f);
        imgColor.DOFade(0f, 0.4f);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    public IEnumerator IE_ColorAniWhite(float time)
    {
        imgColor.DOFade(1f, time);
        yield return new WaitForSeconds(time + 0.2f);
        imgColor.DOFade(0f, time);
        yield return new WaitForSeconds(time + 0.2f);
        Destroy(this.gameObject);
    }
}
