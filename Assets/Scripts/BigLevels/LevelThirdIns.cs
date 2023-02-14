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

    private void Update()
    {
        CheckScroll();
    }

    #region Init
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        //UI Init
        layout.padding = new RectOffset(0, 0, GameGlobal.constSI_paddingTop, GameGlobal.constSI_paddingBottom);
        scrollRect.vertical = false;
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
        PublicTool.ClearChildItem(tfContentPhoto);
        InitScroll();
    }

    //Make the content can be scrolled
    public void InitScroll()
    {
        canvasGroupIns.blocksRaycasts = true;
        scrollRect.vertical = true;
        isRefreshDone = false;
    }
    #endregion

    #region ScrollAction

    private void CheckScroll()
    {
        if (scrollDragCheck.isDrag && rtContent.anchoredPosition.y < -100 && !isRefreshDone)
        {
            if (rtContent.anchoredPosition.y < -128f)
            {
                rtContent.anchoredPosition = new Vector2(0, -128F);
            }

            isRefreshRequired = true;
        }
        else if (scrollDragCheck.isEndDrag && isRefreshRequired)
        {
            isRefreshRequired = false;
            isRefreshDone = true;
            StartCoroutine(IE_Scroll());
        }
    }

    private IEnumerator IE_Scroll()
    {
        canvasGroupIns.blocksRaycasts = false;
        layout.padding = new RectOffset(0, 0, 0, GameGlobal.constSI_paddingBottom);
        rtContent.anchoredPosition = Vector2.zero;
        imgRrefresh.sprite = listSpRefresh[1];
        yield return new WaitForSeconds(1.5f);
        layout.padding = new RectOffset(0, 0, GameGlobal.constSI_paddingTop, GameGlobal.constSI_paddingBottom);
        imgRrefresh.sprite = listSpRefresh[0];
        yield return new WaitForSeconds(0.5f);
        GenerateComment();
        yield return new WaitForSeconds(2f);
        NextLevel();
    }

    #endregion

    #region Comment
    public void GenerateComment()
    {
        for (int i = 0; i < 5; i++)
        {
            float ranTime = Random.Range(0, 1f);
            GameObject objComment = GameObject.Instantiate(pfComment, tfContentComment);
            objComment.transform.localPosition = listPosComment[i];
            ItemInsComment itemComment = objComment.GetComponent<ItemInsComment>();
            itemComment.InitImage(i, false);
            itemComment.InitAni(ranTime);
        }
    }
    #endregion
}

