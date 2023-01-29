using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDressUp : LevelBasic
{
    public IEnumerator IE_LevelComplete()
    {
        //Change to next level after 2 seconds
        yield return new WaitForSeconds(2f);
        NextLevel();
    }
}
