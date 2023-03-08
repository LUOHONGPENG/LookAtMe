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
    Wow
}

public enum MusicType
{
    Discuss,
    InsHappy,
    InsSad,
    InsSuicide,
    Party,
    Hospital
}

public enum ChapterMusicType
{
    Chapter1,
    Chapter2,
    Chapter3
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


    public AudioSource m_discuss;
    public AudioSource m_insHappy;
    public AudioSource m_insSad;
    public AudioSource m_insSuicide;
    public AudioSource m_party;
    public AudioSource m_hospital;

    public AudioSource mc_1;
    public AudioSource mc_2;
    public AudioSource mc_3;

    private AudioSource au_musicPlaying;
    private AudioSource au_musicChapterPlaying;
    private ChapterMusicType characterMusicType;

    private Dictionary<SoundType, float> dic_SoundStartTime = new Dictionary<SoundType, float>();
    private Dictionary<SoundType, AudioSource> dic_SoundType = new Dictionary<SoundType, AudioSource>();
    private Dictionary<MusicType, float> dic_MusicVloume = new Dictionary<MusicType, float>();
    private Dictionary<ChapterMusicType, float> dic_ChapterVloume = new Dictionary<ChapterMusicType, float>();


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

        dic_MusicVloume.Add(MusicType.Discuss, 0.2f);
        dic_MusicVloume.Add(MusicType.InsSuicide, 0.06f);

        dic_ChapterVloume.Add(ChapterMusicType.Chapter1, 0.15f);
        dic_ChapterVloume.Add(ChapterMusicType.Chapter2, 0.05f);
        dic_ChapterVloume.Add(ChapterMusicType.Chapter3, 0.15f);

    }

    #region Sound
    public float GetSoundTime(SoundType soundType)
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

        tempSound.time = GetSoundTime(soundType);
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

    #endregion

    #region Music
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
            case MusicType.InsSuicide:
                tempSound = m_insSuicide;
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
        if (au_musicPlaying != null)
        {
            StopMusic(true);
        }
        tempSound.volume = 0;
        tempSound.Play();

        float goalVolume = 1f;
        if (dic_MusicVloume.ContainsKey(musicType))
        {
            goalVolume = dic_MusicVloume[musicType];
        }

        tempSound.DOFade(goalVolume, 1.5f);
        //Chpater Music
        float goalChpaterVolume = 1f;
        if (dic_ChapterVloume.ContainsKey(characterMusicType))
        {
            goalChpaterVolume = dic_ChapterVloume[characterMusicType];
        }
        au_musicChapterPlaying.DOFade(goalChpaterVolume * 0.5f, 2f);

        au_musicPlaying = tempSound;
    }

    public void StopMusic(bool stopChapter)
    {
        if (au_musicPlaying != null)
        {
            //Chpater Music
            if (!stopChapter)
            {
                float goalChpaterVolume = 1f;
                if (dic_ChapterVloume.ContainsKey(characterMusicType))
                {
                    goalChpaterVolume = dic_ChapterVloume[characterMusicType];
                }
                au_musicChapterPlaying.DOFade(goalChpaterVolume, 2f);
            }

            au_musicPlaying.DOFade(0, 1f);
            StartCoroutine(IE_StopMusic(au_musicPlaying));
        }
    }

    private IEnumerator IE_StopMusic(AudioSource temp)
    {
        yield return new WaitForSeconds(1f);
        temp.Stop();
    }
    #endregion

    #region ChapterMusic

    public void PlayChapterMusic(ChapterMusicType musicType)
    {
        AudioSource tempSound;

        switch (musicType)
        {
            case ChapterMusicType.Chapter1:
                tempSound = mc_1;
                break;
            case ChapterMusicType.Chapter2:
                tempSound = mc_2;
                break;
            case ChapterMusicType.Chapter3:
                tempSound = mc_3;
                break;
            default:
                tempSound = mc_1;
                break;
        }
        if (au_musicChapterPlaying != null)
        {
            StopChapterMusic();
        }
        tempSound.volume = 0;
        tempSound.Play();

        float goalVolume = 0.15f;
        characterMusicType = musicType;
        if (dic_ChapterVloume.ContainsKey(musicType))
        {
            goalVolume = dic_ChapterVloume[musicType];
        }
        tempSound.DOFade(goalVolume, 2f);
        au_musicChapterPlaying = tempSound;
    }

    public void StopChapterMusic()
    {
        if (au_musicChapterPlaying != null)
        {
            au_musicChapterPlaying.DOFade(0, 1f);
            StartCoroutine(IE_StopChapterMusic(au_musicChapterPlaying));
        }
    }

    private IEnumerator IE_StopChapterMusic(AudioSource temp)
    {
        yield return new WaitForSeconds(1f);
        temp.Stop();
    }
    #endregion

}
