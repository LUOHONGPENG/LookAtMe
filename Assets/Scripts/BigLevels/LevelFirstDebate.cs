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

    public OtherThoughtDebate other0;
    public OtherThoughtDebate other1;
    public OtherThoughtDebate other2;

    public DragThoughts dragItem0;
    public DragThoughts dragItem1;
    public DragThoughts dragItem2;

    public ThoughtsSlot dragSlot;

    //你现在拖得形状是啥
    private ThoughtType currentType = ThoughtType.None;

    //Initialize
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        other0.Init();
        other1.Init();
        other2.Init();

        dragItem0.Init(ThoughtType.Square,this);
        dragItem0.Init(ThoughtType.Circle,this);
        dragItem0.Init(ThoughtType.Triangle,this);

        dragSlot.Init(this);

        currentType = ThoughtType.None;
    }

    public void DragGoalFinish()
    {
        //省略号变成各个图形
    }


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
}
