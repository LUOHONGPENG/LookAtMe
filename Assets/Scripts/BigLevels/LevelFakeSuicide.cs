using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelFakeSuicide : LevelBasic
{
    [Header("TempCanvas")]
    public GameObject pfTempCanvas;
    private LevelTempCanvas itemCanvas;
    [Header("ColDetect")]
    public List<ItemColDetect> listColDetect;
    [Header("Hand")]
    public Texture2D texture_Hand;
    public SpriteRenderer srHandMade;
    [Header("Drag")]
    public DragLipstick dragLipstick;
    [Header("Photo")]
    public GameObject pfPhoto;
    private ItemInsPhoto itemPhoto;
    [Header("View")]
    public SpriteRenderer srHand;
    public SpriteRenderer srSink;
    public SpriteRenderer srHurt;
    [Header("Mask")]
    public GameObject pfMask;
    public Transform tfMask;

    //The timer that check whether all point covered
    private float timerCheckReachAllPoints = 0.2f;
    private bool isDrawDone;

    private Texture2D newTexture;
    //Some mapping information
    float worldWidth, worldHeight;
    float pixelWidth, pixelHeight;

    //A coroutine that call 
    private Coroutine coNextLevel = null;
    private bool isInit = false;

    //Initialize
    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        //Data about checking drawing points
        isDrawDone = false;
        foreach (ItemColDetect item in listColDetect)
        {
            item.isTouched = false;
        }
        //InitHand();
        //InitDrag
        dragLipstick.Init();
        //Init Canvas
        GameObject objCanvas = GameObject.Instantiate(pfTempCanvas, parent.tfContentImage);
        itemCanvas = objCanvas.GetComponent<LevelTempCanvas>();
        //Init Mask
        PublicTool.ClearChildItem(tfMask);
        //InitPhoto
        InitPhoto();
        
        isInit = true;
    }

    #region MakingHoleByModifyingTexture
    public void InitHand()
    {
        //According to the asset to create a new texture
        newTexture = new Texture2D(texture_Hand.width, texture_Hand.height);
        Color[] colors = texture_Hand.GetPixels();
        newTexture.SetPixels(colors);
        //Apply and make sprite
        newTexture.Apply();
        MakeSprite();
        //Read and load the mapping information
        worldWidth = srHandMade.bounds.size.x;
        worldHeight = srHandMade.bounds.size.y;
        pixelWidth = srHandMade.sprite.texture.width;
        pixelHeight = srHandMade.sprite.texture.height;

        Debug.Log("World:" + worldWidth + "," + worldHeight + "Pixel:" + pixelWidth + "," + pixelHeight);;
    }

    void MakeSprite()
    {
        srHandMade.sprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.one * 0.5f);
    }

    //Convert To Pixel
    private Vector2Int WorldToPixel(Vector3 pos)
    {
        Vector2Int pixelPosition = Vector2Int.zero;

        var dx = pos.x - srHandMade.transform.position.x;
        var dy = pos.y - srHandMade.transform.position.y;

        pixelPosition.x = Mathf.RoundToInt(0.5f * pixelWidth + dx * (pixelWidth / worldWidth));
        pixelPosition.y = Mathf.RoundToInt(0.5f * pixelHeight + dy * (pixelHeight / worldHeight));

        return pixelPosition;
    }

    //When click the pixel, make a dot
    public void MakeHole(Vector2 pos)
    {
        Vector2Int pixelPos = WorldToPixel(pos);
        Debug.Log(pixelPos);
        int radius = GameGlobal.HoleRadius;

        int px, nx, py, ny, distance;

        for (int i = 0; i < radius; i++)
        {
            distance = Mathf.RoundToInt(Mathf.Sqrt(radius * radius - i * i));
            for (int j = 0; j < distance; j++)
            {
                px = pixelPos.x + i;
                nx = pixelPos.x - i;
                py = pixelPos.y + j;
                ny = pixelPos.y - j;

                CheckClearPixel(px, py);
                CheckClearPixel(nx, py);
                CheckClearPixel(px, ny);
                CheckClearPixel(nx, ny);
            }
        }

        newTexture.Apply();
        MakeSprite();
    }

    private void CheckClearPixel(int x, int y)
    {
        //IMPORTANT CHECK
/*        if (y > pixelHeight)
        {
            return;
        }*/
        newTexture.SetPixel(x, y, Color.clear);
    }

    #endregion


    private void Update()
    {
        if (isDrawDone || !isInit)
        {
            return;
        }

        GoTimeCheckDraw();
        CheckLipStick();
    }


    #region CheckLipStick
    public void AddMask(Vector2 pos)
    {
        GameObject objMask = GameObject.Instantiate(pfMask, pos,Quaternion.Euler(Vector2.zero),tfMask);
    }

    private void CheckLipStick()
    {
        if (Input.GetMouseButton(0) && dragLipstick.isBeingHeld)
        {
            AddMask(dragLipstick.tfDraw.position);

            RaycastHit2D[] hits = Physics2D.RaycastAll(dragLipstick.tfDraw.position, Vector2.zero);
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
            }
        }
    }
    #endregion


    #region Photo

    private void InitPhoto()
    {
        GameObject objPhoto = GameObject.Instantiate(pfPhoto, itemCanvas.tfPhoto);
        itemPhoto = objPhoto.GetComponent<ItemInsPhoto>();
        itemPhoto.Init(this, PhotoType.Manual);
        itemPhoto.gameObject.SetActive(false);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerator IE_InitPhoto()
    {
        dragLipstick.MoveBack();
        dragLipstick.isBeingHeld = false;
        dragLipstick.canDrag = false;
        itemPhoto.gameObject.SetActive(true);
        yield break;
    }

    public override void AfterShoot()
    {
        StartCoroutine(IE_AfterShoot());
    }

    public IEnumerator IE_AfterShoot()
    {
        srHand.DOFade(0, 0.5f);
        srHurt.DOFade(0, 0.5f);
        srSink.DOFade(0, 0.5f);
        dragLipstick.Hide(0.5f);
        yield return new WaitForSeconds(2f);
        NextLevel();
    }
    #endregion

    #region CheckDraw

    private void GoTimeCheckDraw()
    {
        timerCheckReachAllPoints -= Time.deltaTime;

        if (timerCheckReachAllPoints < 0)
        {
            if (CheckAllPointsTouched())
            {
                if (!isDrawDone)
                {
                    isDrawDone = true;
                }
                if (coNextLevel == null)
                {
                    coNextLevel = StartCoroutine(IE_InitPhoto());
                }
            }
            else
            {
                timerCheckReachAllPoints = 0.2f;
            }
        }
    }


    public bool CheckAllPointsTouched()
    {
        bool isAllTouched = true;
        foreach (ItemColDetect col in listColDetect)
        {
            if (!col.isTouched)
            {
                isAllTouched = false;
            }
        }
        return isAllTouched;
    }

    #endregion


}
