using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherThoughtDebate : MonoBehaviour
{

    //0=square 1= circle 2=traingle
    public List<Sprite> listSpShape;
    public Image imgContent;

    public void Init()
    {
        imgContent.gameObject.SetActive(false);
    }

    public void ShowContent()
    {

    }
}
