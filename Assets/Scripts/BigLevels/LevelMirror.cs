using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float timerColdDown = 1f;
    
    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        itemCup.Init(this);
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
        if (count == 3)
        {
            IfMirrorBroken = true;
            PublicTool.PlaySound(SoundType.Break2);
        }
        else
        {
            PublicTool.PlaySound(SoundType.Break1);
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
}
