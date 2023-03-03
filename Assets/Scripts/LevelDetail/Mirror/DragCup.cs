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
    public BoxCollider2D colHit;

    public void Init(LevelMirror parent)
    {
        this.parent = parent;
        isInit = true;
        checkdrag = false;
    }

    void Update()
    {
        if (isInit) 
        {
            if (checkdrag)
            {
                mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
            }
            else
            {
                CheckWhetherDrag();
            }
        }
    }

/*    private void OnMouseDown()
    {
        checkdrag = true;
        parent.CheckDragCup(checkdrag);
    }
*/
    public void CheckWhetherDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosNew = Input.mousePosition;
            mousePosNew.z = 10;
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePosNew);
            RaycastHit2D[] hits = Physics2D.RaycastAll(screenPos, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "CanDrag")
                    {

                        checkdrag = true;
                        parent.CheckDragCup(checkdrag);

                        return;
                    }
                }
            }
        }
    }
}
