using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemInsPhoto : MonoBehaviour
{
    private LevelBasic parent;

    public CanvasGroup canvasGroupPhoto;
    public Button btnShoot;
    public Image imgPhoto;

    private bool isInit = false;
    private bool isShoot = false;

    public void Init(LevelBasic parent,bool shootDone)
    {
        isInit = true;
        this.parent = parent;
        btnShoot.onClick.RemoveAllListeners();
        btnShoot.onClick.AddListener(delegate ()
        {
            if (!isShoot)
            {
                StartCoroutine(ShootIns());
                isShoot = true;

                parent.AfterShoot();
                MoveToCenter();
            }
        });

        if (!shootDone)
        {
            isShoot = false;
            this.transform.localRotation = Quaternion.Euler(Vector3.zero);
            this.transform.localScale = Vector3.one;
        }
        else
        {
            isShoot = true;
            imgPhoto.DOFade(1f, 0);
            this.transform.localPosition = new Vector2(20.2f, 99.4f);
            this.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -10f));
            this.transform.localScale = new Vector3(GameGlobal.scaleFP_photoToInsX, GameGlobal.scaleFP_photoToInsY, 1);
            imgPhoto.sprite = GameManager.Instance.levelManager.spLastShoot;
            imgPhoto.SetNativeSize();
            imgPhoto.transform.localPosition = GameManager.Instance.levelManager.posLastShoot;
        }
    }

    void Update()
    {
        if (isInit && !isShoot)
        {
            this.transform.position = PublicTool.GetMousePosition2D();
        }
    }

    public IEnumerator ShootIns()
    {
        canvasGroupPhoto.alpha = 0;
        yield return new WaitForEndOfFrame();

/*        int ScreenSizeX = Screen.width;
        int ScreenSizeY = Screen.height;*/

        int ScreenSizeX = 1920;
        int ScreenSizeY = 1080;

        //Debug.Log(ScreenSizeX);


        RenderTexture rt = new RenderTexture(ScreenSizeX, ScreenSizeY, 0);
        Camera.main.targetTexture = rt;
        Camera.main.Render();

        RenderTexture.active = rt;

        Texture2D screenShot = new Texture2D(ScreenSizeX, ScreenSizeY, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, ScreenSizeX, ScreenSizeY), 0, 0);
        screenShot.Apply();

        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        //Display Photo
        Sprite sr = Sprite.Create(screenShot, new Rect(0, 0, ScreenSizeX, ScreenSizeY), new Vector2(0.5F, 0.5F));
        imgPhoto.sprite = sr;
        imgPhoto.SetNativeSize();
        imgPhoto.transform.position = Vector2.zero;

        yield return new WaitForEndOfFrame();
        GameManager.Instance.levelManager.SaveShoot(sr, imgPhoto.transform.localPosition);
        canvasGroupPhoto.alpha = 1f;
        imgPhoto.DOFade(1f, 0);

    }

    public void MoveToCenter()
    {
        this.transform.DOLocalMove(new Vector2(20.2f,99.4F), 0.5f);
        this.transform.DOScaleX(GameGlobal.scaleFP_photoToInsX, 0.5f);
        this.transform.DOScaleY(GameGlobal.scaleFP_photoToInsY, 0.5f);
        this.transform.DORotateQuaternion(Quaternion.Euler(new Vector3(0, 0, -10f)), 0.5f);
    }

    public void MoveTo(Vector2 pos)
    {
        this.transform.DOLocalMove(pos, 0.5f);
    }
}
