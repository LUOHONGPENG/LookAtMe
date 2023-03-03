using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonHoverSprite : MonoBehaviour
{
    public Transform tfTarget;
    public Collider2D colHover;
    public bool isEnabled = true;

    private void Update()
    {
        if (isEnabled)
        {
            CheckWhetherHover();
        }
        else
        {
            tfTarget.localScale = Vector2.one;
        }
    }


    public void CheckWhetherHover()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(PublicTool.GetMousePosition2D(), Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Interaction")
                {
                    tfTarget.localScale = new Vector2(GameGlobal.rateScale_hover, GameGlobal.rateScale_hover);
                    return;
                }
            }
        }
        tfTarget.localScale = Vector2.one;
    }

}
