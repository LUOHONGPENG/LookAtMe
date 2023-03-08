using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicTool : MonoBehaviour
{
    public static void ClearChildItem(UnityEngine.Transform tf)
    {
        foreach (UnityEngine.Transform item in tf)
        {
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }

    public static float CalculateAngle(Vector2 from,Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }

    public static Vector2 GetMousePosition2D()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePosition.x, mousePosition.y);
    }

    public static void ShakeCamera()
    {
        GameManager.Instance.ShakeCamera();
    }

    #region ShortCut
    public static void TransitionIconEffect(TransitionIconType type)
    {
        GameManager.Instance.effectManager.InitTransitionIcon(type);
    }

    public static void TransitionChapter(int ID)
    {
        GameManager.Instance.effectManager.InitChapter(ID);
    }

    public static GameObject PostProcessEffect(PostProcessType type)
    {
        return GameManager.Instance.effectManager.InitPostProcess(type);
    }
    #endregion

    #region Music
    public static void PlaySound(SoundType soundType,bool needFadeIn = false, bool needStop = false, float stopTime = 0)
    {
        if (GameManager.Instance.isSoundOn)
        {
            GameManager.Instance.soundManager.PlaySound(soundType, needFadeIn, needStop, stopTime);
        }
    }

    public static void StopSound(SoundType soundType)
    {
        GameManager.Instance.soundManager.StopSound(soundType);
    }

    public static bool CheckSound(SoundType soundType)
    {
        return GameManager.Instance.soundManager.CheckSoundPlay(soundType);
    }


    public static void PlayMusic(MusicType musicType)
    {
        if (GameManager.Instance.isMusicOn)
        {
            GameManager.Instance.soundManager.PlayMusic(musicType);
        }
    }

    public static void StopMusic(bool stopChapter)
    {
        GameManager.Instance.soundManager.StopMusic(stopChapter);
    }

    public static void PlayChapterMusic(ChapterMusicType musicType)
    {
        if (GameManager.Instance.isMusicOn)
        {
            GameManager.Instance.soundManager.PlayChapterMusic(musicType);
        }
    }

    public static void StopChapterMusic()
    {
        GameManager.Instance.soundManager.StopChapterMusic();
    }
    #endregion

    #region Tip

    public static void ShowMouseTip(TipType tipType,float time = 0)
    {
        GameManager.Instance.uiManager.mouseTipManager.ShowTip(tipType,time);
    }


    public static void HideMouseTip()
    {
        GameManager.Instance.uiManager.mouseTipManager.HideTip();
    }
    #endregion
}
