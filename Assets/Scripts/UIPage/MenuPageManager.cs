using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPageManager : MonoBehaviour
{
    public GameObject objPopups;
    public Button btnBack;
    public Button btnRestart;
    public Button btnQuit;

    public void Init()
    {
        btnBack.onClick.RemoveAllListeners();
        btnBack.onClick.AddListener(delegate ()
        {
            PublicTool.PlaySound(SoundType.Click);
            objPopups.SetActive(false);
        });

        btnRestart.onClick.RemoveAllListeners();
        btnRestart.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("Main");
        });

        btnQuit.onClick.RemoveAllListeners();
        btnQuit.onClick.AddListener(delegate ()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        });
    }

    public void ShowPage()
    {
        objPopups.SetActive(true);

    }
}
