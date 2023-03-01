using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LevelThirdIns : LevelBasic
{
    public CanvasGroup canvasGroupIns;
    public Image imgBg;
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

    //PostProcess
    private DepthOfField efBlur;
    private bool isInitBlur = false;
    private float timerBlur = 0;

    private void Update()
    {
        CheckScroll();
        CheckBlurEffect();
    }

    #region Init
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        //UI Init
        layout.padding = new RectOffset(0, 0, GameGlobal.constSI_paddingTop, GameGlobal.constSI_paddingBottom);
        scrollRect.vertical = false;
        canvasGroupIns.alpha = 0;
        imgBg.DOFade(0, 0);
        //Ban interaction
        canvasGroupIns.blocksRaycasts = false;

        InitPhoto();
        StartCoroutine(IE_InitAni());
    }

    public void InitPhoto()
    {
        GameObject objShoot = GameObject.Instantiate(pfPhoto, tfContentPhoto);
        itemPhoto = objShoot.GetComponent<ItemInsPhoto>();
        itemPhoto.Init(this, PhotoType.Display, GameGlobal.posTI_photoToInsX, GameGlobal.posTI_photoToInsY);

        imgInsPhoto.sprite = GameManager.Instance.levelManager.spLastShoot;
        imgInsPhoto.transform.localPosition = GameManager.Instance.levelManager.posLastShoot;
        imgInsPhoto.SetNativeSize();
        //imgInsPhoto.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -10F));
    }

    public IEnumerator IE_InitAni()
    {
        imgBg.DOFade(1f, 0.5f);
        itemPhoto.imgFrame.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5F);
        canvasGroupIns.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(0.5F);
        itemPhoto.canvasGroupPhoto.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5F);
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
        InitBlur();
        yield return new WaitForSeconds(5f);
        isInitBlur = false;
        yield return new WaitForSeconds(1f);
        GameManager.Instance.effectManager.ClearPostProcess();
        NextLevel();
    }

    #endregion

    #region Comment
    private void GenerateComment()
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

    #region Blur

    private void InitBlur()
    {
        GameObject objBlur = PublicTool.PostProcessEffect(PostProcessType.ThirdInsBlur);
        Volume volume = objBlur.GetComponent<Volume>();
        DepthOfField tmp;
        if (volume.profile.TryGet<DepthOfField>(out tmp))
        {
            efBlur = tmp;
            timerBlur = 0;
            efBlur.focalLength.value = 0f;            
            isInitBlur = true;
        }
    }

    private void CheckBlurEffect()
    {
        if (isInitBlur)
        {
            timerBlur += Time.deltaTime;
            efBlur.focalLength.value = 0f + timerBlur * 40f;
        }
    }
    #endregion
}

