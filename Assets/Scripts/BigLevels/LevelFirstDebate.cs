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

public class LevelFirstDebate : LevelBasic
{
    public enum LevelRound
    {
        Round1,
        Round2,
        Round3,
        Cheers
    }

    public LevelRound currentRound = LevelRound.Round1;

    [Header("Frame")]
    public Transform tfGroupSlot;
    public List<Transform> listGroupFrame = new List<Transform>();

    [Header("ThoughtPrefab")]
    public GameObject pfThoughts;
    public List<Transform> listTfContentOtherThought;
    public Transform tfContentMyThought;

    [HideInInspector]
    public List<ThoughtContent> listOtherThought;
    [HideInInspector]
    public ThoughtContent myThought;

    [Header("DragThoughtPrefab")]
    public GameObject pfDrag;
    public Transform tfContentDrag;
    public List<Vector2> listDragPos;
    public List<DragThoughts> listDragItem = new List<DragThoughts>();
    public ThoughtsSlot dragSlot;

    [Header("Cheer")]
    public GameObject pfCheer;
    public GameObject pfDragCheer;
    private DragCheer itemDragCheer;
    public GameObject groupCol;
    public BoxCollider2D triggerCheer;

    public CanvasGroup canvasGroup;

    private ThoughtType currentType = ThoughtType.None;
    private ThoughtType firstRoundType = ThoughtType.None;
    private bool isCheer = false;

    #region Init
    //Initialize
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        InitPrefabs();
        groupCol.gameObject.SetActive(false);

        isCheer = false;
        currentRound = LevelRound.Round1;
        StartCoroutine(IE_InitRound());
    }

    //Dynamic
    public void InitPrefabs()
    {
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
            objDrag.transform.localPosition = listDragPos[i];
            //Important code for automatically generating prefab
            DragThoughts itemDrag = objDrag.GetComponent<DragThoughts>();
            listDragItem.Add(itemDrag);
        }
        listDragItem[0].Init(ThoughtType.Square, this);
        listDragItem[1].Init(ThoughtType.Circle, this);
        listDragItem[2].Init(ThoughtType.Triangle, this);

        dragSlot.Init(this);
    }

    #endregion

    #region Detect

    private void Update()
    {
        CheckCheer();
    }

    #endregion


    #region FlowControl

    public IEnumerator IE_InitRound()
    {
        currentType = ThoughtType.None;
        switch (currentRound)
        {
            case LevelRound.Round1:
                canvasGroup.blocksRaycasts = false;
                tfGroupSlot.DOScale(0, 0);
                for (int i = 0; i < 3; i++)
                {
                    listGroupFrame[i].DOScale(0, 0);
                    listDragItem[i].transform.DOScale(0, 0);
                }
                yield return new WaitForSeconds(1f);
                tfGroupSlot.DOScale(1f, GameGlobal.timeFDB_commonAni);
                for (int i = 0; i < 3; i++)
                {
                    listGroupFrame[i].DOScale(1f, GameGlobal.timeFDB_commonAni);
                    listDragItem[i].transform.DOScale(1f, GameGlobal.timeFDB_commonAni);
                }
                yield return new WaitForSeconds(GameGlobal.timeFDB_commonAni);
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
                        listDragItem[i].transform.DOScale(1f, GameGlobal.timeFDB_commonAni);
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    listOtherThought[i].RoundInit();
                }
                myThought.RoundInit();
                yield return new WaitForSeconds(GameGlobal.timeFDB_commonAni);
                break;
            case LevelRound.Round3:
                for (int i = 0; i < 3; i++)
                {
                    if ((int)firstRoundType != i)
                    {
                        listDragItem[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        listDragItem[i].transform.DOScale(1f, GameGlobal.timeFDB_commonAni);
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    listOtherThought[i].RoundInit();
                }
                myThought.RoundInit();
                yield return new WaitForSeconds(GameGlobal.timeFDB_commonAni);
                break;
            case LevelRound.Cheers:
                for (int i = 0; i < 3; i++)
                {
                    listOtherThought[i].HideAni();
                }
                myThought.HideAni();
                yield return new WaitForSeconds(GameGlobal.timeFDB_commonAni);
                //Clear
                PublicTool.ClearChildItem(tfContentMyThought);
                //Init Cheer Prefab
                for(int i = 0; i < 3; i++)
                {
                    PublicTool.ClearChildItem(listTfContentOtherThought[i]);
                    GameObject objCheer = GameObject.Instantiate(pfCheer, listTfContentOtherThought[i]);
                }
                GameObject objDragCheer = GameObject.Instantiate(pfDragCheer, tfContentMyThought);
                itemDragCheer = objDragCheer.GetComponent<DragCheer>();
                itemDragCheer.Init();
                groupCol.gameObject.SetActive(true);
                break;
        }
        canvasGroup.blocksRaycasts = true;
        yield break;
    }

    public IEnumerator IE_DragGoalFinish()
    {
        myThought.ShowContent(currentType,0);

        if(currentRound == LevelRound.Round1)
        {
            firstRoundType = currentType;
        }

        //If only one agree in turn two
        int ranAgreeID = Random.Range(0, 3);

        for (int i = 0;i < 3;i++)
        {
            ThoughtContent other = listOtherThought[i];
            //Random generate the type of each teammates
            int ranType = Random.Range(0, 3);
            //Random generate the delay time 
            float timeDelay = Random.Range(1f, 2f);

            if (currentRound == LevelRound.Round3)
            {
                ranType = (int)currentType;
            }//round3
            else if (currentRound == LevelRound.Round2)
            {
                if(ranAgreeID == i)
                {
                    ranType = (int)currentType;
                }
                else
                {
                    while (ranType == (int)currentType)
                    {
                        ranType = Random.Range(0, 3);
                    }
                }
            }
            else if (currentRound == LevelRound.Round1)
            {
                while (ranType == (int)currentType)
                {
                    ranType = Random.Range(0, 3);
                }
            }
            other.ShowContent((ThoughtType)ranType,timeDelay);
        }
        canvasGroup.blocksRaycasts = false;
        for (int i = 0; i < 3; i++)
        {
            listDragItem[i].transform.DOScale(0, GameGlobal.timeFDB_commonAni);
        }
        yield return new WaitForSeconds(GameGlobal.timeFDB_commonAni);
        yield return StartCoroutine(IE_EndRound());
    }

    public void CheckCheer()
    {
        if(currentRound == LevelRound.Cheers && !isCheer)
        {
            ContactFilter2D filter = new ContactFilter2D().NoFilter();
            List<Collider2D> results = new List<Collider2D>();
            triggerCheer.OverlapCollider(filter, results);
            foreach (BoxCollider2D col in results)
            {
                if (col.tag == "ColDetect")
                {
                    isCheer = true;
                    StartCoroutine(IE_CheerGoalFinish());
                }
            }
        }
    }

    public IEnumerator IE_CheerGoalFinish()
    {
        itemDragCheer.canDrag = false;
        yield return StartCoroutine(IE_EndRound());
        yield break;
    }


    public IEnumerator IE_EndRound()
    {
        yield return new WaitForSeconds(GameGlobal.timeFDB_roundInterval);
        if(currentRound == LevelRound.Cheers)
        {
            NextLevel();
            yield break;//Similar to return in function
        }
        currentRound++;
        yield return StartCoroutine(IE_InitRound());
    }


    #endregion

    #region AboutDrag
    public void SetCurrentDragging(ThoughtType type)
    {
        currentType = type;
    }

    public void ReleaseDragging()
    {
        currentType = ThoughtType.None;
    }
    #endregion

}
