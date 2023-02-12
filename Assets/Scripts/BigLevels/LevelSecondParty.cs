using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSecondParty : LevelBasic
{
    public CanvasGroup canvasGroupParty;
    [Header("PartyPeople")]
    public GameObject pfPartyPeople;
    public Transform tfPartyPeople;
    public List<Vector2> listPosPartyPeople = new List<Vector2>();
    private List<ItemPartyPeople> listPartyPeople = new List<ItemPartyPeople>();

    [Header("MainCharacter")]
    public ItemCharacterDressUp itemCharacter;
    private int countFilp = 0;
    private bool isTriggerDark = false;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        canvasGroupParty.alpha = 1f;
        isTriggerDark = false;
        countFilp = 0;
        InitPrefab();
        itemCharacter.ChangeClothes(GameManager.Instance.levelManager.savedDressType);
    }
    public void InitPrefab()
    {
        listPartyPeople.Clear();
        PublicTool.ClearChildItem(tfPartyPeople);
        for (int i = 0; i < 4; i++)
        {
            GameObject objPeople = GameObject.Instantiate(pfPartyPeople, tfPartyPeople);
            ItemPartyPeople itemPartyPeople = objPeople.GetComponent<ItemPartyPeople>();
            listPartyPeople.Add(itemPartyPeople);
            itemPartyPeople.Init(i,this);
            itemPartyPeople.transform.localPosition = listPosPartyPeople[i];
        }
    }

    public override void FlipPartyPeople(int ID)
    {
        if (!isTriggerDark)
        {
            countFilp++;
            if (countFilp >= GameGlobal.countSP_fail)
            {
                isTriggerDark = true;
                StartCoroutine(IE_FlipGoalDeal());
            }
        }

        int numPeopleFlip = 0;
        foreach (ItemPartyPeople people in listPartyPeople)
        {
            if (people.isFlip)
            {
                numPeopleFlip++;
            }
        }

        if (numPeopleFlip >= 4 && !isTaskDoneExtra)
        {
            if(ID == listPartyPeople.Count - 1)
            {
                listPartyPeople[0].FlipBack();
            }
            else
            {
                listPartyPeople[ID+1].FlipBack();
            }
        }
    }

    public IEnumerator IE_FlipGoalDeal()
    {


        yield return new WaitForSeconds(2f);
        NextLevel();
    }
}
