using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ThoughtContent : MonoBehaviour
{
    //0=square 1= circle 2=traingle
    public List<Sprite> listSpShape;
    public Sprite spEllipsis;
    public Image imgContent;
    public bool isOther;

    public void Init(bool isOther)
    {
        this.isOther = isOther;
        if (isOther)
        {
            RoundInit();
        }
        else
        {
            imgContent.DOFade(0, 0);
            imgContent.transform.DOScale(0, 0);
        }
    }

    public void ShowContent(ThoughtType type,float delayTime)
    {
        if (isOther)
        {
            StartCoroutine(IE_delayShowOther(type, delayTime));
        }
        else
        {
            StartCoroutine(IE_ShowMe(type));
        }
    }

    //Init the thought content every round starts
    public void RoundInit()
    {
        if (isOther)
        {
            imgContent.sprite = spEllipsis;
        }
        else
        {
            HideAni();
        }
    }

    //The animation of scaling down to zero 
    public void HideAni()
    {
        imgContent.DOFade(0, GameGlobal.timeFD_commonAni);
        imgContent.transform.DOScale(0, GameGlobal.timeFD_commonAni);
    }

    //Show My thought don't need delay
    private IEnumerator IE_ShowMe(ThoughtType type)
    {
        switch (type)
        {
            case ThoughtType.Square:
                imgContent.sprite = listSpShape[0];
                break;
            case ThoughtType.Circle:
                imgContent.sprite = listSpShape[1];
                break;
            case ThoughtType.Triangle:
                imgContent.sprite = listSpShape[2];
                break;
        }
        imgContent.DOFade(1f,GameGlobal.timeFD_commonAni);
        imgContent.transform.DOScale(1f,GameGlobal.timeFD_commonAni);
        yield break;
    }

    //Other people's thought need delay
    private IEnumerator IE_delayShowOther(ThoughtType type, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        switch (type)
        {
            case ThoughtType.Square:
                imgContent.sprite = listSpShape[0];
                break;
            case ThoughtType.Circle:
                imgContent.sprite = listSpShape[1];
                break;
            case ThoughtType.Triangle:
                imgContent.sprite = listSpShape[2];
                break;
        }
    }
}
