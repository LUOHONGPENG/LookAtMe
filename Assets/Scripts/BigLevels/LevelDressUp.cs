using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    public Button btnShoot;
    public GameObject pfPhoto;
    public Transform tfContentPhoto;
    private ItemInsPhoto itemPhoto;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        canvasGroupDress.blocksRaycasts = true;
        itemCharacter.Init(this);
        btnShoot.interactable = false;
        btnShoot.transform.DOScale(0, 0);
        btnShoot.onClick.RemoveAllListeners();
        btnShoot.onClick.AddListener(delegate ()
        {
            btnShoot.interactable = false;
            btnShoot.transform.DOScale(0, 0.5f);
            StartCoroutine(InitShootPhoto());
        });
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
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(IE_ShowButton());
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

    public IEnumerator IE_ShowButton()
    {
        btnShoot.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        btnShoot.interactable = true;
    }

    public IEnumerator InitShootPhoto()
    {
        GameObject objShoot = GameObject.Instantiate(pfPhoto, tfContentPhoto);
        itemPhoto = objShoot.GetComponent<ItemInsPhoto>();
        itemPhoto.Init(this, PhotoType.Auto);
        itemPhoto.MoveTo(new Vector2(1200F, -800F), 0f);
        yield return new WaitForEndOfFrame();
        itemPhoto.MoveTo(new Vector2(80F, -80F), 0.5f);
        yield return new WaitForSeconds(0.5F);
        itemPhoto.MoveTo(new Vector2(-100F, 150F), 1f);
        yield return new WaitForSeconds(1F);
        itemPhoto.ShootExecute();
        yield break;
    }

    public override void AfterShoot()
    {
        StartCoroutine(IE_AfterShoot());
    }

    public IEnumerator IE_AfterShoot()
    {
        yield return new WaitForSeconds(2f);
        itemPhoto.MoveTo(new Vector2(1200F, -800F),0.5f);
        yield return new WaitForSeconds(1f);
        PublicTool.TransitionIconEffect(TransitionIconType.Cheer);
        yield return new WaitForSeconds(1f);
        NextLevel();
    }

    #endregion
}
