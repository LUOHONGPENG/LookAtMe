using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionIconType
{
    Cheer,
    Dress,
    Heart,
    Shape
}

public enum PostProcessType
{
    SecondPartyVig,
    ThirdInsBlur,
    MirrorVig,
    HospitalBlur
}

public class EffectManager : MonoBehaviour
{
    [Header("PostProcess")]
    public Transform tfPostProcess;
    public GameObject efSPVignette;
    public GameObject efTIBlur;
    public GameObject efMVignette;
    public GameObject efHBlur;
    [Header("UIEffect")]
    public Transform tfUIEffect;
    public GameObject pfColor;
    public GameObject pfTransitionIcon;
    public GameObject pfChapter;
    public GameObject pfCloseEye;
    [Header("Warning")]
    public Transform tfWarning;
    public GameObject pfWarning;
    public void Init()
    {
        if (!GameManager.Instance.levelManager.isTestMode)
        {
            InitWarning();
        }
    }

    #region Clear
    public void ClearContentAll()
    {
        PublicTool.ClearChildItem(tfPostProcess);
        PublicTool.ClearChildItem(tfUIEffect);
        PublicTool.ClearChildItem(tfWarning);
    }

    public void ClearPostProcess()
    {
        PublicTool.ClearChildItem(tfPostProcess);
    }

    public void ClearWarning()
    {
        PublicTool.ClearChildItem(tfWarning);
    }
    #endregion

    #region PostProcess
    public GameObject InitPostProcess(PostProcessType type)
    {
        switch (type)
        {
            case PostProcessType.SecondPartyVig:
                return InitSPVig();
            case PostProcessType.ThirdInsBlur:
                return InitTIBlur();
            case PostProcessType.MirrorVig:
                return InitMVig();
            case PostProcessType.HospitalBlur:
                return InitHBlur();
        }
        return null;
    }


    private GameObject InitSPVig()
    {
        GameObject obj = GameObject.Instantiate(efSPVignette, tfPostProcess);
        return obj;
    }

    private GameObject InitTIBlur()
    {
        GameObject obj = GameObject.Instantiate(efTIBlur, tfPostProcess);
        return obj;
    }

    private GameObject InitMVig()
    {
        GameObject obj = GameObject.Instantiate(efMVignette, tfPostProcess);
        return obj;
    }
    private GameObject InitHBlur()
    {
        GameObject obj = GameObject.Instantiate(efHBlur, tfPostProcess);
        return obj;
    }
    #endregion

    #region UIEffect

    public void InitColor(EffectColor.EffectColorType type,float time =0)
    {
        GameObject obj = GameObject.Instantiate(pfColor, tfUIEffect);
        EffectColor effectColor = obj.GetComponent<EffectColor>();
        effectColor.Init(type,time);
    }

    public void InitTransitionIcon(TransitionIconType type)
    {
        GameObject obj = GameObject.Instantiate(pfTransitionIcon, tfUIEffect);
        EffectTransitionIcon effectIcon = obj.GetComponent<EffectTransitionIcon>();
        effectIcon.Init(type);
    }

    public void InitEye()
    {
        GameObject obj = GameObject.Instantiate(pfCloseEye, tfUIEffect);
        EffectEyeClose effectEye = obj.GetComponent<EffectEyeClose>();
        effectEye.Init();
    }
    #endregion

    #region Warning
    private void InitWarning()
    {
        GameObject obj = GameObject.Instantiate(pfWarning, tfWarning);
        EffectWarning efWarning = obj.GetComponent<EffectWarning>();
        efWarning.Init();
    }
    #endregion

    #region Chapter
    public void InitChapter(int ID)
    {
        StartCoroutine(IE_Chapter(ID));   
    }

    private IEnumerator IE_Chapter(int ID)
    {
        InitColor(EffectColor.EffectColorType.WhiteChapter);
        yield return new WaitForSeconds(0.2F);
        GameObject obj = GameObject.Instantiate(pfChapter, tfUIEffect);
        EffectChapter itemChapter = obj.GetComponent<EffectChapter>();
        itemChapter.Init(ID);
    }
    #endregion
}
