using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFakeSelfAbuse : LevelBasic
{
    public GameObject pfLipstick;
    public Transform tfLipstick;

    public Collider2D colMask;
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

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider!=null&&hit.collider.tag == "ColMask")
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousePos);
            }


            //AssignScreenAsMask();
        }
    }



    public void AssignScreenAsMask()
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
    }

}
