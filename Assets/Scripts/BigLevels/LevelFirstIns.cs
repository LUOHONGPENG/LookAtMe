using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Video;

public class LevelFirstIns : LevelBasic
{
    public ScrollRect scroll;
    public CanvasGroup canvasGroupIns;
    public Image imgBg;


    [Header("Photo")]
    public GameObject pfPhoto;
    public Transform tfContentPhoto;
    private ItemInsPhoto itemPhoto;
    public Transform tfMaskPhoto;
    public Image imgInsPhoto;

    [Header("Like")]
    public Image imgLike;
    public Button btnLike;
    public Text codeLike;
    public ParticleSystem particleLike;

    [Header("Comment")]
    public GameObject pfComment;
    public Transform tfContentComment;
    public List<Vector2> listPosComment = new List<Vector2>();

    [Header("Video")]
    public VideoPlayer objVideo;

    private bool isLike = false;
    private int numLike = 0;

    public override void Init(LevelManager parent)
    {
        imgBg.DOFade(0, 0);
        base.Init(parent);
        objVideo.targetCamera = GameManager.Instance.uiCamera;
        objVideo.gameObject.SetActive(false);

        InitView();

        PublicTool.PlayMusic(MusicType.InsHappy);


        btnLike.onClick.RemoveAllListeners();
        btnLike.onClick.AddListener(delegate ()
        {
            if (!isLike)
            {
                StartCoroutine(IE_FirstLike());
                StartCoroutine(IE_FirstLikeNum());
                btnLike.interactable = false;
                imgLike.raycastTarget = true;
                isLike = true;
            }
        });
    }

    #region Init
    public void InitView()
    {
        canvasGroupIns.alpha = 0;
        //Init view
        imgLike.DOFade(0.01f, 0);
        imgLike.raycastTarget = false;
        codeLike.text = 0.ToString();
        numLike = 0;
        scroll.vertical = false;

        InitPhoto();
        StartCoroutine(IE_InitAni());
    }

    public void InitPhoto()
    {
        GameObject objShoot = GameObject.Instantiate(pfPhoto, tfContentPhoto);
        itemPhoto = objShoot.GetComponent<ItemInsPhoto>();
        itemPhoto.Init(this, PhotoType.Display,GameGlobal.posFP_photoToInsX, GameGlobal.posFP_photoToInsY);


        imgInsPhoto.sprite = GameManager.Instance.levelManager.spLastShoot;
        imgInsPhoto.transform.localPosition = GameManager.Instance.levelManager.posLastShoot;
        imgInsPhoto.SetNativeSize();
        //imgInsPhoto.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -10F));
    }

    public IEnumerator IE_InitAni()
    {
        itemPhoto.imgFrame.DOFade(0, 0.5f);
        imgBg.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(0.5F);
        itemPhoto.imgPhoto.DOFade(0, 0.5f);
        canvasGroupIns.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(1F);
        PublicTool.ClearChildItem(tfContentPhoto);

        PublicTool.ShowMouseTip(TipType.Click);
    }

    #endregion

    #region Like
    public IEnumerator IE_FirstLike()
    {
        PublicTool.HideMouseTip();

        imgLike.DOFade(1f, 0.5f);
        imgLike.transform.DOScale(2f, 0.5f);
        
        yield return new WaitForSeconds(0.5f);

        imgLike.transform.DOScale(1f, 0.5f);
        particleLike.Play();
        //Play Particle System
        
        yield return new WaitForSeconds(0.5f);
        
        GenerateComment();
        yield return new WaitForSeconds(3f);

        //yield return StartCoroutine(IE_AniVideo());
        NextLevel();
    }



    public IEnumerator IE_FirstLikeNum()
    {
        yield return StartCoroutine(IE_LikeLoop(10, 0.1F));
        yield return StartCoroutine(IE_LikeLoop(50, 0.04F));
        yield return StartCoroutine(IE_LikeLoop(10, 0.1F));
    }

    public IEnumerator IE_LikeLoop(int num,float time)
    {
        for (int i = 0; i < num; i++)
        {
            numLike++;
            codeLike.text = numLike.ToString();
            yield return new WaitForSeconds(time);
        }
    }
    #endregion

    #region Comment
    public void GenerateComment()
    {
        for(int i = 0; i < 5; i++)
        {
            float ranTime = Random.Range(0, 1f);
            GameObject objComment = GameObject.Instantiate(pfComment, tfContentComment);
            objComment.transform.localPosition = listPosComment[i];
            ItemInsComment itemComment = objComment.GetComponent<ItemInsComment>();
            itemComment.InitImage(i, true);
            itemComment.InitAni(ranTime);
        }
    }
    #endregion
}
