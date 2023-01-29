using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeaponBubble : MonoBehaviour
{
    public SpriteRenderer srWeaponBubble;
    public bool isBeingHeld = false;

    private float dragStartPosX;
    private float dragStartPosY;


    private void Update()
    {
        
    }

    private void CheckDrag()
    {

        if (isBeingHeld == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.position = new Vector3(mousePos.x - dragStartPosX, mousePos.y - dragStartPosY, 0);
        }
    }

    private void OnMouseUp()
    {
        
    }

    private void OnMouseDown()
    {
        
    }
}
