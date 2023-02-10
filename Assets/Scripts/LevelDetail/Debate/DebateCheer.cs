using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebateCheer : MonoBehaviour
{
    public Animator aniCheer;

    public void InitAni()
    {
        aniCheer.SetFloat("Speed", 1f);
    }

    public void AniStop()
    {
        aniCheer.SetFloat("Speed", 0);
    }
}
