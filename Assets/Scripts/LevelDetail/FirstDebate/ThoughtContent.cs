using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtContent : MonoBehaviour
{
    //0=square 1= circle 2=traingle
    public List<Sprite> listSpShape;
    public Image imgContent;

    public void Init()
    {
        imgContent.gameObject.SetActive(false);
    }

    public void ShowContent(ThoughtType type)
    {
        //imgContent.sprite = listSpShape[(int)type]; also work
        switch (type)
        {
            case ThoughtType.Square:
                imgContent.sprite = listSpShape[0];
                break;
            case ThoughtType.Circle:
                imgContent.sprite = listSpShape[1];
                break;
            case ThoughtType.Triangle:
                imgContent.sprite = listSpShape[2];
                break;
        }
        imgContent.gameObject.SetActive(true);
    }
}
