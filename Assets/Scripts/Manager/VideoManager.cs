using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoManager : MonoBehaviour
{
    public VideoPlayer objVideoSad;

    public void Init()
    {
        objVideoSad.gameObject.SetActive(false);
        objVideoSad.targetCamera = GameManager.Instance.mainCamera;
    }
    public IEnumerator InitVideoHappy()
    {
        objVideoSad.gameObject.SetActive(true);
        objVideoSad.Play();
        yield return new WaitForSeconds(2f);
        objVideoSad.Pause();
        yield return new WaitForSeconds(0.5f);
        objVideoSad.gameObject.SetActive(false);
        yield break;
    }

    public IEnumerator InitVideoSad()
    {
        objVideoSad.gameObject.SetActive(true);
        objVideoSad.time = 2.5f;
        objVideoSad.Play();
        yield return new WaitForSeconds(3.2f);
        objVideoSad.Pause();
        yield return new WaitForSeconds(0.5f);
        objVideoSad.gameObject.SetActive(false);
        yield break;
    }
}
