using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelFirstIns : LevelBasic
{
    public Image imgLike;
    public Button btnLike;
    public Text codeLike;
    public ParticleSystem particleLike;

    public GameObject pfComment;
    public List<Transform> listPointComment;


    private bool isLike = false;
    private int numLike = 0;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        //Init view
        imgLike.DOFade(0.01f, 0);
        codeLike.text = 0.ToString();
        numLike = 0;

        btnLike.onClick.RemoveAllListeners();
        btnLike.onClick.AddListener(delegate ()
        {
            if (!isLike)
            {
                StartCoroutine(IE_FirstLike());
                StartCoroutine(IE_FirstLikeNum());
                isLike = true;
            }
        });
    }

    public IEnumerator IE_FirstLike()
    {
        imgLike.DOFade(1f, 0.5f);
        imgLike.transform.DOScale(2f, 0.5f);
        
        yield return new WaitForSeconds(0.5f);

        imgLike.transform.DOScale(1f, 0.5f);
        particleLike.Play();
        //Play Particle System
        
        yield return new WaitForSeconds(0.5f);
        
        GenerateComment();
        yield return new WaitForSeconds(4f);
        NextLevel();
    }
    public IEnumerator IE_FirstLikeNum()
    {
        for(int i = 0; i < 10; i++)
        {
            numLike++;
            codeLike.text = numLike.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 50; i++)
        {
            numLike++;
            codeLike.text = numLike.ToString();
            yield return new WaitForSeconds(0.04f);
        }

        for (int i = 0; i < 10; i++)
        {
            numLike++;
            codeLike.text = numLike.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void GenerateComment()
    {
        for(int i = 0; i < listPointComment.Count; i++)
        {
            float ranTime = Random.Range(0, 0.5f);
            GameObject objComment = GameObject.Instantiate(pfComment, listPointComment[i]);
            ItemInsComment itemComment = objComment.GetComponent<ItemInsComment>();
            itemComment.InitImage(i, true);
            itemComment.InitAni(ranTime);
        }
    }

}
