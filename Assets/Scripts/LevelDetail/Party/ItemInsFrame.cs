using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInsFrame : MonoBehaviour
{
    public Button btnShoot;
    public RectTransform rtShoot;

    private bool isInit = false;

    public void Init()
    {
        isInit = true;
    }

    void Update()
    {
        if (isInit)
        {
            this.transform.position = PublicTool.GetMousePosition2D();
        }
    }
}
