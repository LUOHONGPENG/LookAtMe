using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBasic : MonoBehaviour
{
    private LevelManager parent;
    public Button btnCommonFullClick;

    public virtual void Init(LevelManager parent)
    {
        this.parent = parent;
        Debug.Log(11);


    }

    public virtual void NextLevel()
    {
        parent.NextLevel();
    }



}
