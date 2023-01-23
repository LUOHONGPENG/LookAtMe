using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    
}

public struct LevelInfo
{
    public LevelState levelState;
    public string levelUrl;
    public bool isImage;

    public LevelInfo(LevelState levelState, bool isImage)
    {
        this.levelState = levelState;
        this.levelUrl = "";
        this.isImage = true;
    }
}