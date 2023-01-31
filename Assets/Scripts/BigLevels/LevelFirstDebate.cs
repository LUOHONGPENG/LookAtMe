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

    public List<ThoughtContent> listOtherThought;
    public ThoughtContent myThought;

    public DragThoughts dragItem0;
    public DragThoughts dragItem1;
    public DragThoughts dragItem2;

    public ThoughtsSlot dragSlot;

    public CanvasGroup canvasGroup;

    //�������ϵ���״��ɶ
    private ThoughtType currentType = ThoughtType.None;
    
    //Initialize
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        foreach(ThoughtContent other in listOtherThought)
        {
            other.Init();
        }
        myThought.Init();

        dragItem0.Init(ThoughtType.Square,this);
        dragItem1.Init(ThoughtType.Circle,this);
        dragItem2.Init(ThoughtType.Triangle,this);

        dragSlot.Init(this);

        currentType = ThoughtType.None;
    }

    #region FlowControl
    public void DragGoalFinish()
    {
        myThought.ShowContent(currentType);
        foreach (ThoughtContent other in listOtherThought)
        {
            int ranType = Random.Range(0, 3);
            while(ranType == (int)currentType)
            {
                ranType = Random.Range(0, 3);
            }
            other.ShowContent((ThoughtType)ranType);
        }

        //Can't interact with thing any more
        canvasGroup.blocksRaycasts = false;

        StartCoroutine(IE_EndLevel());

    }

    public IEnumerator IE_EndLevel()
    {
        
        yield return new WaitForSeconds(2f);
        NextLevel();
    }
    #endregion

    #region AboutDrag
    //�����������ϵ���������״
    public void SetCurrentDragging(ThoughtType type)
    {
        currentType = type;
    }

    //������û���϶�����
    public void ReleaseDragging()
    {
        currentType = ThoughtType.None;
    }
    #endregion
}
