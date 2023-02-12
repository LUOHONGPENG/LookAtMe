using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class LevelSecondDebate : LevelBasic
{
    private void GoalFinishGenerateOtherThought()
    {
        for (int i = 0; i < 3; i++)
        {
            ThoughtContent other = listOtherThought[i];
            int tempType = 0;
            //Random generate the delay time 
            float timeDelay = Random.Range(1f, 2f);
            if (currentRound == LevelRound.Round1)
            {
                //Random generate the type of each teammates
                tempType = Random.Range(0, 3);
                while (tempType == (int)currentType)
                {
                    tempType = Random.Range(0, 3);
                }
            }
            else if (currentRound == LevelRound.Round2)
            {
                if ((int)currentType == 2)
                {
                    tempType = 0;
                }
                else
                {
                    tempType = (int)(currentType+1);
                }
            }
            other.ShowContent((ThoughtType)tempType, timeDelay);
        }
    }

    private void GoalFinishZeroScaleAllDragOption()
    {
        for (int i = 0; i < 3; i++)
        {
            listDragItem[i].transform.DOScale(0, GameGlobal.timeFD_commonAni);
        }
    }

    private void InitRoundNormalPeople()
    {
        for (int i = 0; i < 3; i++)
        {
            listPeople[i].NormalPeople();
        }
        itemPeopleMe.NormalPeople();
    }

    private void GoalFinishSurprisePeople()
    {
        if (currentRound == LevelRound.Round1 || currentRound == LevelRound.Round2)
        {
            for (int i = 0; i < 3; i++)
            {
                listPeople[i].SurprisePeople();
            }
            itemPeopleMe.SurprisePeople();
        }
    }
}
