using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelThirdIns : LevelBasic
{
    public CanvasGroup canvasGroupIns;
    [Header("Photo")]
    public GameObject pfPhoto;
    public Transform tfContentPhoto;
    private ItemInsPhoto itemPhoto;
    public Image imgInsPhoto;

    [Header("Scroll")]
    public ScrollRect scrollRect;
    public ItemDragScroll scrollDragCheck;
    public VerticalLayoutGroup layout;
    public RectTransform rtContent;
    public Image imgRrefresh;
    public List<Sprite> listSpRefresh;
    //Check Whether Need Refresh
    private bool isRefreshDone = false;
    private bool isRefreshRequired = false;
    [Header("Comment")]
    public GameObject pfComment;
    public Transform tfContentComment;
    public List<Vector2> listPosComment = new List<Vector2>();

    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        //UI Init
        layout.padding = new RectOffset(0, 0, GameGlobal.constSI_paddingTop, GameGlobal.constSI_paddingBottom);
        canvasGroupIns.alpha = 0;
        //Ban interaction
        canvasGroupIns.blocksRaycasts = false;

        InitPhoto();
        StartCoroutine(IE_InitAni());
    }

    public void InitPhoto()
    {
        GameObject objShoot = GameObject.Instantiate(pfPhoto, tfContentPhoto);
        itemPhoto = objShoot.GetComponent<ItemInsPhoto>();
        itemPhoto.Init(this, PhotoType.Display, 20.2f, 105.7f);


        imgInsPhoto.sprite = GameManager.Instance.levelManager.spLastShoot;
        imgInsPhoto.transform.localPosition = GameManager.Instance.levelManager.posLastShoot;
        imgInsPhoto.SetNativeSize();
        imgInsPhoto.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -10F));
    }

    public IEnumerator IE_InitAni()
    {
        canvasGroupIns.DOFade(1f, 0.5f);
        itemPhoto.canvasGroupPhoto.DOFade(0, 0.5f);
        yield return new WaitForSeconds(1F);
        //PublicTool.ClearChildItem(tfContentPhoto);
    }
}
