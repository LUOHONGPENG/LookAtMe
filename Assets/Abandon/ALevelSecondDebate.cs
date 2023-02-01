using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ALevelSecondDebate : LevelBasic
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
    public Transform tfLine;
    public SpriteRenderer srMe;
    public SpriteRenderer srEnemy;
    public Collider2D colHitBox;
    public AItemWeaponBubble itemWeaponBubble;
    private float timerCheckHit = 0;
    private int vHealthPoint = 10;

    //Initialize
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        itemWeaponBubble.Init(this);

        currentState = InnerLevelState.Round1;
        ReadState();
    }

    private void Update()
    {
        timerCheckHit -= Time.deltaTime;
        if (timerCheckHit < 0)
        {
            CheckHit();
        }
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
                StartCoroutine(IE_Battle());
                break;
            case InnerLevelState.YouWin:
                NextLevel();
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
    //Process of Battle Start
    public IEnumerator IE_Battle()
    {
        objRound.SetActive(false);
        objBattle.SetActive(true);

        vHealthPoint = 10;
        
        yield break;
    }

    //The bubble hit the line
    public void GetBattleHit()
    {
        vHealthPoint--;
        timerCheckHit = 0.5f;
        UpdateBattleStatus();
        if (vHealthPoint <= 0 && currentState == InnerLevelState.BattleStart)
        {
            NextState();
        }
    }

    public void CheckHit()
    {
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
        colHitBox.OverlapCollider(filter, results);
        foreach (Collider2D col in results)
        {
            if (col.tag == "ColDetect")
            {
                if (itemWeaponBubble.CalculateSpeed() > 2.5f)
                {
                    GetBattleHit();
                }
            }
        }
    }


    public void UpdateBattleStatus()
    {
        float scaleEnemy = 0.2f + vHealthPoint * 0.08f;
        float posLine = (vHealthPoint - 10) * 0.6f;
        float scaleMe = 1f + (10 - vHealthPoint) * 0.08f;

        srEnemy.transform.DOScale(scaleEnemy, 0.2f);
        srMe.transform.DOScale(scaleMe, 0.2f);
        tfLine.DOMoveX(posLine, 0.2f);

    }

    //

    #endregion
}
