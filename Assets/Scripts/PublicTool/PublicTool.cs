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

    public static void PlaySound(SoundType soundType,bool needFadeIn = false, bool needStop = false, float stopTime = 0)
    {
        GameManager.Instance.soundManager.PlaySound(soundType, needFadeIn,needStop, stopTime);
    }

    public static bool CheckSound(SoundType soundType)
    {
        return GameManager.Instance.soundManager.CheckSoundPlay(soundType);
    }


    public static void PlayMusic(MusicType musicType)
    {
        GameManager.Instance.soundManager.PlayMusic(musicType);
    }

    public static void StopMusic()
    {
        GameManager.Instance.soundManager.StopMusic();
    }

}
