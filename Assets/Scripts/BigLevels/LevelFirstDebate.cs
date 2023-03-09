using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum ThoughtType
{
    Square,
    Circle,
    Triangle,
    None
}

public partial class LevelFirstDebate : LevelBasic
{
    public enum LevelRound
    {
        Round1,
        Round2,
        Round3,
        Cheers
    }

    public LevelRound currentRound = LevelRound.Round1;
    public CanvasGroup canvasGroupAll;
    public Animator aniDebate;

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

    [Header("Cheer")]
    public GameObject pfCheer;
    public GameObject pfDragCheer;
    private List<DebateCheer> listDebateCheer = new List<DebateCheer>();
    private DragCheer itemDragCheer;
    public GameObject groupCol;
    public BoxCollider2D triggerCheer;
    public bool isCheer = false;

    private ThoughtType currentType = ThoughtType.None;
    private ThoughtType firstRoundType = ThoughtType.None;

    #region Init
    //Initialize
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        InitPrefabs();
        groupCol.gameObject.SetActive(false);

        isCheer = false;
        currentRound = LevelRound.Round1;
        StartCoroutine(IE_Init());
    }

    public IEnumerator IE_Init()
    {
        yield return new WaitForSeconds(3f);
        PublicTool.PlayMusic(MusicType.Discuss);//Play the music
        aniDebate.Play("Debate");//Play the introductive animation
        yield return new WaitForSeconds(4f);
        PublicTool.TransitionColor(EffectColor.EffectColorType.White,0.4f);//Pure Color Transition
        yield return new WaitForSeconds(0.4f);
        aniDebate.gameObject.SetActive(false);//Hide the animation page when it is covered by white page
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(IE_InitRound());//Init the Level
        PublicTool.ShowMouseTip(TipType.Drag, 1f);//Show the Tip after 1f

    }

    //Dynamic
    public void InitPrefabs()
    {
        listDebateCheer.Clear();

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

        //GameObject objDrag = GameObject.Instantiate(pfDrag, listDragPos[i],Quaternion.Euler(Vector2.zero), tfContentDrag);
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
                for (int i = 0; i < 3; i++)
                {
                    listGroupFrame[i].DOScale(1f, GameGlobal.timeFD_commonAni);
                    listDragItem[i].transform.DOScale(1f, GameGlobal.timeFD_commonAni);
                }
                imgDragBox.DOFade(1f, GameGlobal.timeFD_commonAni);
                yield return new WaitForSeconds(GameGlobal.timeFD_commonAni);
                break;
            case LevelRound.Round2:
            case LevelRound.Round3:
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
            case LevelRound.Cheers:
                for (int i = 0; i < 3; i++)
                {
                    listOtherThought[i].HideAni();
                }
                myThought.HideAni();
                PublicTool.ShowMouseTip(TipType.Drag, 1f);//Show the Tip after 1f
                yield return new WaitForSeconds(GameGlobal.timeFD_commonAni);
                //Clear
                PublicTool.ClearChildItem(tfContentMyThought);
                //Init Cheer Prefab
                for(int i = 0; i < 3; i++)
                {
                    PublicTool.ClearChildItem(listTfContentOtherThought[i]);
                    GameObject objCheer = GameObject.Instantiate(pfCheer, listTfContentOtherThought[i]);
                    DebateCheer itemDebateCheer = objCheer.GetComponent<DebateCheer>();
                    itemDebateCheer.InitAni();
                    listDebateCheer.Add(itemDebateCheer);
                }
                GameObject objDragCheer = GameObject.Instantiate(pfDragCheer, tfContentMyThought);
                itemDragCheer = objDragCheer.GetComponent<DragCheer>();
                itemDragCheer.Init(this);
                groupCol.gameObject.SetActive(true);
                break;
        }
        canvasGroupAll.blocksRaycasts = true;
        yield break;
    }

    public IEnumerator IE_DragGoalFinish()
    {
        PublicTool.HideMouseTip();
        canvasGroupAll.blocksRaycasts = false;
        myThought.ShowContent(currentType,0);
        if(currentRound == LevelRound.Round1)
        {
            firstRoundType = currentType;
        }
        GoalFinishGenerateOtherThought();
        GoalFinishZeroScaleAllDragOption();
        yield return new WaitForSeconds(GameGlobal.timeFD_commonAni);
        yield return new WaitForSeconds(2f);
        GoalFinishSurprisePeople();
        yield return StartCoroutine(IE_EndRound());
    }



    public IEnumerator IE_CheerGoalFinish()
    {
        PublicTool.HideMouseTip();
        itemDragCheer.canDrag = false;
        PublicTool.PlaySound(SoundType.Cheer);
        listDebateCheer[2].AniStop();
        yield return StartCoroutine(IE_EndRound());
        yield break;
    }


    public IEnumerator IE_EndRound()
    {
        if (currentRound == LevelRound.Cheers)
        {
            PublicTool.TransitionIconEffect(TransitionIconType.Cheer);
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
        if (currentRound != LevelRound.Cheers)
        {
            StartCoroutine(IE_DragGoalFinish());
        }
    }
    #endregion
}
