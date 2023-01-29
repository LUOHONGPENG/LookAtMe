using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFakeSelfAbuse : LevelBasic
{
    public GameObject pfLipstick;
    public Transform tfLipstick;
    public List<ItemColDetect> listColDetect;

    private LipstickManager activeLine;

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
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPoint, Vector2.zero);

            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "ColMask")
                    {
                        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        activeLine.UpdateLine(mousePos);
                    }

                    if (hit.collider.tag == "ColDetect")
                    {
                        ItemColDetect itemDetect = hit.collider.gameObject.GetComponent<ItemColDetect>();
                        if (!itemDetect.isTouched)
                        {
                            itemDetect.isTouched = true;
                        }
                    }
                }
            }


            //AssignScreenAsMask();
        }
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



    /*    public void AssignScreenAsMask()
        {
            int height = Screen.height;
            int width = Screen.width;
            int depth = 1;

            RenderTexture renderTexture = new RenderTexture(width, height, depth);
            Rect rect = new Rect(0, 0, width, height);
            Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);

            GameManager.Instance.cameraMask.targetTexture = renderTexture;
            GameManager.Instance.cameraMask.Render();

            RenderTexture currentRenderTexture = RenderTexture.active;
            RenderTexture.active = renderTexture;
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();

            GameManager.Instance.cameraMask.targetTexture = null;
            RenderTexture.active = currentRenderTexture;
            Destroy(renderTexture);

            Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), Screen.height / 10);



            Debug.Log("AssignScreenAsMask");
        }*/

}
