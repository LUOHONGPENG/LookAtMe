using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LevelState
{
    FirstDebate,
    FirstParty,
    FirstIns,
    Clock,
    ChapterTwo,
    SecondDebate,
    DressUp,
    SecondParty,
    SecondIns,
    FakeSuicide,
    ThirdIns,
    Mirror,
    RealSuicide,
    Hospital,
    Diagnosis,
    End
}


public partial class LevelManager : MonoBehaviour
{
    //Data
    public LevelState currentLevelState;

    //View container
    public Transform tfContentSprite;
    public Transform tfContentImage;

    [Header("Test")]
    public bool isTestMode = false;
    public LevelState testState = LevelState.DressUp;

    public void Init()
    {
        if (isTestMode)
        {
            currentLevelState = testState;
            LoadLevel();
        }
        else
        {
            currentLevelState = LevelState.FirstDebate;
        }
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
