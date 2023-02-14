using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public Transform tfPostProcess;

    public GameObject efSPVignette;
    public GameObject efTIBlur;

    public void ClearContent()
    {
        PublicTool.ClearChildItem(tfPostProcess);
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
}
