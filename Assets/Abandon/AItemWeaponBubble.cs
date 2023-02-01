using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AItemWeaponBubble : MonoBehaviour
{
    public SpriteRenderer srWeaponBubble;
    public BoxCollider2D colBubble;
    public Rigidbody2D thisBody;
    public bool isBeingHeld = false;

    public float speedBubble = 5f;

    private ALevelSecondDebate parent; 
    private float dragStartPosX;
    private float dragStartPosY;

    public void Init(ALevelSecondDebate parent)
    {
        this.parent = parent;
    }

    private void Update()
    {
        CheckDrag();
    }

    private void CheckDrag()
    {
        if (isBeingHeld == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            //Make sure the point you click is kept on the pointer of the mouse
            thisBody.velocity = new Vector3(speedBubble * (mousePos.x - dragStartPosX - this.transform.position.x), speedBubble * (mousePos.y - dragStartPosY - this.transform.position.y), 0);
            //this.gameObject.transform.position = new Vector3(mousePos.x - dragStartPosX, mousePos.y - dragStartPosY, 0);
        }
    }

    public float CalculateSpeed()
    {
        return thisBody.velocity.magnitude;
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }

    private void OnMouseDown()
    {
        isBeingHeld = true;
    }


}
