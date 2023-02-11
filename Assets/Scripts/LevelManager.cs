using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState
{
    FirstDebate,
    FirstParty,
    FirstIns,
    SecondDebate,
    DressUp,
    SecondParty,
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
    public CameraShake canvasShake;

    [Header("Test")]
    public bool isTestMode = false;
    public LevelState testState = LevelState.DressUp;

    public void Init()
    {
        if (isTestMode)
        {
            currentLevelState = testState;
        }
        else
        {
            currentLevelState = LevelState.FirstDebate;
        }
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
            if (!levelInfo.isDone)
            {
                NextLevel();
                return;
            }
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
