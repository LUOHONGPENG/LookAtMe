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

    private float rateHalfDayValue = GameGlobal.rateClock_oneRound;


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

            if (vClock > 0.99f)
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
        float rateSlope = 1f / rateHalfDayValue;
        int countDay = Mathf.FloorToInt(vClock / rateHalfDayValue);
        float rateColor = 0;
        if(countDay % 2 == 0)
        {
            //e.g. 1-5(v-0.4)
            rateColor = 1f - rateSlope * (vClock - countDay * rateHalfDayValue);
        }
        else
        {
            //e.g. 5(v-0.2)
            rateColor = rateSlope * (vClock - countDay * rateHalfDayValue);
        }

        imgBg.color = new Color(1, 1, 1, rateColor);
        imgShy.color = new Color(1, 1, 1, rateColor);
        imgDesk.color = new Color(1, 1, 1, rateColor);


        float posyMoon = 0;
        if (countDay % 2 == 0)
        {
            posyMoon = 100f - (1000f * rateSlope * (vClock - countDay * rateHalfDayValue));
        }
        else
        {
            posyMoon = (100f-1000f) + (1000f * rateSlope * (vClock - countDay * rateHalfDayValue));

        }
        if (posyMoon < -400f)
        {
            posyMoon = -400f;
        }


        float posySun = 0;
        if (countDay % 2 == 0)
        {
            posySun =  (100f - 1000f) + (1000f * rateSlope * (vClock - countDay * rateHalfDayValue));
        }
        else
        {
            posySun = 100f - (1000f * rateSlope * (vClock - countDay * rateHalfDayValue));

        }
        if (posySun < -400f)
        {
            posySun = -400f;
        }

        imgMoon.rectTransform.anchoredPosition = new Vector2(-100F, posyMoon);
        imgSun.rectTransform.anchoredPosition = new Vector2(-100F, posySun);


        /*        if (vClock > 0.5f)
                {
                    imgMoon.rectTransform.anchoredPosition = new Vector2(-100F, -400F);
                    imgSun.rectTransform.anchoredPosition = new Vector2(-100F, -400F + (vClock - 0.5F) * 1000F);
                }
                else if (vClock < 0.5f)
                {
                    imgSun.rectTransform.anchoredPosition = new Vector2(-100F, -400F);
                    imgMoon.rectTransform.anchoredPosition = new Vector2(-100F, 100F - vClock * 1000F);
                }*/
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
