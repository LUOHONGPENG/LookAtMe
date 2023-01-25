using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState
{
    FirstDebate,
    FirstMeet,
    FirstIns,
    SecondDebate,
    DressUp,
    SecondMeet,
    SecondIns,
    FakeSelfAbuse,
    Mirror,
    Hospital,
    End
}


public class LevelManager : MonoBehaviour
{
    //Data
    public LevelState currentLevelState;
    //View container
    public Transform tfContentSprite;
    public Transform tfContentImage;



    public void Init()
    {
        currentLevelState = LevelState.FirstDebate;
        LoadLevel();
    }
    
    public void LoadLevel()
    {
        //Clear the level
        PublicTool.ClearChildItem(tfContentImage);
        PublicTool.ClearChildItem(tfContentSprite);

        string levelUrl = "";

        DataManager data = GameManager.Instance.dataManager;
        LevelInfo levelInfo;

        if (data.dicLevelInfo.ContainsKey(currentLevelState))
        {
            levelInfo = data.dicLevelInfo[currentLevelState];
            levelUrl = levelInfo.Url;
        }
        else
        {
            //Stop Loading
            return;
        }

        //Check
        if (levelUrl.Length > 0)
        {
            Object objLevel = Resources.Load(levelUrl);

            //CheckError
            if (objLevel != null)
            {
                GameObject gobjLevel;
                if (levelInfo.isImage)
                {
                    gobjLevel = Instantiate(objLevel, tfContentImage) as GameObject;
                }
                else
                {
                    gobjLevel = Instantiate(objLevel, tfContentSprite) as GameObject;
                }
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