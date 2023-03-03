using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectChapter : MonoBehaviour
{
    public Animator aniChapter;

    public void Init(int ID)
    {
        switch (ID)
        {
            case 1:
                aniChapter.Play("Chapter1");
                break;
            case 2:
                aniChapter.Play("Chapter2");
                break;
            case 3:
                aniChapter.Play("Chapter3");
                break;
        }

        Destroy(this.gameObject, 4f);
    }

    

}
