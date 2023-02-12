using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFakeSelfAbuse : LevelBasic
{

    public List<ItemColDetect> listColDetect;

    //The timer that check whether all point covered
    private float timerCheckReachAllPoints = 0.2f;
    private bool isDrawDone = false;

    //A coroutine that call 
    private Coroutine coNextLevel = null;


    //Initialize
    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        isDrawDone = false;

        foreach (ItemColDetect item in listColDetect)
        {
            item.isTouched = false;
        }
    }

    private void Update()
    {
        if (isDrawDone)
        {
            return;
        }

        timerCheckReachAllPoints -= Time.deltaTime;

        if (timerCheckReachAllPoints < 0)
        {
            if (CheckAllPointsTouched())
            {
                if (coNextLevel == null)
                {
                    coNextLevel = StartCoroutine(IE_LevelComplete());
                }
            }
            else
            {
                timerCheckReachAllPoints = 0.2f;
            }
        }


/*        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPoint, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "ColDetect")
                {
                    ItemColDetect itemDetect = hit.collider.gameObject.GetComponent<ItemColDetect>();
                    if (!itemDetect.isTouched)
                    {
                        itemDetect.isTouched = true;
                    }
                }
            }
        }*/
    }

    public bool CheckAllPointsTouched()
    {
        bool isAllTouched = true;
        foreach(ItemColDetect col in listColDetect)
        {
            if (!col.isTouched)
            {
                isAllTouched = false;
            }
        }

        return isAllTouched;
    }

    //Call when level is completed
    public IEnumerator IE_LevelComplete()
    {
        //Change to next level after 2 seconds
        yield return new WaitForSeconds(2f);
        NextLevel();
    }

}
