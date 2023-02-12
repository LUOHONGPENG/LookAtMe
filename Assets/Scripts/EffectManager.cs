using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public Transform tfPostProcess;

    public GameObject efSPVignette;

    public void ClearContent()
    {
        PublicTool.ClearChildItem(tfPostProcess);
    }

    public GameObject InitSPVig()
    {
        ClearContent();
        GameObject obj = GameObject.Instantiate(efSPVignette, tfPostProcess);
        return obj;
    }

}
