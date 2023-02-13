using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragCup : MonoBehaviour
{
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public bool checkmouse;
    public bool isInit;
    private LevelMirror parent;

 
    public void Init(LevelMirror parent)
    {
        this.parent = parent;
        isInit = true;
    }

        void Update()
    { 
        if(isInit) { 
            Checkmouse(); 
        }

        if (checkmouse == true)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }
        else { }
       

    }


    
    public void Checkmouse()
    {
        if (Input.GetMouseButton(0))
        {
           checkmouse = true;
           parent.CheckDragCup(checkmouse);
        }
    }
    
    
}
