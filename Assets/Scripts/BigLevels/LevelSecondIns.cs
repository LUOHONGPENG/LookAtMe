using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSecondIns : LevelBasic
{
    public CanvasGroup canvasGroupAll;
    public Transform tfBg;
    [Header("Photo")]
    public GameObject pfPhoto;
    public Transform tfContentPhoto;
    private ItemInsPhoto itemPhoto;
    public Image imgInsPhoto;
    public Image imgBlack;
    [Header("Like")]
    public Image imgLike;
    public Button btnLike;
    public Text codeLikeMe;
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
    [Header("OtherContent")]
    public CanvasGroup groupTip;
    public Image imgEmoji;
    public Image imgPicB;
    public GameObject objGapB;
    public Text codeLikeOther;
    private int numLikeOther = 0;
    private int numFreeRound = 0;

    public LevelRound currentRound = LevelRound.Like;

    public enum LevelRound
    {
        Like,
        FirstScroll,
        SecondScroll,
        FreeScroll
    }

    private void Update()
    {
        CheckScroll();
    }

    #region Init
    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        PublicTool.PlayMusic(MusicType.InsSad);

        //Init Alpha
        imgBlack.DOFade(1, 0);
        groupTip.DOFade(0, 0);
        imgEmoji.transform.DOScale(0, 0);

        btnLike.onClick.RemoveAllListeners();
        btnLike.onClick.AddListener(delegate ()
        {
            if (currentRound == LevelRound.Like)
            {
                StartCoroutine(IE_ClickLike());
                btnLike.interactable = false;
                imgLike.raycastTarget = true;
            }
        });

        //UI Init
        layout.padding = new RectOffset(0, 0, GameGlobal.constSI_paddingTop, GameGlobal.constSI_paddingBottom);
        imgLike.DOFade(0, 0);
        imgLike.raycastTarget = false;
        btnLike.interactable = true;
        imgPicB.gameObject.SetActive(false);
        objGapB.SetActive(false);
        //Like Init
        codeLikeMe.text = 0.ToString();
        numLikeOther = 2;
        numFreeRound = 0;
        codeLikeOther.text = numLikeOther.ToString();
        //Ban interaction
        canvasGroupAll.blocksRaycasts = false;

        currentRound = LevelRound.Like;
        
        InitPhoto();
        StartCoroutine(IE_InitAni());
    }

    public void InitPhoto()
    {
        imgBlack.DOFade(1f,0);

        GameObject objShoot = GameObject.Instantiate(pfPhoto, tfContentPhoto);
        itemPhoto = objShoot.GetComponent<ItemInsPhoto>();
        itemPhoto.Init(this, PhotoType.Display,1200f,-800f);

        imgInsPhoto.sprite = GameManager.Instance.levelManager.spLastShoot;
        imgInsPhoto.transform.localPosition = GameManager.Instance.levelManager.posLastShoot;
        imgInsPhoto.SetNativeSize();
        //imgInsPhoto.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -10F));

    }

    public IEnumerator IE_InitAni()
    {
        itemPhoto.imgFrame.DOFade(0, 0.5F);
        itemPhoto.MoveTo(new Vector2(GameGlobal.posSI_photoToInsX, GameGlobal.posSI_photoToInsY), 0.5F);
        yield return new WaitForSeconds(0.8F);
        imgBlack.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5F);
        itemPhoto.canvasGroupPhoto.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5F);
        canvasGroupAll.blocksRaycasts = true;
        PublicTool.ClearChildItem(tfContentPhoto);
        InitRound();
    }


    #endregion

    #region Action
    private IEnumerator IE_ClickLike()
    {
        imgLike.DOFade(1f, 0.5f);
        imgLike.transform.DOScale(2f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        codeLikeMe.text = 1.ToString();
        imgLike.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(IE_EndRound());
    }

    private void CheckScroll()
    {
        if (scrollDragCheck.isDrag && rtContent.anchoredPosition.y < -100 && !isRefreshDone)
        {
            if (rtContent.anchoredPosition.y < GameGlobal.constSI_paddingTop)
            {
                rtContent.anchoredPosition = new Vector2(0, GameGlobal.constSI_paddingTop);
            }

            isRefreshRequired = true;
        }
        else if(scrollDragCheck.isEndDrag && isRefreshRequired )
        {
            isRefreshRequired = false;
            isRefreshDone = true;
            HideTip();
            if (currentRound == LevelRound.FirstScroll)
            {
                StartCoroutine(IE_FirstScroll());
            }
            else if (currentRound == LevelRound.SecondScroll)
            {
                StartCoroutine(IE_SecondScroll());
            }
            else if (currentRound == LevelRound.FreeScroll)
            {
                StartCoroutine(IE_FreeScroll());
            }
        }
    }

    private IEnumerator IE_FirstScroll()
    {
        canvasGroupAll.blocksRaycasts = false;
        layout.padding = new RectOffset(0, 0, 0, GameGlobal.constSI_paddingBottom);
        rtContent.anchoredPosition = Vector2.zero;
        imgRrefresh.sprite = listSpRefresh[1];
        yield return new WaitForSeconds(1.5f);
        layout.padding = new RectOffset(0, 0, GameGlobal.constSI_paddingTop, GameGlobal.constSI_paddingBottom);
        imgRrefresh.sprite = listSpRefresh[0];
        codeLikeMe.text = 2.ToString();
        canvasGroupAll.blocksRaycasts = true;
        yield return StartCoroutine(IE_EndRound());
    }

    private IEnumerator IE_SecondScroll()
    {
        canvasGroupAll.blocksRaycasts = false;
        layout.padding = new RectOffset(0, 0, 0, GameGlobal.constSI_paddingBottom);
        rtContent.anchoredPosition = Vector2.zero;
        imgRrefresh.sprite = listSpRefresh[1];
        yield return new WaitForSeconds(1.5f);
        layout.padding = new RectOffset(0, 0, GameGlobal.constSI_paddingTop, GameGlobal.constSI_paddingBottom);
        imgRrefresh.sprite = listSpRefresh[0];
        imgPicB.gameObject.SetActive(true);
        objGapB.SetActive(true);


        int ranNum = Random.Range(20, 40);

        float timerDelta = GameGlobal.timerSI_freeScrollNumGrow / 4F;
        yield return StartCoroutine(IE_LikeLoop(ranNum, timerDelta / ranNum));
        yield return StartCoroutine(IE_LikeLoop(5 * ranNum, 2 * timerDelta / (ranNum * 5)));
        yield return StartCoroutine(IE_LikeLoop(ranNum, timerDelta / ranNum));

        canvasGroupAll.blocksRaycasts = true;
        yield return StartCoroutine(IE_EndRound());
    }

    private IEnumerator IE_FreeScroll()
    {
        canvasGroupAll.blocksRaycasts = false;
        layout.padding = new RectOffset(0, 0, 0, GameGlobal.constSI_paddingBottom);
        rtContent.anchoredPosition = Vector2.zero;
        imgRrefresh.sprite = listSpRefresh[1];
        yield return new WaitForSeconds(1.5f);
        layout.padding = new RectOffset(0, 0, GameGlobal.constSI_paddingTop, GameGlobal.constSI_paddingBottom);
        imgRrefresh.sprite = listSpRefresh[0];

        int ranNum = Random.Range(20, 40);

        float timerDelta = GameGlobal.timerSI_freeScrollNumGrow / 4F;
        yield return StartCoroutine(IE_LikeLoop(ranNum, timerDelta / ranNum));
        yield return StartCoroutine(IE_LikeLoop(5 * ranNum, 2* timerDelta / (ranNum*5)));
        yield return StartCoroutine(IE_LikeLoop(ranNum, timerDelta / ranNum));

        numFreeRound++;
        if (numFreeRound >= GameGlobal.countSI_freeScroll)
        {
            yield return StartCoroutine(IE_EndRound());
        }
        else
        {
            canvasGroupAll.blocksRaycasts = true;
            isRefreshDone = false;
            ShowTip();
        }
    }

    public IEnumerator IE_LikeLoop(int num, float time)
    {
        for (int i = 0; i < num; i++)
        {
            int ranNum = Random.Range(1, 3);
            numLikeOther += ranNum;
            codeLikeOther.text = numLikeOther.ToString();
            yield return new WaitForSeconds(time);
        }
    }
    #endregion

    #region
    public void ShowTip()
    {
        groupTip.DOFade(0.75f, 0.5f);
    }

    public void HideTip()
    {
        groupTip.DOFade(0, 0.5f);
    }

    #endregion

    #region Flowcontrol

    private void InitRound()
    {
        switch (currentRound)
        {
            case LevelRound.Like:
                isRefreshDone = true;
                scrollRect.vertical = false;
                break;
            case LevelRound.FirstScroll:
                scrollRect.vertical = true;
                isRefreshDone = false;
                ShowTip();
                break;
            case LevelRound.SecondScroll:
                isRefreshDone = false;
                ShowTip();
                break;
            case LevelRound.FreeScroll:
                isRefreshDone = false;
                ShowTip();
                break;
        }
    }

    private IEnumerator IE_EndRound()
    {
        switch (currentRound)
        {
            case LevelRound.SecondScroll:
            case LevelRound.FreeScroll:
                StartCoroutine(IE_ShowEmoji());
                break;
        }
        yield return new WaitForSeconds(1f);
        if (currentRound == LevelRound.FreeScroll)
        {
            yield return StartCoroutine(IE_ScrollBack());
            StartCoroutine(GameManager.Instance.videoManager.InitVideoSad());
            yield return new WaitForSeconds(3.2f);
            PublicTool.TransitionChapter(3);
            PublicTool.PlayChapterMusic(ChapterMusicType.Chapter3);
            PublicTool.StopMusic(true);
            yield return new WaitForSeconds(1.5f);
            NextLevel();
            yield break;//Similar to return in function
        }
        currentRound++;
        InitRound();
        yield break;
    }

    private IEnumerator IE_ScrollBack()
    {
        DOTween.To(() => { return rtContent.anchoredPosition; }, v => { rtContent.anchoredPosition = v; }, new Vector2(0, 582f), 1.5f);
        yield return new WaitForSeconds(1.5f);

        tfBg.DOLocalMove(new Vector2(254f, 598f),0.3f);
        tfBg.DOScale(3f, 0.3f);
        yield return new WaitForSeconds(1f);

/*        tfBg.DOLocalMove(new Vector2(448, 1006f), 0.3f);
        tfBg.DOScale(5f, 0.3f);
        yield return new WaitForSeconds(1f);*/

        tfBg.DOLocalMove(new Vector2(636f, 1411f), 0.3f);
        tfBg.DOScale(7f, 0.3f);
        yield return new WaitForSeconds(1f);

/*        tfBg.DOLocalMove(new Vector2(807f, 1813f), 0.3f);
        tfBg.DOScale(9f, 0.3f);
        yield return new WaitForSeconds(1f);*/

        tfBg.DOLocalMove(new Vector2(992f, 2215f), 0.3f);
        tfBg.DOScale(11f, 0.3f);
        yield return new WaitForSeconds(2f);

    }



    #endregion

    #region Emoji
    public IEnumerator IE_ShowEmoji()
    {
        imgEmoji.transform.DOScale(1f, 1f);
        yield return new WaitForSeconds(3f);
        imgEmoji.transform.DOScale(0, 1f);
    }
    #endregion
}
