using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<LevelState, LevelInfo> dicLevelInfo = new Dictionary<LevelState, LevelInfo>();

    public void Init()
    {
        dicLevelInfo.Clear();
        dicLevelInfo.Add(LevelState.FirstDebate, new LevelInfo(LevelState.FirstDebate, true));
        dicLevelInfo.Add(LevelState.FirstMeet, new LevelInfo(LevelState.FirstMeet, true));
        dicLevelInfo.Add(LevelState.FirstIns, new LevelInfo(LevelState.FirstIns, true));
        dicLevelInfo.Add(LevelState.SecondDebate, new LevelInfo(LevelState.SecondDebate, true));
        dicLevelInfo.Add(LevelState.SecondMeet, new LevelInfo(LevelState.SecondMeet, true));
        dicLevelInfo.Add(LevelState.SecondIns, new LevelInfo(LevelState.SecondIns, true));
        dicLevelInfo.Add(LevelState.FakeSelfAbuse, new LevelInfo(LevelState.FakeSelfAbuse, true));
        dicLevelInfo.Add(LevelState.Mirror, new LevelInfo(LevelState.Mirror, true));
        dicLevelInfo.Add(LevelState.Hospital, new LevelInfo(LevelState.Hospital, true));


    }

}



public struct LevelInfo
{
    public LevelState levelState;
    public bool isImage;
    public string Url;

    public LevelInfo(LevelState levelState, bool isImage)
    {
        this.levelState = levelState;
        this.isImage = isImage;
        this.Url = "LevelPrefabs/Level" + levelState.ToString();
    }
}