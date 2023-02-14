using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class BreakMirror : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public List<Sprite> breakmirror;
    public bool IfDragCup;
    private int i;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Init(LevelMirror parent)
    {
      //  this.parent = parent;
    }

    void Update()
    {
        // IfDragCup = DragCup.Checkmouse(); && IfDragCup == true
        if (Input.GetMouseButtonDown(0))
        { 
                ChangeSprite();
        }
        
    }


    void ChangeSprite()
    {
        spriteRenderer.sprite = breakmirror[i];
        // spriteRenderer.sprite = newSprite;
        i++;
        if(i == 4)
        {
           // parent.CheckIfBroken(ture);
        }
    }

}
