using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class LevelFirstDebate
{
    private void GoalFinishGenerateOtherThought()
    {
        //If only one agree in turn two
        int ranAgreeID = Random.Range(0, 3);

        for (int i = 0; i < 3; i++)
        {
            ThoughtContent other = listOtherThought[i];
            //Random generate the type of each teammates
            int tempType = Random.Range(0, 3);
            //Random generate the delay time 
            float timeDelay = Random.Range(1f, 2f);

            if (currentRound == LevelRound.Round1)
            {
                while (tempType == (int)currentType)
                {
                    tempType = Random.Range(0, 3);
                }
            }
            else if (currentRound == LevelRound.Round2)
            {
                if (ranAgreeID == i)
                {
                    tempType = (int)currentType;
                }
                else
                {
                    while (tempType == (int)currentType)
                    {
                        tempType = Random.Range(0, 3);
                    }
                }
            }
            else if (currentRound == LevelRound.Round3)
            {
                tempType = (int)currentType;
            }//round3

            other.ShowContent((ThoughtType)tempType, timeDelay);
        }
    }

    private void GoalFinishZeroScaleAllDragOption()
    {
        imgDragBox.DOFade(0, GameGlobal.timeFD_commonAni);
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
                listPeople[i].SurprisePeople(false);
            }
            itemPeopleMe.SurprisePeople(true);
        }
    }
}
