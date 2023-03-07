using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobal
{
    //FirstDebate
    public static float timeFD_commonAni = 0.2f;//The time of most of the animation
    public static float timeFD_roundInterval = 3f;//The time interval between one round end and next round

    //FirstParty
    public static float timerFP_notice = 2.2f;//The time that people lose interested in you
    public static float timerFP_light = 0.5f;//The time that the light become bright from none
    public static float scaleFP_photoToInsX = 1;//The X scale of the photo scale down after taking photo(Dont touch it) 
    public static float scaleFP_photoToInsY = 1;//The Y scale of the photo scale down after taking photo(Dont touch it) 
    public static float posFP_photoToInsX = 36f;
    public static float posFP_photoToInsY = 113.6f;

    //Clock
    public static float rateClock_oneRound = 0.2f;//the process rate of one round of clock

    //SecondParty
    public static int countSP_fail = 12;
    public static float timerSP_notice = 2f;//The time that people lose interested in you

    //SecondIns
    public static float posSI_photoToInsX = 36.8f;
    public static float posSI_photoToInsY = 84.5f;
    public static int countSI_freeScroll = 1;
    public static float timerSI_freeScrollNumGrow = 2f;
    public static int constSI_paddingTop = -106;
    public static int constSI_paddingBottom = 150;

    //FakeSuicide
    public static int HoleRadius = 32;//For both suicide

    //ThirdIns
    public static float posTI_photoToInsX = 36.5f;
    public static float posTI_photoToInsY = 83.8f;

    public static float rateRS_shake = 0.05f;

    //Hospital
    public static float rateHos_dragSpeed = 0.3f;
    public static float rateHos_eyeBack = 40f;

    public static float rateScale_hover = 1.15f;
}
