using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public StartPageManager startPageManager;
    public MenuPageManager menuPageManager;
    public MouseTipManager mouseTipManager;
    public Button btnMenu;

    public void Init()
    {
        startPageManager.Init();
        menuPageManager.Init();
        mouseTipManager.Init();

        btnMenu.onClick.RemoveAllListeners();
        btnMenu.onClick.AddListener(delegate ()
        {
            menuPageManager.ShowPage();
            btnMenu.transform.localScale = Vector2.one;
        });
        btnMenu.transform.DOScale(0, 0);
    }

    public void ShowMenu()
    {
        btnMenu.transform.DOScale(1, 1F);
    }
}
