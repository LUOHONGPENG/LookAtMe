using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public SoundManager soundManager;
    public LevelManager levelManager;
    public DataManager dataManager;

    public void Start()
    {
        dataManager = new DataManager();
        dataManager.Init();

        soundManager.Init();
        levelManager.Init();
    }
}
