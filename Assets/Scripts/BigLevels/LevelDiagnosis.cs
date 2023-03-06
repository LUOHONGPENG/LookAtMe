using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDiagnosis : LevelBasic
{
    public Animator aniHospital;
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        aniHospital.Play("Hospital", 0, -1);
    }

}
