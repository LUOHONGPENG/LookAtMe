using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFirstMeet : LevelBasic
{
    //A list that contains 4 partypeople prefabs
    public List<ItemPartyPeople> listPartyPeople = new List<ItemPartyPeople>();
    //When 4 people look the character, it become true
    public bool isLookDone = false;
    //A coroutine that call 
    private Coroutine coNextLevel = null;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        isLookDone = false;

        foreach (ItemPartyPeople people in listPartyPeople)
        {
            people.Init(this);
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
        if (numPeopleFlip >= 4)
        {
            isLookDone = true;
            if (coNextLevel == null)
            {
                coNextLevel = StartCoroutine(IE_LevelComplete());
            }
        }
    }

    public IEnumerator IE_LevelComplete()
    {
        //Change to next level after 2 seconds
        yield return new WaitForSeconds(2f);
        NextLevel();
    }
}
