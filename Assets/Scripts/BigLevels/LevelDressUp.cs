using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDressUp : LevelBasic
{
    public DressSlot itemDressSlot;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        itemDressSlot.Init(this);
    }

    public IEnumerator IE_DragGoalFinish()
    {
        //Change to next level after 2 seconds
        yield return new WaitForSeconds(2f);
        NextLevel();
    }
}
