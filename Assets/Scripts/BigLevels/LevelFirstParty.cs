using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFirstParty : LevelBasic
{
    [Header("PartyPeople")]
    public GameObject pfPartyPeople;
    public Transform tfPartyPeople;
    public List<Vector2> listPosPartyPeople = new List<Vector2>();

    //A list that contains 4 partypeople prefabs
    private List<ItemPartyPeople> listPartyPeople = new List<ItemPartyPeople>();
    //When 4 people look the character, it become true
    public bool isLookDone = false;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        isLookDone = false;

        InitPrefab();
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
            StartCoroutine(IE_LevelComplete());
        }
    }

    public IEnumerator IE_LevelComplete()
    {
        //Change to next level after 2 seconds
        yield return new WaitForSeconds(2f);
        NextLevel();
    }
}
