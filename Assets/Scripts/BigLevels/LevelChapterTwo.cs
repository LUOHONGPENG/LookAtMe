using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChapterTwo : LevelBasic
{
    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        StartCoroutine(IE_NextLevel());
    }

    private IEnumerator IE_NextLevel()
    {
        PublicTool.TransitionIconEffect(TransitionIconType.Shape);
        yield return new WaitForSeconds(2f);
        NextLevel();
    }
}
