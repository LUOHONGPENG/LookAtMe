using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartPageManager : MonoBehaviour
{
    public enum PageType
    {
        Main,
        Setting,
        About
    }

    public GameObject objPopups;
    public CanvasGroup canvasGroupAll;

    public GameObject objMain;
    public GameObject objSetting;
    public GameObject objAbout;
    [Header("Main")]
    public Button btnStart;
    public Button btnSetting;
    public Button btnAbout;
    [Header("Setting")]
    public Button btnSound;
    public Button btnMusic;
    public Button btnBackS;
    public Image imgSound;
    public Image imgMusic;
    public List<Sprite> listSpSound;
    public List<Sprite> listSpMusic;
    [Header("About")]
    public Button btnBackA;


    public void Init()
    {
        btnStart.onClick.RemoveAllListeners();
        btnStart.onClick.AddListener(delegate ()
        {
            PublicTool.PlaySound(SoundType.Click);
            PublicTool.TransitionChapter(1);
            canvasGroupAll.blocksRaycasts = false;
            StartCoroutine(IE_Close());
            GameManager.Instance.uiManager.ShowMenu();
        });

        btnSetting.onClick.RemoveAllListeners();
        btnSetting.onClick.AddListener(delegate ()
        {
            PublicTool.PlaySound(SoundType.Click);
            ChangePage(PageType.Setting);
        });

        btnAbout.onClick.RemoveAllListeners();
        btnAbout.onClick.AddListener(delegate ()
        {
            PublicTool.PlaySound(SoundType.Click);
            ChangePage(PageType.About);
        });

        btnBackS.onClick.RemoveAllListeners();
        btnBackS.onClick.AddListener(delegate ()
        {
            PublicTool.PlaySound(SoundType.Click);
            ChangePage(PageType.Main);
        });

        btnBackA.onClick.RemoveAllListeners();
        btnBackA.onClick.AddListener(delegate ()
        {
            PublicTool.PlaySound(SoundType.Click);
            ChangePage(PageType.Main);
        });

        btnSound.onClick.RemoveAllListeners();
        btnSound.onClick.AddListener(delegate ()
        {
            GameManager.Instance.isSoundOn = !GameManager.Instance.isSoundOn;
            UpdateSoundButton();
            PublicTool.PlaySound(SoundType.Click);
        });

        //Music
        btnMusic.onClick.RemoveAllListeners();
        btnMusic.onClick.AddListener(delegate ()
        {
            GameManager.Instance.isMusicOn = !GameManager.Instance.isMusicOn;
            UpdateMusicButton();
            PublicTool.PlaySound(SoundType.Click);
        });
    }

    public IEnumerator IE_Close()
    {
        canvasGroupAll.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.levelManager.LoadLevel();
        objPopups.SetActive(false);
    }

    public void ShowPage()
    {
        canvasGroupAll.alpha = 0;
        objPopups.SetActive(true);
        canvasGroupAll.DOFade(1, 1f);
        canvasGroupAll.blocksRaycasts = true;
        ChangePage(PageType.Main);
    }

    public void ChangePage(PageType pageType)
    {
        switch (pageType)
        {
            case PageType.Main:
                objMain.SetActive(true);
                objSetting.SetActive(false);
                objAbout.SetActive(false);
                break;
            case PageType.Setting:
                objMain.SetActive(false);
                objSetting.SetActive(true);
                objAbout.SetActive(false);
                UpdateSoundButton();
                UpdateMusicButton();
                break;
            case PageType.About:
                objMain.SetActive(false);
                objSetting.SetActive(false);
                objAbout.SetActive(true);
                break;
        }
    }

    public void UpdateSoundButton()
    {
        if (GameManager.Instance.isSoundOn)
        {
            imgSound.sprite = listSpSound[0];
        }
        else
        {
            imgSound.sprite = listSpSound[1];
        }
        imgSound.SetNativeSize();
    }

    public void UpdateMusicButton()
    {
        if (GameManager.Instance.isMusicOn)
        {
            imgMusic.sprite = listSpMusic[0];
        }
        else
        {
            imgMusic.sprite = listSpMusic[1];
        }
        imgMusic.SetNativeSize();

    }
}
