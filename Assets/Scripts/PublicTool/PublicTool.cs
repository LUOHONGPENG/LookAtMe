using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicTool : MonoBehaviour
{
    public static void ClearChildItem(UnityEngine.Transform tf)
    {
        foreach (UnityEngine.Transform item in tf)
        {
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }


}
