using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDiagnosis : LevelBasic
{
    public Animator aniHospital;

    public Image imgPage;
    public List<Sprite> listSpPage = new List<Sprite>();
    public Button btnLeft;
    public Button btnRight;
    private int countPage = 0;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        aniHospital.Play("Hospital");

        StartCoroutine(IE_Ani());

        countPage = 0;
        UpdatePage();

        btnLeft.onClick.RemoveAllListeners();
        btnLeft.onClick.AddListener(delegate ()
        {
            countPage--;
            UpdatePage();
        });

        btnRight.onClick.RemoveAllListeners();
        btnRight.onClick.AddListener(delegate ()
        {
            countPage++;
            UpdatePage();
        });
    }

    public IEnumerator IE_Ani()
    {
        yield return new WaitForSeconds(2.5f);

        yield return new WaitForSeconds(2.2f);
        GameManager.Instance.effectManager.InitEye();

        yield return new WaitForSeconds(3f);
        GameManager.Instance.effectManager.InitEye();

        yield return new WaitForSeconds(5f);
        GameManager.Instance.effectManager.InitEye();
        yield return new WaitForSeconds(0.8f);
        aniHospital.gameObject.SetActive(false);
    }

    public void UpdatePage()
    {
        imgPage.sprite = listSpPage[countPage];

        switch (countPage)
        {
            case 0:
                btnLeft.gameObject.SetActive(false);
                btnRight.gameObject.SetActive(true);
                break;
            case 1:
                btnLeft.gameObject.SetActive(true);
                btnRight.gameObject.SetActive(true);
                break;
            case 2:
                btnLeft.gameObject.SetActive(true);
                btnRight.gameObject.SetActive(false);
                break;
        }
    }
}
