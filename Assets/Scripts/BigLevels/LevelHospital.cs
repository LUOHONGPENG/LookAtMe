using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;

public class LevelHospital : LevelBasic
{
    public DragHosEye dragEyeTop;
    public DragHosEye dragEyeBottom;
    public Image imgBlack;
    //PostProcess
    private DepthOfField efBlur;
    private ChromaticAberration efChro;
    private bool isInit = false;
    private bool isPostChange = false;
    private float timerPostChange = 0;
    private bool isOpen = false;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        PublicTool.PlayMusic(MusicType.Hospital);
        GameObject objBlur = PublicTool.PostProcessEffect(PostProcessType.HospitalBlur);
        InitBlur(objBlur);
        InitChor(objBlur);
        StartCoroutine(IE_InitDark());

        isInit = true;
    }

    #region PostProcess
    public void InitBlur(GameObject obj)
    {
        Volume volume = obj.GetComponent<Volume>();
        DepthOfField tmp;
        if (volume.profile.TryGet<DepthOfField>(out tmp))
        {
            efBlur = tmp;
            efBlur.focalLength.value = 300f;
        }
    }
    public void InitChor(GameObject obj)
    {
        Volume volume = obj.GetComponent<Volume>();
        ChromaticAberration tmp;
        if (volume.profile.TryGet<ChromaticAberration>(out tmp))
        {
            efChro = tmp;
            efChro.intensity.value = 1f;
        }
    }

    public IEnumerator IE_InitDark()
    {
        imgBlack.DOFade(1f, 0);
        imgBlack.raycastTarget = true;
        PublicTool.ShowMouseTip(TipType.Drag, 0.5f);
        yield return new WaitForSeconds(0.5f);
        imgBlack.DOFade(0, 2F);
        yield return new WaitForSeconds(2f);
        imgBlack.raycastTarget = false;
    }

    #endregion

    public void Update()
    {
        if (!isInit)
        {
            return;
        }

        CheckBlurEffect();

        if (!isOpen && dragEyeBottom.absPosY > 280f && dragEyeTop.absPosY > 280f)
        {
            isOpen = true;
            StartCoroutine(IE_OpenEye());
        }

/*        if (dragEyeTop.isDragging)
        {
            dragEyeBottom.rtThis.anchoredPosition = new Vector2(0,-dragEyeTop.absPosY);
        }
        else if (dragEyeBottom.isDragging)
        {
            dragEyeTop.rtThis.anchoredPosition = new Vector2(0,dragEyeBottom.absPosY);
        }*/
    }

    public IEnumerator IE_OpenEye()
    {
        PublicTool.HideMouseTip();
        isPostChange = true;
        dragEyeTop.OpenEye();
        dragEyeBottom.OpenEye();
        yield return new WaitForSeconds(5f);

        yield return new WaitForSeconds(2f);
        isPostChange = false;
        GameManager.Instance.effectManager.ClearPostProcess();
        GameManager.Instance.effectManager.InitEye();
        yield return new WaitForSeconds(1f);
        NextLevel();
    }

    private void CheckBlurEffect()
    {
        if (isPostChange)
        {
            timerPostChange += Time.deltaTime;
            efBlur.focalLength.value = 300f - timerPostChange * 40f;
            efChro.intensity.value = 1f - timerPostChange * 0.2f;
        }
    }
}
