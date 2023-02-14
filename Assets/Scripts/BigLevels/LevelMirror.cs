using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMirror : LevelBasic
{

    public GameObject mirror;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> breakmirror;//list of broken mirror sprite
    public bool IfDragCup; //check if drag the cup while click the mirror
    public bool IfMirrorBroken;
    private int count; //count the round
    private LevelManager parent;
    public DragCup itemCup;
    
    
    
    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        itemCup.Init(this);
        spriteRenderer = mirror.GetComponent<SpriteRenderer>();
        count = 0;
        IfDragCup = false;
        IfMirrorBroken = false ;
    }
 
    void Update()
    {
        if (IfDragCup)
        {
            ChangeSprite();
        }
        else if (IfMirrorBroken == true)
        {
            StartCoroutine(IE_GoToNextLevel());
        }

    }
    

    void ChangeSprite()
    {
        spriteRenderer.sprite = breakmirror[count];
        count = count+1;
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
        yield return new WaitForSeconds(2f);
        NextLevel();
    }

   
}
