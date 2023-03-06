using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoManager : MonoBehaviour
{
    public VideoPlayer objVideo;

    public IEnumerator InitVideoSad()
    {
        objVideo.gameObject.SetActive(true);
        objVideo.Play();
        yield return new WaitForSeconds(3f);
        objVideo.Pause();
        yield break;
    }
}
