using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState
{
    FirstDebate,
    FirstMeet,
    FirstIns,
    SecondDebate,
    SecondMeet,
    SecondIns,
    FakeSelfAbuse,
    Mirror,
    Hospital,
    End
}


public class LevelManager : MonoBehaviour
{
    public LevelState currentLevelState;

    public Transform tfContentSprite;
    public Transform tfContentImage;


    public void Init()
    {
        currentLevelState = LevelState.FirstDebate;
        LoadLevel();
    }
    
    public void LoadLevel()
    {
        //Clear
        PublicTool.ClearChildItem(tfContentImage);
        PublicTool.ClearChildItem(tfContentSprite);

        string strLevelName = "";

        if(currentLevelState!= LevelState.End)
        {
            strLevelName = currentLevelState.ToString();
        }

        if (strLevelName.Length > 0)
        {
            Object objLevel = Resources.Load("LevelPrefabs/Level" + strLevelName);

            //CheckError
            if (objLevel != null)
            {
                GameObject gobjLevel = Instantiate(objLevel, tfContentImage) as GameObject;
                LevelBasic itemLevel = gobjLevel.GetComponent<LevelBasic>();
                itemLevel.Init(this);
            }
        }
    }

    public void NextLevel()
    {
        if(currentLevelState == LevelState.End)
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
