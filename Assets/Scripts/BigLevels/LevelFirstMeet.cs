using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFirstMeet : LevelBasic
{
    public List<ItemPartyPeople> listPartyPeople = new List<ItemPartyPeople>();
    Coroutine coNextLevel = null;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        foreach(ItemPartyPeople people in listPartyPeople)
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
            if (coNextLevel == null)
            {
                coNextLevel = StartCoroutine(IE_NextLevel());
            }
        }
    }

    public IEnumerator IE_NextLevel()
    {
        yield return new WaitForSeconds(2f);
        NextLevel();
    }
}
