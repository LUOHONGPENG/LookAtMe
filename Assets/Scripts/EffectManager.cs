using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [Header("PostProcess")]
    public Transform tfPostProcess;
    public GameObject efSPVignette;
    public GameObject efTIBlur;
    public GameObject efHBlur;
    [Header("UIEffect")]
    public Transform tfUIEffect;
    public GameObject pfWarning;
    public GameObject pfColor;
    public void Init()
    {
        InitWarning();
    }

    public void ClearContent()
    {
        PublicTool.ClearChildItem(tfPostProcess);
        PublicTool.ClearChildItem(tfUIEffect);
    }

    public GameObject InitSPVig()
    {
        GameObject obj = GameObject.Instantiate(efSPVignette, tfPostProcess);
        return obj;
    }

    public GameObject InitTIBlur()
    {
        GameObject obj = GameObject.Instantiate(efTIBlur, tfPostProcess);
        return obj;
    }
    public GameObject InitHBlur()
    {
        GameObject obj = GameObject.Instantiate(efHBlur, tfPostProcess);
        return obj;
    }

    public void InitWarning()
    {
        GameObject obj = GameObject.Instantiate(pfWarning, tfUIEffect);
        EffectWarning efWarning = obj.GetComponent<EffectWarning>();
        efWarning.Init();
    }

    public void InitColor(EffectColor.EffectColorType type)
    {
        GameObject obj = GameObject.Instantiate(pfColor, tfUIEffect);
        EffectColor effectColor = obj.GetComponent<EffectColor>();
        effectColor.Init(type);
    }
}
