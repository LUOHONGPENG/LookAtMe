using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum SoundType
{
    Click,
    Camera,
    Breath,
    HeartBeat
}

public enum MusicType
{
    Discuss,
    InsHappy,
    InsSad,
    Party
}


public class SoundManager : MonoBehaviour
{
    public AudioSource au_click;
    public AudioSource au_camera;
    public AudioSource au_breath;
    public AudioSource au_heartBeat;

    public AudioSource m_discuss;

    private AudioSource au_voicePlaying;


    private Dictionary<SoundType, float> dic_SoundStartTime = new Dictionary<SoundType, float>();

    public void Init()
    {
        InitTime();
    }

    public void InitTime()
    {
        dic_SoundStartTime.Clear();
        dic_SoundStartTime.Add(SoundType.Click, 0);

    }

    public float GetTime(SoundType soundType)
    {
        if (dic_SoundStartTime.ContainsKey(soundType))
        {
            return dic_SoundStartTime[soundType];
        }
        else
        {
            return 0.2f;
        }
    }
    public void PlaySound(SoundType soundType, bool needFadeIn = false,  bool needStop = false, float stopTime = 0)
    {
        AudioSource tempSound;

        switch (soundType)
        {
            case SoundType.Click:
                tempSound = au_click;
                break;
            case SoundType.Camera:
                tempSound = au_camera;
                break;
            case SoundType.Breath:
                tempSound = au_breath;
                break;
            case SoundType.HeartBeat:
                tempSound = au_heartBeat;
                break;

            default:
                tempSound = au_click;
                break;
        }

        tempSound.time = GetTime(soundType);
        //AudioClip play = tempVoice.clip;
        //AudioSource.PlayClipAtPoint(play,transform.position, 10f); ;
        tempSound.Play();

        if (needFadeIn)
        {
            tempSound.volume = 0;
            tempSound.DOFade(1f,0.5f);
        }


        if (needStop)
        {
            StartCoroutine(IE_StopSound(tempSound,stopTime));
        }

    }

    public IEnumerator IE_StopSound(AudioSource sound, float time)
    {
        yield return new WaitForSeconds(time);
        sound.Stop();
    }

    public void PlayMusic(MusicType musicType)
    {
        AudioSource tempSound;

        switch (musicType)
        {
            case MusicType.Discuss:
                tempSound = m_discuss;
                break;
            default:
                tempSound = m_discuss;
                break;
        }
        if (au_voicePlaying != null)
        {
            au_voicePlaying.Stop();
        }
        tempSound.Play();
        au_voicePlaying = tempSound;

    }

    public void StopMusic()
    {
        if (au_voicePlaying != null)
        {
            au_voicePlaying.DOFade(0, 1f);
            StartCoroutine(IE_StopMusic());
        }
    }

    private IEnumerator IE_StopMusic()
    {
        yield return new WaitForSeconds(1f);
        au_voicePlaying.Stop();

    }
}
