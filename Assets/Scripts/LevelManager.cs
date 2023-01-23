using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState
{
    FirstDebate,
    FirstMeet,
    FirstIns,
    SecondMeet,
    SecondIns,
    FakeSelfAbuse,
    End
}


public class LevelManager : MonoBehaviour
{
    public LevelState currentLevelState;

    public Transform tfContentSprite;
    public Transform tfContentImage;


    public void Init()
    {
        currentLevelState = LevelState.FirstMeet;
        LoadLevel();
    }
    
    public void LoadLevel()
    {
        //Clear
        PublicTool.ClearChildItem(tfContentImage);
        PublicTool.ClearChildItem(tfContentSprite);

        Object objLevel = Resources.Load("LevelPrefabs/LevelFirstMeet");
        GameObject gobjLevel = Instantiate(objLevel,tfContentImage)as GameObject;
        LevelBasic itemLevel = gobjLevel.GetComponent<LevelBasic>();
        itemLevel.Init(this);
    }

    public void NextLevel()
    {
        if(currentLevelState != LevelState.End)
        {
            //The final End
        }
        else
        {
            currentLevelState++;
            LoadLevel();
        }
    }
}
