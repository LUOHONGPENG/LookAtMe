using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum DressType
{
    dress1,
    dress2,
    None
}
public class LevelDressUp : LevelBasic
{
    public DressType currentType;
    public GameObject objCharacter;
    public List<Sprite> newCharacter;
    public List<DragDress> listDragDress = new List<DragDress>();
    public DressSlot itemDressSlot;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        itemDressSlot.Init(this);
        InitialDresses();
    }

    
    public void InitialDresses()
    {
        listDragDress[0].Init(DressType.dress1,this);
        listDragDress[1].Init(DressType.dress2, this);
    }
 public void ChangeCharacterDress()
    {
       //change the character image here according to the input from DragDress;
       if(currentType == DressType.dress1)
        {
            print("dress1");
            //character's spriet change to newCharacter[0]
        }else if (currentType == DressType.dress2)
        {
            print("dress1");
            //character's spriet change to newCharacter[1]
        }
        else { }

    }



    public void SetCurrentDress(DressType type)
    {
        currentType = type;
    }

    public IEnumerator IE_DragGoalFinish()
    {
        //Change to next level after 2 seconds
        yield return new WaitForSeconds(2f);
        NextLevel();
    }
}
