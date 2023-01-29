using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSecondDebate : LevelBasic
{
    #region Declare Enum
    public enum InnerLevelState
    {
        Round1,
        Round2,
        BattleStart,
        YouWin
    }

    public InnerLevelState currentState = InnerLevelState.Round1;

    #endregion

    [Header("Round")]
    public GameObject objRound;
    public List<SpriteRenderer> listSRPeople = new List<SpriteRenderer>();
    public List<SpriteRenderer> listSRBubble = new List<SpriteRenderer>();
    public List<SpriteRenderer> listSRContent = new List<SpriteRenderer>();
    public List<Sprite> listSPContent = new List<Sprite>();

    [Header("Battle")]
    public GameObject objBattle;
    public SpriteRenderer srMe;
    public SpriteRenderer srEnemy;
    public ItemWeaponBubble itemWeaponBubble;

    //Initialize
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        currentState = InnerLevelState.Round1;
        ReadState();
    }

    public void ReadState()
    {
        switch (currentState)
        {
            case InnerLevelState.Round1:
                StartCoroutine(IE_Round1());
                break;
            case InnerLevelState.Round2:
                StartCoroutine(IE_Round2());
                break;
            case InnerLevelState.BattleStart:
                StartCoroutine(IE_Round2());

                break;
        }
    }

    public void NextState()
    {
        currentState++;
        ReadState();
    }

    #region Round1&2_Animation

    public IEnumerator IE_Round1()
    {
        //Initialization
        objRound.SetActive(false);
        objBattle.SetActive(false);
        foreach (SpriteRenderer sr in listSRBubble)
        {
            sr.transform.localScale = Vector3.zero;
        }
        foreach (SpriteRenderer sr in listSRPeople)
        {
            sr.transform.localScale = Vector3.zero;
        }
        foreach (SpriteRenderer sr in listSRContent)
        {
            sr.transform.localScale = Vector3.zero;
        }
        //Set the content of the bubbles
        listSRContent[0].sprite = listSPContent[1];
        listSRContent[1].sprite = listSPContent[0];
        listSRContent[2].sprite = listSPContent[0];
        listSRContent[3].sprite = listSPContent[0];
        objRound.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        //Show the people
        foreach (SpriteRenderer sr in listSRPeople)
        {
            sr.transform.DOScale(0.5f, 0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        //Show the bubbles
        foreach (SpriteRenderer sr in listSRBubble)
        {
            sr.transform.DOScale(1, 0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        //Show the content
        listSRContent[1].transform.DOScale(1, 0.5f);
        listSRContent[2].transform.DOScale(1, 0.5f);
        listSRContent[3].transform.DOScale(1, 0.5f);
        yield return new WaitForSeconds(2f);
        listSRContent[0].transform.DOScale(1, 0.5f);
        yield return new WaitForSeconds(2f);
        foreach (SpriteRenderer sr in listSRBubble)
        {
            sr.transform.DOScale(0, 0.5f);
        }
        foreach (SpriteRenderer sr in listSRContent)
        {
            sr.transform.DOScale(0, 0.5f);
        }
        yield return new WaitForSeconds(1f);
        NextState();
        yield break;
    }

    public IEnumerator IE_Round2()
    {
        foreach (SpriteRenderer sr in listSRBubble)
        {
            sr.transform.DOScale(1, 0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        //Show the content
        listSRContent[1].transform.DOScale(1, 0.5f);
        listSRContent[2].transform.DOScale(1, 0.5f);
        listSRContent[3].transform.DOScale(1, 0.5f);
        yield return new WaitForSeconds(2f);
        listSRContent[0].transform.DOScale(1, 0.5f);

        yield return new WaitForSeconds(2f);
        foreach (SpriteRenderer sr in listSRPeople)
        {
            sr.transform.DOScale(0, 0.5f);
        }
        yield return new WaitForSeconds(0.5f);

        NextState();

        yield break;
    }
    #endregion


    #region Battle
    public IEnumerator IE_Battle()
    {
        objRound.SetActive(false);
        objBattle.SetActive(true);
        yield break;
    }
    #endregion
}
