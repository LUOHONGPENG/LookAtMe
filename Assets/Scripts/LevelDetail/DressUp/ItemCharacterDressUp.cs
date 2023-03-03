using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCharacterDressUp : CommonImageDragSlot
{
    public Image imgCharacter;
    public List<Sprite> listSpCharacter = new List<Sprite>();

    private LevelDressUp parent;

    public void Init(LevelDressUp parent)
    {
        this.parent = parent;
        imgCharacter.sprite = listSpCharacter[0];
        imgCharacter.SetNativeSize();
    }

    //character wares the dress
    public override void DropDeal(PointerEventData eventData)
    {
        parent.DragGoalFinish();
    }

    public void ChangeClothes(DressType dressType)
    {
        switch (dressType)
        {
            case DressType.Red:
                imgCharacter.sprite = listSpCharacter[1];
                break;
            case DressType.Black:
                imgCharacter.sprite = listSpCharacter[2];
                break;
            case DressType.Blue:
                imgCharacter.sprite = listSpCharacter[3];
                break;
            case DressType.Flower:
                imgCharacter.sprite = listSpCharacter[4];
                break;
        }
        imgCharacter.SetNativeSize();
    }
}
