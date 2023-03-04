using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class LevelMirror : LevelBasic
{
    public SpriteRenderer srCrack;
    public List<Sprite> breakmirror;//list of broken mirror sprite
    public bool IfDragCup; //check if drag the cup while click the mirror
    public bool IfMirrorBroken;
    private bool isReadyNextLevel = false;
    private int count; //count the round
    private LevelManager parent;
    public DragCup itemCup;

    public GameObject objCup;
    public GameObject objCupHeld;

    private Vignette efVignette;
    private float timerColdDown = 1f;
    
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        itemCup.Init(this);
        InitVigEffect();
        count = 0;
        IfDragCup = false;
        IfMirrorBroken = false ;

        objCup.SetActive(true);
        objCupHeld.SetActive(false);
    }
 
    void Update()
    {
        if (isReadyNextLevel)
        {
            return;
        }


        if (IfMirrorBroken == true)
        {
            StartCoroutine(IE_GoToNextLevel());

            return;
        }

        if (IfDragCup)
        {
            timerColdDown -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (IfDragCup && CheckHitMirror() && timerColdDown<0)
            {
                if (count < 3)
                {
                    ChangeSprite();
                    timerColdDown = 0.3f;
                }
            }
        }


    }

    void ChangeSprite()
    {
        srCrack.sprite = breakmirror[count];
        count = count + 1;
        PublicTool.ShakeCamera();

        switch (count)
        {
            case 1:
                ChangeVigEffect(0.15f);
                PublicTool.PlaySound(SoundType.Break1);
                break;
            case 2:
                ChangeVigEffect(0.25f);
                PublicTool.PlaySound(SoundType.Break1);
                break;
            case 3:
                IfMirrorBroken = true;
                ChangeVigEffect(0.32f);
                PublicTool.PlaySound(SoundType.Break2);
                break;
        }
    }

    public void CheckDragCup(bool flag)
    {
        if (!IfDragCup && flag)
        {
            objCup.SetActive(false);
            objCupHeld.SetActive(true);
            timerColdDown = 0.3f;
            IfDragCup = flag;
        }

    }

    public IEnumerator IE_GoToNextLevel()
    {
        isReadyNextLevel = true;
        yield return new WaitForSeconds(2f);
        NextLevel();
    }

   public bool CheckHitMirror()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(PublicTool.GetMousePosition2D(), Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "ColDetect")
                {
                    return true;
                }
            }
        }
        return false;
    }

    #region PostProcess
    private void InitVigEffect()
    {
        GameObject objVip = PublicTool.PostProcessEffect(PostProcessType.MirrorVig);
        Volume volume = objVip.GetComponent<Volume>();
        Vignette tmp;
        if (volume.profile.TryGet<Vignette>(out tmp))
        {
            efVignette = tmp;
            efVignette.intensity.value = 0;
        }
    }

    private void ChangeVigEffect(float value)
    {
        efVignette.intensity.value = value;
    }

    #endregion
}
