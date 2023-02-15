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
    
    
    
    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        itemCup.Init(this);
        count = 0;
        IfDragCup = false;
        IfMirrorBroken = false ;
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


        if (Input.GetMouseButtonDown(0))
        {
            if (IfDragCup && CheckHitMirror())
            {
                if (count < 3)
                {
                    ChangeSprite();
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
        }
    }

    public void CheckDragCup(bool flag)
    {
        IfDragCup = flag;
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
