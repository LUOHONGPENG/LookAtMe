using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public StartPageManager startPageManager;

    public void Init()
    {
        startPageManager.Init();
    }
}
