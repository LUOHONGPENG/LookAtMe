using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSecondIns : LevelBasic
{
    [Header("Like")]
    public Image imgLike;
    public Button btnLike;
    public Text codeLike;
    [Header("Scroll")]
    public ScrollRect scrollRect;
    public ItemDragScroll scrollDragCheck;
    public VerticalLayoutGroup layout;
    public RectTransform rtContent;
    public Image imgRrefresh;
    public List<Sprite> listSpRefresh;
    private bool isRreshDrag = false;
    private bool isRreshRequired = false;
    [Header("OtherContent")]
    public Image imgPicB;
    public GameObject objGapB;


    public LevelRound currentRound = LevelRound.Like;

    public enum LevelRound
    {
        Like,
        Scroll1,
        Scroll2
    }

    private void Update()
    {
        CheckScroll();
    }

    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        btnLike.onClick.RemoveAllListeners();
        btnLike.onClick.AddListener(delegate ()
        {
            if (currentRound == LevelRound.Like)
            {
                StartCoroutine(IE_ClickLike());
            }
        });

        currentRound = LevelRound.Like;
        InitRound();
    }

    #region Action
    private IEnumerator IE_ClickLike()
    {
        btnLike.interactable = false;
        imgLike.DOFade(1f, 0.5f);
        imgLike.transform.DOScale(2f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        codeLike.text = 1.ToString();
        imgLike.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(IE_EndRound());
    }

    private void CheckScroll()
    {
        if (scrollDragCheck.isDrag && rtContent.anchoredPosition.y < -100)
        {
            isRreshDrag = false;
        }


    }

    private IEnumerator IE_FirstScroll()
    {

        yield break;
    }
    #endregion



    #region Flowcontrol
    private void InitRound()
    {
        switch (currentRound)
        {
            case LevelRound.Like:
                scrollRect.vertical = false;
                layout.padding = new RectOffset(0, 0, -126, 0);
                imgLike.DOFade(0, 0);
                btnLike.interactable = true;
                imgPicB.gameObject.SetActive(false);
                codeLike.text = 0.ToString();
                break;
            case LevelRound.Scroll1:
                scrollRect.vertical = true;
                break;
            case LevelRound.Scroll2:

                break;
        }
    }

    private IEnumerator IE_EndRound()
    {
        yield return new WaitForSeconds(1f);
        currentRound++;
        InitRound();
        yield break;
    }

    #endregion
}
