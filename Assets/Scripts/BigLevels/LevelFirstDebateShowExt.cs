using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class LevelFirstDebate
{
    public void GoalFinishGenerateOtherThought()
    {
        //If only one agree in turn two
        int ranAgreeID = Random.Range(0, 3);

        for (int i = 0; i < 3; i++)
        {
            ThoughtContent other = listOtherThought[i];
            //Random generate the type of each teammates
            int ranType = Random.Range(0, 3);
            //Random generate the delay time 
            float timeDelay = Random.Range(1f, 2f);

            if (currentRound == LevelRound.Round3)
            {
                ranType = (int)currentType;
            }//round3
            else if (currentRound == LevelRound.Round2)
            {
                if (ranAgreeID == i)
                {
                    ranType = (int)currentType;
                }
                else
                {
                    while (ranType == (int)currentType)
                    {
                        ranType = Random.Range(0, 3);
                    }
                }
            }
            else if (currentRound == LevelRound.Round1)
            {
                while (ranType == (int)currentType)
                {
                    ranType = Random.Range(0, 3);
                }
            }
            other.ShowContent((ThoughtType)ranType, timeDelay);
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
