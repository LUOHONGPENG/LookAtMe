using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCharacterParty : MonoBehaviour
{
    public Image imgCharacter;
    public List<Sprite> listSpCharacter00 = new List<Sprite>();
    public List<Sprite> listSpCharacter01 = new List<Sprite>();
    public List<Sprite> listSpCharacter02 = new List<Sprite>();
    private int poseID = 0;
    private DressType dressType = DressType.Black;

    List<List<Sprite>> listPose = new List<List<Sprite>>();

    [Header("Bar")]
    public Image imgBar;
    public List<Sprite> listSpBar = new List<Sprite>();

    public void Init(DressType dressType)
    {
        poseID = 0;
        this.dressType = dressType;

        listPose.Add(listSpCharacter00);
        listPose.Add(listSpCharacter01);
        listPose.Add(listSpCharacter02);

        UpdatePose();
    }

    public void UpdatePose()
    {
        poseID++;
        if (poseID > 2)
        {
            poseID = 0;
        }

        imgCharacter.sprite = listPose[poseID][(int)dressType];
        imgCharacter.SetNativeSize();
    }

    public void SetBar(int count)
    {
        if (count >= 0 && count <= 4)
        {
            imgBar.sprite = listSpBar[count];
            imgBar.SetNativeSize();
        }
    }

}
