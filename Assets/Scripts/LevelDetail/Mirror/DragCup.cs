using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragCup : MonoBehaviour
{
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public bool checkdrag;//if cups isbeen drag
    public bool isInit;
    private LevelMirror parent;

    public void Init(LevelMirror parent)
    {
        this.parent = parent;
        isInit = true;
        checkdrag = false;
    }

    void Update()
    {
        if (isInit) {
        if (checkdrag)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }
    }

    }

    private void OnMouseDown()
    {
        checkdrag = true;
        parent.CheckDragCup(checkdrag);
    }


}
