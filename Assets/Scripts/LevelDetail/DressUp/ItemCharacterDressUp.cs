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
    }

    //character wares the dress
    public override void DropDeal(PointerEventData eventData)
    {
        StartCoroutine(parent.IE_DragGoalFinish());
    }

    public void ChangeClothes(DressType dressType)
    {
        switch (dressType)
        {
            case DressType.Duck:
                imgCharacter.sprite = listSpCharacter[1];
                break;
            case DressType.Flower:
                imgCharacter.sprite = listSpCharacter[2];
                break;
        }
        imgCharacter.SetNativeSize();
    }
}
