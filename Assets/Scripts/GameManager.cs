using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public SoundManager soundManager;
    public LevelManager levelManager;

    public void Start()
    {
        soundManager.Init();
        levelManager.Init();
    }
}
