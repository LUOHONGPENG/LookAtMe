using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRealSuicide : LevelBasic
{
    public GameObject pfTempCanvas;
    private LevelTempCanvas itemCanvas;
    public GameObject pfEye;
    [Header("ColDetect")]
    public List<ItemColDetect> listColDetect;
    [Header("Mask")]
    public GameObject pfMask;
    public Transform tfMask;

    public DragGrass dragGrass;

    //The timer that check whether all point covered
    private float timerCheckReachAllPoints = 0.2f;
    private bool isDrawDone;
    //A coroutine that call 
    private Coroutine coNextLevel = null;
    private Coroutine coColorEffect = null;
    private bool isInit = false;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        //Data about checking drawing points
        isDrawDone = false;
        foreach(ItemColDetect item in listColDetect)
        {
            item.isTouched = false;
        }
        //InitDrag
        dragGrass.Init();
        PublicTool.ClearChildItem(tfMask);
        //Init Canvas
        GameObject objCanvas = GameObject.Instantiate(pfTempCanvas, parent.tfContentImage);
        itemCanvas = objCanvas.GetComponent<LevelTempCanvas>();

        PublicTool.PlaySound(SoundType.HeartBeat);

        isInit = true;
    }
    private void Update()
    {
        if (isDrawDone || !isInit)
        {
            return;
        }

        GoTimeCheckDraw();
        CheckLipStick();
    }

    #region CheckLipStick
    public void AddMask(Vector2 pos)
    {
        GameObject objMask = GameObject.Instantiate(pfMask, pos, Quaternion.Euler(Vector2.zero), tfMask);
    }

    private void CheckLipStick()
    {
        if (Input.GetMouseButton(0) && dragGrass.isBeingHeld)
        {
            AddMask(dragGrass.tfDraw.position);

            RaycastHit2D[] hits = Physics2D.RaycastAll(dragGrass.tfDraw.position, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "ColDetect")
                    {
                        ItemColDetect itemDetect = hit.collider.gameObject.GetComponent<ItemColDetect>();
                        if (!itemDetect.isTouched)
                        {
                            itemDetect.isTouched = true;
                            if(coColorEffect == null)
                            {
                                coColorEffect = StartCoroutine(IE_ColorEffect());
                            }
                        }
                    }
                }
            }
        }
    }

    private IEnumerator IE_ColorEffect()
    {
        GameManager.Instance.effectManager.InitColor(EffectColor.EffectColorType.Red);
        yield return new WaitForSeconds(0.8f);
        coColorEffect = null;
    }
    #endregion

    #region CheckDraw
    private void GoTimeCheckDraw()
    {
        timerCheckReachAllPoints -= Time.deltaTime;

        if (timerCheckReachAllPoints < 0)
        {
            if (CheckAllPointsTouched())
            {
                if (!isDrawDone)
                {
                    isDrawDone = true;
                }
                if (coNextLevel == null)
                {
                    coNextLevel = StartCoroutine(IE_LevelComplete());
                }
            }
            else
            {
                timerCheckReachAllPoints = 0.2f;
            }
        }
    }


    public bool CheckAllPointsTouched()
    {
        bool isAllTouched = true;
        foreach (ItemColDetect col in listColDetect)
        {
            if (!col.isTouched)
            {
                isAllTouched = false;
            }
        }
        return isAllTouched;
    }
    #endregion

    #region EyeAni

    private void InitEye()
    {
        GameObject objEye = GameObject.Instantiate(pfEye, itemCanvas.tfPhoto);
        Animator aniEye = objEye.GetComponent<Animator>();
        aniEye.Play("CloseEye");
/*        itemPhoto = objPhoto.GetComponent<ItemInsPhoto>();
        itemPhoto.Init(this, PhotoType.Manual);
        itemPhoto.gameObject.SetActive(false);*/
    }



    #endregion



    public IEnumerator IE_LevelComplete()
    {
        InitEye();
        //Change to next level after 2 seconds
        yield return new WaitForSeconds(2f);
        PublicTool.StopSound(SoundType.HeartBeat);
        GameManager.Instance.effectManager.ClearPostProcess();
        NextLevel();
    }

}
