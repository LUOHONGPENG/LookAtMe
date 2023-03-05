using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum SoundType
{
    Click,
    Camera,
    Breath,
    HeartBeat,
    Lipstick,
    Disagree,
    Break1,
    Break2,
    Cheer,
    Wow,
    Footstep
}

public enum MusicType
{
    Discuss,
    InsHappy,
    InsSad,
    Party,
    Hospital
}


public class SoundManager : MonoBehaviour
{
    public AudioSource au_click;
    public AudioSource au_camera;
    public AudioSource au_breath;
    public AudioSource au_heartBeat;
    public AudioSource au_lipstick;
    public AudioSource au_disagree;
    public AudioSource au_break1;
    public AudioSource au_break2;
    public AudioSource au_cheer;
    public AudioSource au_wow;
    public AudioSource au_footstep;


    public AudioSource m_discuss;
    public AudioSource m_insHappy;
    public AudioSource m_insSad;
    public AudioSource m_party;
    public AudioSource m_hospital;

    private AudioSource au_voicePlaying;

    private Dictionary<SoundType, float> dic_SoundStartTime = new Dictionary<SoundType, float>();
    private Dictionary<SoundType, AudioSource> dic_SoundType = new Dictionary<SoundType, AudioSource>();
    public void Init()
    {
        InitTime();
    }

    public void InitTime()
    {
        dic_SoundStartTime.Clear();
        dic_SoundStartTime.Add(SoundType.Click, 0);

        dic_SoundType.Clear();
        dic_SoundType.Add(SoundType.Click, au_click);
        dic_SoundType.Add(SoundType.Camera, au_camera);
        dic_SoundType.Add(SoundType.Breath, au_breath);
        dic_SoundType.Add(SoundType.HeartBeat, au_heartBeat);
        dic_SoundType.Add(SoundType.Lipstick, au_lipstick);
        dic_SoundType.Add(SoundType.Disagree, au_disagree);
        dic_SoundType.Add(SoundType.Break1, au_break1);
        dic_SoundType.Add(SoundType.Break2, au_break2);
        dic_SoundType.Add(SoundType.Cheer, au_cheer);
        dic_SoundType.Add(SoundType.Wow, au_wow);
        dic_SoundType.Add(SoundType.Footstep, au_footstep);
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
        AudioSource tempSound = au_click;

        if (dic_SoundType.ContainsKey(soundType))
        {
            tempSound = dic_SoundType[soundType];
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
        else
        {
            tempSound.volume = 1f;
        }

        if (needStop)
        {
            StartCoroutine(IE_StopSound(tempSound,stopTime));
        }
    }

    public void StopSound(SoundType soundType)
    {
        AudioSource tempSound = au_click;

        if (dic_SoundType.ContainsKey(soundType))
        {
            tempSound = dic_SoundType[soundType];
            tempSound.DOFade(0, 1f);
            StartCoroutine(IE_StopSound(tempSound, 1f));
        }
    }



    public bool CheckSoundPlay(SoundType soundType)
    {
        if (dic_SoundType.ContainsKey(soundType))
        {
            return dic_SoundType[soundType].isPlaying;
        }
        else
        {
            return false;
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
            case MusicType.InsHappy:
                tempSound = m_insHappy;
                break;
            case MusicType.InsSad:
                tempSound = m_insSad;
                break;
            case MusicType.Party:
                tempSound = m_party;
                break;
            case MusicType.Hospital:
                tempSound = m_hospital;
                break;
            default:
                tempSound = m_discuss;
                break;
        }
        if (au_voicePlaying != null)
        {
            au_voicePlaying.Stop();
        }
        tempSound.volume = 0;
        tempSound.Play();
        tempSound.DOFade(1f, 1f);
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
