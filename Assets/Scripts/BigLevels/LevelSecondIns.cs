using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSecondIns : LevelBasic
{
    public Image imgLike;
    public Button btnLike;
    public Text codeLike;
    public Image imgPicB;

    public LevelRound currentRound = LevelRound.Like;

    public enum LevelRound
    {
        Like,
        Drag1,
        Drag2
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

    public IEnumerator IE_ClickLike()
    {
        btnLike.interactable = false;

        imgLike.DOFade(1f, 0.5f);
        imgLike.transform.DOScale(2f, 0.5f);

        yield return new WaitForSeconds(0.5f);
        codeLike.text = 1.ToString();
        imgLike.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);

    }
    #endregion



    #region Flowcontrol
    public void InitRound()
    {
        switch (currentRound)
        {
            case LevelRound.Like:
                imgLike.DOFade(0, 0);
                btnLike.interactable = true;
                imgPicB.gameObject.SetActive(false);
                codeLike.text = 0.ToString();
                break;
            case LevelRound.Drag1:

                break;
            case LevelRound.Drag2:

                break;
        }
    }

    public IEnumerator IE_EndRound()
    {
        yield return new WaitForSeconds(3f);


        yield break;
    }

    #endregion
}
