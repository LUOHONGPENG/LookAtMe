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
    [Header("Dressing")]
    public CanvasGroup canvasGroupDress;
    public GameObject pfDress;
    public Transform tfContentDress;
    public List<Vector2> listPosDress = new List<Vector2>();
    private List<DragDress> listDragDress = new List<DragDress>();
    [Header("Character")]
    public ItemCharacterDressUp itemCharacter;
    [Header("Photo")]
    public GameObject pfPhoto;
    public Transform tfContentPhoto;
    private ItemInsPhoto itemPhoto;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        canvasGroupDress.blocksRaycasts = true;
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

    #region Drag

    public IEnumerator IE_DragGoalFinish()
    {
        //Ban all interaction about dressing
        canvasGroupDress.blocksRaycasts = false;
        //ChangeTheClothe
        itemCharacter.ChangeClothes(currentType);
        //HideTheClothe
        switch (currentType)
        {
            case DressType.Duck:
                listDragDress[0].SuddenHide();
                break;
            case DressType.Flower:
                listDragDress[1].SuddenHide();
                break;
        }
        //Save the type
        GameManager.Instance.levelManager.savedDressType = currentType;
        //Change to next level after 2 seconds
        yield return new WaitForSeconds(2f);
        InitShootPhoto();
        yield break;
    }

    public void SetCurrentDragging(DressType type)
    {
        currentType = type;
    }

    public override void ReleaseDragging()
    {
        currentType = DressType.None;
    }
    #endregion

    #region ShootPhoto

    public void InitShootPhoto()
    {
        GameObject objShoot = GameObject.Instantiate(pfPhoto, tfContentPhoto);
        itemPhoto = objShoot.GetComponent<ItemInsPhoto>();
        itemPhoto.Init(this, false);
    }

    public override void AfterShoot()
    {
        StartCoroutine(IE_AfterShoot());
    }

    public IEnumerator IE_AfterShoot()
    {
        yield return new WaitForSeconds(2f);
        itemPhoto.MoveTo(new Vector2(1200F, -800F));
        yield return new WaitForSeconds(2f);
        NextLevel();
    }

    #endregion
}
