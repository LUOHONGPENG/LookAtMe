using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelSecondDebate : LevelBasic
{
    public enum LevelState
    {
        Round1,
        Round2,
        Round3
    }

    public LevelState currentround = LevelState.Round1;

    public List<ThoughtContent> listOtherThought;
    public ThoughtContent myThought;

    public DragThoughtSecond dragItem0;
    public DragThoughtSecond dragItem1;
    public DragThoughtSecond dragItem2;
    
    public ThoughtSlotSecond dragSlot;

    public CanvasGroup canvasGroup;

    //你现在拖得形状是啥
    private ThoughtType currentType = ThoughtType.None;

    /*
    public void Changeround()
    {
        ThoughtType curchosen;
        curchosen = currentType;
        //currentType = ThoughtType.None;
        if (currentround == LevelState.Round1)
        {
        }
        else if (currentround == LevelState.Round2)
        {
            Debug.Log("changetoround2");
        }
        else if (currentround == LevelState.Round3)
        {
            Debug.Log("changetoround3");


        }
    }

    */


    //Initialize
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        foreach (ThoughtContent other in listOtherThought)
        {
            other.Init();
        }
        myThought.Init();

        dragItem0.Init(ThoughtType.Square, this) ;
        dragItem1.Init(ThoughtType.Circle, this);
        dragItem2.Init(ThoughtType.Triangle, this);

        dragSlot.Init(this);

        currentType = ThoughtType.None;
    }

    
    #region FlowControl
    public void DragGoalFinish()
    {
        myThought.ShowContent(currentType, 0.1f);
        foreach (ThoughtContent other in listOtherThought)
        {
            int ranType = Random.Range(0, 3);
            float timeDelay = Random.Range(1f, 2f);

            while(ranType == (int)currentType)
            {
                ranType = Random.Range(0, 3);
            }//end while
            
            other.ShowContent((ThoughtType)ranType, timeDelay);

        }

        //Can't interact with thing any more
         canvasGroup.blocksRaycasts = false;
      //  currentround++;
        StartCoroutine(IE_EndLevel());

    }

    public IEnumerator IE_EndLevel()
    {

        yield return new WaitForSeconds(3f);
        if ((int)currentround <= 2)
        {
            //other.ShowContent(ThoughtType.None);
            //Changeround();
            // DragGoalFinish();
        }
        else
        { NextLevel(); }
    }
    #endregion

    #region AboutDrag
    //设置你现在拖得是哪种形状
    public void SetCurrentDragging(ThoughtType type)
    {
        currentType = type;
    }

    //你现在没有拖东西了
    public void ReleaseDragging()
    {
        currentType = ThoughtType.None;
    }
    #endregion

    
}