using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState
{
    FirstMeet,
    FirstIns,
    SecondMeet,
    SecondIns,
    FakeSelfHurt,
    End
}


public class LevelManager : MonoBehaviour
{
    public LevelState currentLevelState;

    public Transform tfContentSprite;
    public Transform tfContentImage;


    public void Init()
    {

    }
    
    public void LoadLevel()
    {
        //Clear
        PublicTool.ClearChildItem(tfContentImage);
        PublicTool.ClearChildItem(tfContentSprite);

        //GameObject objLevel = 
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
