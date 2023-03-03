using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum DressType
{
    Red,
    Black,
    Blue,
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
    public GameObject pfCharacter;
    public Transform tfCharacter;
    private ItemCharacterDressUp itemCharacter;
    [Header("Photo")]
    public Button btnShoot;
    public CommonHoverUI hoverBtnShoot;
    public Image imgBtnShoot;
    public List<Sprite> listSpShoot;
    public GameObject pfPhoto;
    public Transform tfContentPhoto;
    private ItemInsPhoto itemPhoto;
    private bool isShowButton;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        canvasGroupDress.blocksRaycasts = true;
        imgBtnShoot.sprite = listSpShoot[0];
        btnShoot.interactable = false;
        btnShoot.transform.DOScale(0, 0);
        btnShoot.onClick.RemoveAllListeners();
        btnShoot.onClick.AddListener(delegate ()
        {
            canvasGroupDress.blocksRaycasts = false;
            btnShoot.interactable = false;
            hoverBtnShoot.isEnabled = false;
            imgBtnShoot.sprite = listSpShoot[1];
            //btnShoot.transform.DOScale(0, 0.5f);
            StartCoroutine(InitShootPhoto());

            PublicTool.PlaySound(SoundType.Click);
        });
        isShowButton = false;
        InitCharacter();
        InitDress();
    }

    public void InitCharacter()
    {
        GameObject objCharacter = GameObject.Instantiate(pfCharacter, tfCharacter);
        itemCharacter = objCharacter.GetComponent<ItemCharacterDressUp>();
        itemCharacter.Init(this);
    }
        
    public void InitDress()
    {
        //Init Dress
        listDragDress.Clear();
        PublicTool.ClearChildItem(tfContentDress);
        for(int i = 0; i < 4; i++)
        {
            GameObject objDress = GameObject.Instantiate(pfDress, tfContentDress);
            DragDress itemDress = objDress.GetComponent<DragDress>();
            itemDress.InitPosition(listPosDress[i]);
            listDragDress.Add(itemDress);
        }
        listDragDress[0].Init(DressType.Red, this);
        listDragDress[1].Init(DressType.Black, this);
        listDragDress[2].Init(DressType.Blue, this);
        listDragDress[3].Init(DressType.Flower, this);

    }

    #region Drag

    public void DragGoalFinish()
    {
        //Ban all interaction about dressing
        //ChangeTheClothe
        itemCharacter.ChangeClothes(currentType);
        //HideTheClothe
        for(int i = 0; i < 4; i++)
        {
            if(listDragDress[i].dressType == currentType)
            {
                listDragDress[i].SuddenHide();
            }
            else
            {
                listDragDress[i].SuddenShow();
            }
        }
        GameManager.Instance.levelManager.savedDressType = currentType;
        if (!isShowButton)
        {
            StartCoroutine(IE_ShowButton());
            isShowButton = true;
        }
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
        itemPhoto.Init(this, PhotoType.Manual);

/*        itemPhoto.Init(this, PhotoType.Auto);
        itemPhoto.MoveTo(new Vector2(1200F, -800F), 0f);
        yield return new WaitForEndOfFrame();
        itemPhoto.MoveTo(new Vector2(-100F, -80F), 0.5f);
        yield return new WaitForSeconds(0.5F);
        itemPhoto.MoveTo(new Vector2(-280F, 320F), 1f);
        yield return new WaitForSeconds(1F);
        itemPhoto.ShootExecute();*/
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
