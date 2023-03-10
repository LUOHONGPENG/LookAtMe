using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class LevelSecondDebate : LevelBasic
{
    public enum LevelRound
    {
        Round1,
        Round2,
        Battle
    }

    public LevelRound currentRound = LevelRound.Round1;
    public CanvasGroup canvasGroupAll;

    [Header("People")]
    public GameObject pfPeople;
    public Transform tfContentPeople;
    private List<ItemDebatePeople> listPeople = new List<ItemDebatePeople>();
    private ItemDebatePeople itemPeopleMe;

    [Header("ThoughtFrame")]
    public Transform tfGroupMe;
    public List<Transform> listGroupFrame = new List<Transform>();

    [Header("ThoughtPrefab")]
    public GameObject pfThoughts;
    public List<Transform> listTfContentOtherThought;
    public Transform tfContentMyThought;
    private List<ThoughtContent> listOtherThought = new List<ThoughtContent>();
    private ThoughtContent myThought;

    [Header("DragThoughtPrefab")]
    public GameObject pfDrag;
    public Transform tfContentDrag;
    public List<Vector2> listDragPos;
    private List<DragThoughts> listDragItem = new List<DragThoughts>();
    public ThoughtsSlot dragSlot;
    public Image imgDragBox;

    [Header("Battle")]
    public List<BoxCollider2D> listColOther = new List<BoxCollider2D>();
    public List<bool> listColInScreen;
    public GameObject pfDragSharp;
    private DragSharp itemSharp;
    public BoxCollider2D triggerCheckLeaveScreen;
    public bool canDragSharp = false;
    private bool isSharpWin = false;

    private ThoughtType currentType = ThoughtType.None;
    private ThoughtType firstRoundType = ThoughtType.None;

    private void Update()
    {
        CheckWhetherLeaveScreen();
    }

    #region Init

    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        PublicTool.PlayMusic(MusicType.Discuss);
        PublicTool.ShowMouseTip(TipType.Drag, 3f);

        InitPrefabs();

        //Initialize data
        canDragSharp = false;
        isSharpWin = false;
        listColInScreen = new List<bool>() { true, true, true };
        currentRound = LevelRound.Round1;


        StartCoroutine(IE_InitRound());
    }

    public void InitPrefabs()
    {
        //Initial People
        listPeople.Clear();
        PublicTool.ClearChildItem(tfContentPeople);
        for (int i = 0; i < 3; i++)
        {
            GameObject objPeople = GameObject.Instantiate(pfPeople, tfContentPeople);
            ItemDebatePeople itemPeople = objPeople.GetComponent<ItemDebatePeople>();
            itemPeople.Init((ItemDebatePeople.PeopleType)i);
            listPeople.Add(itemPeople);
        }
        GameObject objPeopleMe = GameObject.Instantiate(pfPeople, tfContentPeople);
        itemPeopleMe = objPeopleMe.GetComponent<ItemDebatePeople>();
        itemPeopleMe.Init(ItemDebatePeople.PeopleType.Me);


        //Initial other thoughts
        listOtherThought.Clear();
        for (int i = 0; i < 3; i++)
        {
            PublicTool.ClearChildItem(listTfContentOtherThought[i]);
            GameObject objThought = GameObject.Instantiate(pfThoughts, listTfContentOtherThought[i]);
            ThoughtContent itemThought = objThought.GetComponent<ThoughtContent>();
            itemThought.Init(true);
            listOtherThought.Add(itemThought);
        }

        //Initial my thought
        GameObject objMyThought = GameObject.Instantiate(pfThoughts, tfContentMyThought);
        myThought = objMyThought.GetComponent<ThoughtContent>();
        myThought.Init(false);

        //Initial Drag thing
        PublicTool.ClearChildItem(tfContentDrag);
        listDragItem.Clear();

        for (int i = 0; i < 3; i++)
        {
            //Important code for automatically generating prefab
            GameObject objDrag = GameObject.Instantiate(pfDrag, tfContentDrag);
            //Important code for automatically generating prefab
            DragThoughts itemDrag = objDrag.GetComponent<DragThoughts>();
            itemDrag.InitPosition(listDragPos[i]);
            listDragItem.Add(itemDrag);
        }
        listDragItem[0].Init(ThoughtType.Square, this);
        listDragItem[1].Init(ThoughtType.Circle, this);
        listDragItem[2].Init(ThoughtType.Triangle, this);

        dragSlot.Init(this);
    }
    #endregion

    #region FlowControl
    public IEnumerator IE_InitRound()
    {
        currentType = ThoughtType.None;
        InitRoundNormalPeople();
        canvasGroupAll.blocksRaycasts = false;
        switch (currentRound)
        {
            case LevelRound.Round1:
                InitRoundNormalPeople();
                //HideAllComponent
                tfGroupMe.DOScale(0, 0);
                imgDragBox.DOFade(0, 0);
                for (int i = 0; i < 3; i++)
                {
                    listGroupFrame[i].DOScale(0, 0);
                    listDragItem[i].transform.DOScale(0, 0);
                }
                yield return new WaitForSeconds(0.5f);
                //ShowUpAnimation
                tfGroupMe.DOScale(1f, GameGlobal.timeFD_commonAni);
                imgDragBox.DOFade(1f, GameGlobal.timeFD_commonAni);
                for (int i = 0; i < 3; i++)
                {
                    listGroupFrame[i].DOScale(1f, GameGlobal.timeFD_commonAni);
                    listDragItem[i].transform.DOScale(1f, GameGlobal.timeFD_commonAni);
                }
                yield return new WaitForSeconds(GameGlobal.timeFD_commonAni);
                break;
            case LevelRound.Round2:
                for (int i = 0; i < 3; i++)
                {
                    if ((int)firstRoundType != i)
                    {
                        listDragItem[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        listDragItem[i].transform.DOScale(1f, GameGlobal.timeFD_commonAni);
                    }
                }
                imgDragBox.DOFade(1f, GameGlobal.timeFD_commonAni);
                for (int i = 0; i < 3; i++)
                {
                    listOtherThought[i].RoundInit();
                }
                myThought.RoundInit();
                yield return new WaitForSeconds(GameGlobal.timeFD_commonAni);
                break;
            case LevelRound.Battle:
                dragSlot.transform.DOScale(0, GameGlobal.timeFD_commonAni);
                yield return new WaitForSeconds(GameGlobal.timeFD_commonAni);
                GameObject objSharp = GameObject.Instantiate(pfDragSharp, tfGroupMe);
                itemSharp = objSharp.GetComponent<DragSharp>();
                itemSharp.Init(tfGroupMe,this);
                itemSharp.transform.DOScale(1f, GameGlobal.timeFD_commonAni);
                yield return new WaitForSeconds(GameGlobal.timeFD_commonAni);
                canDragSharp = true;
                break;
        }
        canvasGroupAll.blocksRaycasts = true;
        yield break;
    }

    public IEnumerator IE_DragGoalFinish()
    {
        canvasGroupAll.blocksRaycasts = false;
        PublicTool.HideMouseTip();
        myThought.ShowContent(currentType, 0);
        if (currentRound == LevelRound.Round1)
        {
            firstRoundType = currentType;
        }
        GoalFinishGenerateOtherThought();
        GoalFinishZeroScaleAllDragOption();
        yield return new WaitForSeconds(GameGlobal.timeFD_commonAni);
        yield return new WaitForSeconds(2f);
        GoalFinishSurprisePeople();
        yield return StartCoroutine(IE_EndRound());
        yield break;
    }
    public IEnumerator IE_EndRound()
    {
        if (currentRound == LevelRound.Battle)
        {
            yield return new WaitForSeconds(1f);
            PublicTool.TransitionIconEffect(TransitionIconType.Dress);
            PublicTool.StopMusic(false);
            yield return new WaitForSeconds(1.1f);
            NextLevel();
            yield break;//Similar to return in function
        }
        yield return new WaitForSeconds(GameGlobal.timeFD_roundInterval);
        currentRound++;
        yield return StartCoroutine(IE_InitRound());
    }
    #endregion

    #region AboutDragExtra
    public override void SetCurrentDragging(int typeID)
    {
        currentType = (ThoughtType)typeID;
    }

    public override void ReleaseDragging()
    {
        currentType = ThoughtType.None;
    }

    public override void DragFinishCheck()
    {
        if (currentRound != LevelRound.Battle)
        {
            StartCoroutine(IE_DragGoalFinish());
        }
    }
    #endregion
}