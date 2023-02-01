using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemInsComment : MonoBehaviour
{
    public Image imgComment;

    public List<Sprite> listGoodComment;
    public List<Sprite> listBadComment;

    public void InitImage(int id, bool isGood)
    {
        imgComment.transform.DOScale(0, 0);
        if (isGood)
        {
            imgComment.sprite = listGoodComment[id];
        }
        else
        {
            imgComment.sprite = listBadComment[id];
        }
        imgComment.SetNativeSize();
    }

    public void InitAni(float delayTime)
    {
        StartCoroutine(IE_Ani(delayTime));
    }

    public IEnumerator IE_Ani(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        imgComment.transform.DOScale(2f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        imgComment.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);

        yield break;
    }

}
