using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColDetect : MonoBehaviour
{
    public Collider2D colDetect;
    public bool isTouched = false;

    public void Init()
    {
        isTouched = false;
    }
}
