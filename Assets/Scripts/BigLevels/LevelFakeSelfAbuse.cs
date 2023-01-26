using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFakeSelfAbuse : LevelBasic
{
    public GameObject pfLipstick;
    public Transform tfLipstick;



    LipstickManager activeLine;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newLine = Instantiate(pfLipstick,tfLipstick);
            activeLine = newLine.GetComponent<LipstickManager>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }

        if(activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }
    }

}
