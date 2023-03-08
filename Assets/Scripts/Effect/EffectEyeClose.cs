using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEyeClose : MonoBehaviour
{
    public Animator aniEyeClose;

    public void Init()
    {
        aniEyeClose.Play("EyeClose");
        //aniEyeClose.Play("EyeClose", 0, -1);
        Destroy(this.gameObject, 2.1f);
    }
}
