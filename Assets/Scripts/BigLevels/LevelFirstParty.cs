using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelFirstParty : LevelBasic
{
    [Header("PartyPeople")]
    public GameObject pfPartyPeople;
    public Transform tfPartyPeople;
    public List<Vector2> listPosPartyPeople = new List<Vector2>();

    [Header("MainCharacter")]
    public Image imgLight;
    public Image imgRibbon;

    [Header("Shooting")]
    public Transform tfContentShoot;


    //A list that contains 4 partypeople prefabs
    private List<ItemPartyPeople> listPartyPeople = new List<ItemPartyPeople>();
    //When 4 people look the character, it become true
    public bool isLookDone = false;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        isLookDone = false;

        InitCharacterGroup();
        InitPrefab();
    }

    public void InitCharacterGroup()
    {
        imgLight.DOFade(0, 0);
        imgRibbon.transform.DOLocalMoveY(900f, 0);
    }

    public void InitPrefab()
    {
        listPartyPeople.Clear();
        PublicTool.ClearChildItem(tfPartyPeople);
        for(int i = 0; i < 4; i++)
        {
            GameObject objPeople = GameObject.Instantiate(pfPartyPeople, tfPartyPeople);
            ItemPartyPeople itemPartyPeople = objPeople.GetComponent<ItemPartyPeople>();
            listPartyPeople.Add(itemPartyPeople);
            itemPartyPeople.Init(this);
            itemPartyPeople.transform.localPosition = listPosPartyPeople[i];
        }
    }

    public void FlipPeople()
    {
        int numPeopleFlip = 0;
        foreach (ItemPartyPeople people in listPartyPeople)
        {
            if (people.isFlip)
            {
                numPeopleFlip++;
            }
        }

        //check whether 4 
        if (numPeopleFlip >= 4 && !isLookDone)
        {
            isLookDone = true;
            StartCoroutine(IE_ClickGoalDeal());
        }
    }

    public IEnumerator IE_ClickGoalDeal()
    {
        imgLight.DOFade(1f, GameGlobal.timerFP_light);
        imgRibbon.transform.DOLocalMoveY(100, GameGlobal.timerFP_light);
        yield return new WaitForSeconds(GameGlobal.timerFP_light);

        yield return new WaitForSeconds(2f);

        NextLevel();
        yield break;
    }



}
