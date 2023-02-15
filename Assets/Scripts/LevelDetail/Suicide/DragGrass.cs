using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragGrass : DragHurtBasic
{
    protected override void CheckDrag()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos = PublicTool.GetMousePosition2D();
            float deltaX = Random.Range(-GameGlobal.rateRS_shake, GameGlobal.rateRS_shake);
            float deltaY = Random.Range(-GameGlobal.rateRS_shake, GameGlobal.rateRS_shake);


            this.gameObject.transform.position = new Vector3(mousePos.x - dragStartPosX+deltaX, mousePos.y - dragStartPosY+deltaY, 0);
        }
    }
}
