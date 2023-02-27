using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemDebatePeople : MonoBehaviour
{
    public enum PeopleType
    {
        LeftTop,
        RightTop,
        LeftBottom,
        Me
    }

    private PeopleType peopleType;
    public Image imgPeople;
    public Image imgSurprise;
    public List<Sprite> listSpPeople;
    public List<Vector2> listPosPeople;
    public List<Sprite> listSpSurprise;
    public List<Vector2> listPosSurprise;
    private Coroutine coSurprise;

    public void Init(PeopleType peopleType)
    {
        this.peopleType = peopleType;
        NormalPeople();
        SetPosition();
        InitSurprise();
        imgSurprise.gameObject.SetActive(true);
        imgSurprise.transform.localScale = new Vector3(0,0,1f);
    }

    public void NormalPeople()
    {
        imgPeople.sprite = listSpPeople[(int)peopleType * 2];
        imgPeople.SetNativeSize();
        imgSurprise.transform.DOScale(0, 0.5F);
    }

    public void SurprisePeople(bool isTag)
    {
        imgPeople.sprite = listSpPeople[(int)peopleType * 2 + 1];
        imgPeople.SetNativeSize();
        if (isTag)
        {
            if (coSurprise == null)
            {
                coSurprise = StartCoroutine(IE_Surprise());
            }
        }
    }

    private IEnumerator IE_Surprise()
    {
        imgSurprise.transform.DOScale(1.5f, 0.4F);
        yield return new WaitForSeconds(0.4f);
        imgSurprise.transform.DOScale(1f, 0.4F);
        coSurprise = null;
        yield break;
    }

    public void SetPosition()
    {
        imgPeople.transform.localPosition = listPosPeople[(int)peopleType];
        imgSurprise.transform.localPosition = listPosSurprise[(int)peopleType];

    }

    public void InitSurprise()
    {
        if (peopleType != PeopleType.Me)
        {
            imgSurprise.sprite = listSpSurprise[(int)peopleType];
        }
        imgSurprise.SetNativeSize();
    }
}
