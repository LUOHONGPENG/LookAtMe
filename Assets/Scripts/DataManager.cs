using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    //This Dictionary contains the Level Information.!
    public Dictionary<LevelState, LevelInfo> dicLevelInfo = new Dictionary<LevelState, LevelInfo>();

    public void Init()
    {
        dicLevelInfo.Clear();
        dicLevelInfo.Add(LevelState.FirstDebate, new LevelInfo(LevelState.FirstDebate, true,true));
        dicLevelInfo.Add(LevelState.FirstParty, new LevelInfo(LevelState.FirstParty, true,true));
        dicLevelInfo.Add(LevelState.FirstIns, new LevelInfo(LevelState.FirstIns, true,true));
        dicLevelInfo.Add(LevelState.DressUp, new LevelInfo(LevelState.DressUp, true,true));
        dicLevelInfo.Add(LevelState.SecondDebate, new LevelInfo(LevelState.SecondDebate, true,true));
        dicLevelInfo.Add(LevelState.SecondParty, new LevelInfo(LevelState.SecondParty, true,true));
        dicLevelInfo.Add(LevelState.SecondIns, new LevelInfo(LevelState.SecondIns, true,true));
        dicLevelInfo.Add(LevelState.FakeSuicide, new LevelInfo(LevelState.FakeSuicide, false,true));
        dicLevelInfo.Add(LevelState.ThirdIns, new LevelInfo(LevelState.ThirdIns, true, true));
        dicLevelInfo.Add(LevelState.Mirror, new LevelInfo(LevelState.Mirror, true, true));
        dicLevelInfo.Add(LevelState.Hospital, new LevelInfo(LevelState.Hospital, true, true));
    }

}



public struct LevelInfo
{
    public LevelState levelState;
    public bool isImage;
    public bool isDone;
    public string Url;

    public LevelInfo(LevelState levelState, bool isImage,bool isDone)
    {
        this.levelState = levelState;
        this.isImage = isImage;
        this.isDone = isDone;
        this.Url = "LevelPrefabs/Level" + levelState.ToString();
    }
}