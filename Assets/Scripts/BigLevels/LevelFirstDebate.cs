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

    //�������ϵ���״��ɶ
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
        //ʡ�Ժű�ɸ���ͼ��
    }


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
}
