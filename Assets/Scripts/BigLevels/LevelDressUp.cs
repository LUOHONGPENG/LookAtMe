using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum DressType
{
    Duck,
    Flower,
    None
}
public class LevelDressUp : LevelBasic
{
    public DressType currentType;
    [Header("Clothes")]
    public GameObject pfDress;
    public Transform tfContentDress;
    public List<Vector2> listPosDress = new List<Vector2>();
    private List<DragDress> listDragDress = new List<DragDress>();
    [Header("Character")]
    public ItemCharacterDressUp itemCharacter;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        itemCharacter.Init(this);
        InitDress();
    }

    
    public void InitDress()
    {
        //Init Dress
        listDragDress.Clear();
        PublicTool.ClearChildItem(tfContentDress);
        for(int i = 0; i < 2; i++)
        {
            GameObject objDress = GameObject.Instantiate(pfDress, tfContentDress);
            DragDress itemDress = objDress.GetComponent<DragDress>();
            itemDress.InitPosition(listPosDress[i]);
            listDragDress.Add(itemDress);
        }
        listDragDress[0].Init(DressType.Duck, this);
        listDragDress[1].Init(DressType.Flower, this);
    }


    public void SetCurrentDragging(DressType type)
    {
        currentType = type;
    }

    public override void ReleaseDragging()
    {
        currentType = DressType.None;
    }

    public IEnumerator IE_DragGoalFinish()
    {
        //Change to next level after 2 seconds
        yield return new WaitForSeconds(2f);
        NextLevel();
    }
}
