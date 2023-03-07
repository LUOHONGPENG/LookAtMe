using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PartyPeopleType
{
    Normal,
    Bored
}

public class ItemPartyPeople : MonoBehaviour
{
    

    //View
    [Header("Sprite")]
    public List<Sprite> listSpPeopleNormal = new List<Sprite>();
    public List<Sprite> listSpPeopleLove = new List<Sprite>();
    public List<Sprite> listSpPeopleSlient = new List<Sprite>();
    public List<Sprite> listSpPeopleAngry = new List<Sprite>();

    [Header("Interaction")]
    public Image imgPartyPeople;
    public Button btnPartyPeople;
    public CommonHoverUI hoverBtnPeople;

    [Header("Bar")]
    public Image imgBar;
    public Image imgBarFill;
    public List<Sprite> listSpBar = new List<Sprite>();
    public List<Vector2> listPosBar = new List<Vector2>();


    //Data
    public bool isFlip = false;
    private int peopleID = 0;
    private int countFlip = 0;
    private float timerFlip = 0f;
    private float timerFlipLimit = 0f;
    private PartyPeopleType peopleType;
    private LevelBasic parent;
    private bool isInit = false;

    public void Init(int ID,LevelBasic parent)
    {
        this.peopleID = ID;
        this.parent = parent;

        if (ID % 2 == 1)
        {
            imgBar.rectTransform.anchoredPosition = listPosBar[1];
        }
        else
        {
            imgBar.rectTransform.anchoredPosition = listPosBar[0];
        }
        imgBar.sprite = listSpBar[0];


        if (parent is LevelFirstParty)
        {
            peopleType = PartyPeopleType.Normal;
            timerFlipLimit = GameGlobal.timerFP_notice;
        }
        else if(parent is LevelSecondParty)
        {
            peopleType = PartyPeopleType.Bored;
            timerFlipLimit = GameGlobal.timerSP_notice;
        }


        isFlip = false;
        imgPartyPeople.sprite = listSpPeopleNormal[ID];
        imgPartyPeople.SetNativeSize();

        btnPartyPeople.onClick.RemoveAllListeners();
        btnPartyPeople.onClick.AddListener(delegate ()
        {
            ClickPeople();
        });

        isInit = true;
    }

    private void Update()
    {
        if (!isInit)
        {
            return;
        }

        if (isFlip && parent!=null && !parent.isTaskDoneExtra)
        {
            timerFlip -= Time.deltaTime;
            if (timerFlip <= 0)
            {
                FlipBack();
            }
        }

        UpdateBarUI();
    }


    #region FlipFunction

    private void UpdateBarUI()
    {
        imgBarFill.fillAmount = timerFlip / timerFlipLimit;
    }

    public void ClickPeople()
    {
        switch (peopleType)
        {
            case PartyPeopleType.Normal:
                timerFlip += timerFlipLimit * 1f;
                break;
            case PartyPeopleType.Bored:
                timerFlip += timerFlipLimit * 0.34f;
                break;
        }

        if (timerFlip >= timerFlipLimit)
        {
            timerFlip = timerFlipLimit;

            if (!isFlip)
            {
                countFlip++;
                Flip();
                parent.FlipPartyPeople(peopleID);
            }
        }
    }



    public void Flip()
    {
        timerFlip = GameGlobal.timerFP_notice;
        if(parent is LevelFirstParty)
        {
            imgPartyPeople.sprite = listSpPeopleLove[peopleID];
        }
        else if(parent is LevelSecondParty)
        {
            if (countFlip >= 2)
            {
                imgPartyPeople.sprite = listSpPeopleAngry[peopleID];
            }
            else
            {
                imgPartyPeople.sprite = listSpPeopleSlient[peopleID];
            }
        }
        imgBar.sprite = listSpBar[1];
        isFlip = true;
    }
    public void FlipBack()
    {
        timerFlip = 0;
        imgPartyPeople.sprite = listSpPeopleNormal[peopleID];
        imgBar.sprite = listSpBar[0];
        isFlip = false;
    }
    #endregion
}
