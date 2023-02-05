using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ThoughtType
{
    Square,
    Circle,
    Triangle,
    None
}

public class LevelFirstDebate : LevelBasic
{
    public enum LevelState
    {
        Round1,
        Round2,
        Round3,
        Cheers
    }

    public LevelState currentround = LevelState.Round1;

    public List<ThoughtContent> listOtherThought;
    public ThoughtContent myThought;

    public DragThoughts dragItem0;
    public DragThoughts dragItem1;
    public DragThoughts dragItem2;

    public ThoughtsSlot dragSlot;

    public CanvasGroup canvasGroup;

    //你现在拖得形状是啥
    private ThoughtType currentType = ThoughtType.None;

    
    public void Changeround()
    {
        ThoughtType curchosen;
        curchosen = currentType;
        //currentType = ThoughtType.None;
        if (currentround == LevelState.Round1) {
           // DragGoalFinish();
        }
        else if (currentround == LevelState.Round2)
        {
          //  myThought.ShowContent(ThoughtType.None,0f);
           // dragItem0.Init(currentType,this);
           // dragItem1.Init(ThoughtType.None, this);
           // dragItem2.Init(ThoughtType.None, this);
            Debug.Log("changetoround2");
            //yield return new WaitForSeconds(5f);
            //DragGoalFinish();
        }
        else if (currentround == LevelState.Round3)
        {
            Debug.Log("changetoround3");


        }
    }
    



    //Initialize
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        foreach (ThoughtContent other in listOtherThought)
        {
            other.Init();
        }
        myThought.Init();

        dragItem0.Init(ThoughtType.Square, this);
        dragItem1.Init(ThoughtType.Circle, this);
        dragItem2.Init(ThoughtType.Triangle, this);

        dragSlot.Init(this);

        currentType = ThoughtType.None;
    }


    #region FlowControl
    public void DragGoalFinish()
    {
        myThought.ShowContent(currentType,0.1f);
        foreach (ThoughtContent other in listOtherThought)
        {
            int ranType = Random.Range(0, 3);
            float timeDelay = Random.Range(1f, 2f);

            if ((int)currentround == 2)
            {
                ranType = (int)currentType;
                Debug.Log("round3+" + (int)currentround);
            }//round3

            else if ((int)currentround == 1)
            {
                int agreethought = 1;
                while (agreethought == 1)
                {
                    ranType = (int)currentType;
                    agreethought--;
                    Debug.Log("round2+" + (int)currentround);
                    if (agreethought != 1 && ranType == (int)currentType)
                    {
                        ranType = Random.Range(0, 3);
                    }
                }
            }//round2

            else if ((int)currentround == 0)
            {
                Debug.Log("round1+" + (int)currentround);
                while (ranType == (int)currentType)
                {
                    ranType = Random.Range(0, 3);
                }
            }//round1
            else if ((int)currentround == 3)
            {
                //cheers
            }
            else { }
            /*
            while(ranType == (int)currentType)
            {
                ranType = Random.Range(0, 3);
            }//end while
            */
            other.ShowContent((ThoughtType)ranType,timeDelay);
            
        }

        //Can't interact with thing any more
        // canvasGroup.blocksRaycasts = false;
        currentround++;
        StartCoroutine(IE_EndLevel());

    }

    public IEnumerator IE_EndLevel()
    {
        
        yield return new WaitForSeconds(3f);
        if ((int)currentround <= 2) {
            //other.ShowContent(ThoughtType.None);
            Changeround();
           // DragGoalFinish();
        }
        else
        { NextLevel();}
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
