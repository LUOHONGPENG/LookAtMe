using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelClock : LevelBasic
{
    public Image imgBg;
    public Image imgShy;
    public Image imgDesk;
    public Image imgMoon;
    public Image imgSun;
    [Range(0,1f)]public float vClock = 0;
    private bool isInit = false;
    public bool canDrag = true;

    [Header("Clock")]
    public DragClock itemClock;
    public Transform tfPointer;
    private Coroutine coClock;


    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        itemClock.Init(this);
        vClock = 0;
        this.isInit = true;
        this.canDrag = true;
    }

    private void Update()
    {
        if (isInit)
        {
            if (vClock < 1)
            {
                vClock = itemClock.ConvertAngleToRate();
            }

            UpdateClock();

            if (vClock > 0.95f)
            {
                if(coClock == null)
                {
                    canDrag = false;
                    coClock = StartCoroutine(IE_ClockFinish());
                }
            }
        }
    }

    public void UpdateClock()
    {
        imgBg.color = new Color(1, 1, 1, 1f - vClock);
        imgShy.color = new Color(1, 1, 1, 1f - vClock);
        imgDesk.color = new Color(1, 1, 1, 1f - vClock);

        if (vClock > 0.5f)
        {
            imgMoon.rectTransform.anchoredPosition = new Vector2(-100F, -400F);
            imgSun.rectTransform.anchoredPosition = new Vector2(-100F, -400F + (vClock - 0.5F) * 1000F);
        }
        else if (vClock < 0.5f)
        {
            imgSun.rectTransform.anchoredPosition = new Vector2(-100F, -400F);
            imgMoon.rectTransform.anchoredPosition = new Vector2(-100F, 100F - vClock * 1000F);
        }
    }

    private IEnumerator IE_ClockFinish()
    {
        yield return new WaitForSeconds(1f);
        PublicTool.TransitionChapter(2);
        yield return new WaitForSeconds(1f);
        PublicTool.StopMusic();
        yield return new WaitForSeconds(1f);
        NextLevel();
        yield break;
    }
}
