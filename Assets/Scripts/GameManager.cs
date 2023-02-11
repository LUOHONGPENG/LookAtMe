using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Camera")]
    public Camera mainCamera;
    public Camera uiCamera;
    public CameraShake cameraShake;

    [Header("Manager")]
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

    public void ShakeCamera()
    {
        cameraShake.Shake();
        levelManager.canvasShake.Shake();
    }

}
