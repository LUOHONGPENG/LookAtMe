using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum PhotoType
{
    Manual,
    Auto,
    Display
}


public class ItemInsPhoto : MonoBehaviour
{
    private LevelBasic parent;
    private PhotoType photoType;

    public CanvasGroup canvasGroupPhoto;
    public Button btnShoot;
    public Image imgPhoto;
    public Image imgBlack;
    public Image imgFrame;

    private bool isInit = false;
    private bool isShoot = false;

    public void Init(LevelBasic parent,PhotoType type,float posX = 0, float posY = 0)
    {
        this.photoType = type;
        this.parent = parent;
        //Hide Black Mask
        imgBlack.gameObject.SetActive(false);
        btnShoot.onClick.RemoveAllListeners();
        btnShoot.onClick.AddListener(delegate ()
        {
            ShootExecute();
            btnShoot.interactable = false;
        });

        switch (photoType)
        {
            case PhotoType.Manual:
                isShoot = false;
                btnShoot.interactable = true;
                this.transform.localRotation = Quaternion.Euler(Vector3.zero);
                this.transform.localScale = Vector3.one;
                break;
            case PhotoType.Display:
                isShoot = true;
                btnShoot.interactable = false;
                this.transform.localPosition = new Vector2(posX, posY);
                //this.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -10f));
                this.transform.localScale = new Vector3(GameGlobal.scaleFP_photoToInsX, GameGlobal.scaleFP_photoToInsY, 1);
                imgPhoto.DOFade(1f, 0);
                imgPhoto.sprite = GameManager.Instance.levelManager.spLastShoot;
                imgPhoto.SetNativeSize();
                imgPhoto.transform.localPosition = GameManager.Instance.levelManager.posLastShoot;
                break;
            case PhotoType.Auto:
                isShoot = false;
                btnShoot.interactable = false;
                this.transform.localRotation = Quaternion.Euler(Vector3.zero);
                this.transform.localScale = Vector3.one;
                break;
        }

        isInit = true;
    }

    void Update()
    {
        CheckFollowMouse();
    }

    #region BasicFunction

    private void CheckFollowMouse()
    {
        if (isInit && !isShoot && photoType == PhotoType.Manual)
        {
            this.transform.position = PublicTool.GetMousePosition2D();
        }
    }

    public void ShootExecute()
    {
        if (!isShoot)
        {
            isShoot = true;
            StartCoroutine(ShootIns());
            parent.AfterShoot();
            BlackMask();
            MoveToCenter();
        }
    }

    public IEnumerator ShootIns()
    {
        canvasGroupPhoto.alpha = 0;
        PublicTool.PlaySound(SoundType.Camera);
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

    public void BlackMask()
    {
        imgBlack.DOFade(1f, 0);
        imgBlack.gameObject.SetActive(true);
        imgBlack.DOFade(0, 0.5f);
    }

    public void MoveToCenter()
    {
        switch(GameManager.Instance.levelManager.currentLevelState)
        {
            case LevelState.FirstParty:
                this.transform.DOLocalMove(new Vector2(GameGlobal.posFP_photoToInsX, GameGlobal.posFP_photoToInsY), 0.5f);
                break;
            case LevelState.DressUp:
                this.transform.DOLocalMove(new Vector2(GameGlobal.posSI_photoToInsX, GameGlobal.posSI_photoToInsY), 0.5f);
                break;
/*            case LevelState.SecondParty:
                this.transform.DOLocalMove(new Vector2(GameGlobal.posSI_photoToInsX, GameGlobal.posSI_photoToInsY), 0.5f);
                break;*/
            case LevelState.FakeSuicide:
                this.transform.DOLocalMove(new Vector2(GameGlobal.posTI_photoToInsX, GameGlobal.posTI_photoToInsY), 0.5f);
                break;
        }
        this.transform.DOScaleX(GameGlobal.scaleFP_photoToInsX, 0.5f);
        this.transform.DOScaleY(GameGlobal.scaleFP_photoToInsY, 0.5f);
        //this.transform.DORotateQuaternion(Quaternion.Euler(new Vector3(0, 0, -10f)), 0.5f);
    }

    public void MoveTo(Vector2 pos,float time)
    {
        this.transform.DOLocalMove(pos, time);
    }
    #endregion


}
