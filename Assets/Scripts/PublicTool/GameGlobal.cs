using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobal
{
    //FirstDebate
    public static float timeFD_commonAni = 0.2f;//The time of most of the animation
    public static float timeFD_roundInterval = 3f;//The time interval between one round end and next round

    //FirstParty
    public static float timerFP_notice = 2f;//The time that people lose interested in you
    public static float timerFP_light = 0.5f;//The time that the light become bright from none
    public static float scaleFP_photoToInsX = 0.65f;//The X scale of the photo scale down after taking photo(Dont touch it) 
    public static float scaleFP_photoToInsY = 0.7f;//The Y scale of the photo scale down after taking photo(Dont touch it) 


    //SecondParty
    public static int countSP_fail = 12;

    //SecondIns
    public static int countSI_freeScroll = 3;
    public static float timerSI_freeScrollNumGrow = 2f;


}
