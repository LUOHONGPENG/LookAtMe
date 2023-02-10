using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void Init(PeopleType peopleType)
    {
        this.peopleType = peopleType;
        NormalPeople();
        SetPosition();
    }

    public void NormalPeople()
    {
        imgPeople.sprite = listSpPeople[(int)peopleType * 2];
        imgPeople.SetNativeSize();
        imgSurprise.gameObject.SetActive(false);
    }

    public void SurprisePeople()
    {
        imgPeople.sprite = listSpPeople[(int)peopleType * 2 + 1];
        imgPeople.SetNativeSize();
        if (peopleType != PeopleType.Me)
        {
            imgSurprise.gameObject.SetActive(true);
        }
    }

    public void SetPosition()
    {
        imgPeople.transform.localPosition = listPosPeople[(int)peopleType];
        if(peopleType!= PeopleType.Me)
        {
            imgSurprise.transform.localPosition = listPosSurprise[(int)peopleType];
        }
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
