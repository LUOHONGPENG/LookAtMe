using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDiagnosis : LevelBasic
{
    public Animator aniHospital;
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        aniHospital.Play("Hospital");

        StartCoroutine(IE_Ani());
    }

    public IEnumerator IE_Ani()
    {
        yield return new WaitForSeconds(1.9f);
        GameManager.Instance.effectManager.InitEye();

        yield return new WaitForSeconds(3f);
        GameManager.Instance.effectManager.InitEye();
    }
}
