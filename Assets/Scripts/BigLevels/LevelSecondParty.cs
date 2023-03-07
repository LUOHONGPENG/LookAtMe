using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
public class LevelSecondParty : LevelBasic
{
    public CanvasGroup canvasGroupParty;
    [Header("PartyPeople")]
    public GameObject pfPartyPeople;
    public Transform tfPartyPeople;
    public List<Vector2> listPosPartyPeople = new List<Vector2>();
    private List<ItemPartyPeople> listPartyPeople = new List<ItemPartyPeople>();

    public Image imgBlack;

    [Header("MainCharacter")]
    public ItemCharacterParty itemCharacter;
    private int countFilp = 0;
    private bool isTriggerDark = false;

    //PostProcess
    private Vignette efVignette;
    private bool isInitVignette = false;
    private float timerVignette = 0;

    private void Update()
    {
        CheckVigEffect();
    }


    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        PublicTool.PlayMusic(MusicType.Party);

        //Data
        isTriggerDark = false;
        countFilp = 0;
        isInitVignette = false;
        timerVignette = 0;
        //View
        imgBlack.gameObject.SetActive(false);
        canvasGroupParty.alpha = 1f;
        InitPrefab();
        itemCharacter.Init(GameManager.Instance.levelManager.savedDressType);
    }
    public void InitPrefab()
    {
        listPartyPeople.Clear();
        PublicTool.ClearChildItem(tfPartyPeople);
        for (int i = 0; i < 4; i++)
        {
            GameObject objPeople = GameObject.Instantiate(pfPartyPeople, tfPartyPeople);
            ItemPartyPeople itemPartyPeople = objPeople.GetComponent<ItemPartyPeople>();
            listPartyPeople.Add(itemPartyPeople);
            itemPartyPeople.Init(i,this);
            itemPartyPeople.transform.localPosition = listPosPartyPeople[i];
        }
    }

    public override void FlipPartyPeople(int ID)
    {
        itemCharacter.UpdatePose();

        if (!isTriggerDark)
        {
            countFilp++;
            if (countFilp >= GameGlobal.countSP_fail)
            {
                isTriggerDark = true;
                StartCoroutine(IE_FlipGoalDeal());
            }
        }

        int numPeopleFlip = 0;
        foreach (ItemPartyPeople people in listPartyPeople)
        {
            if (people.isFlip)
            {
                numPeopleFlip++;
            }
        }

        if (numPeopleFlip >= 4 && !isTaskDoneExtra)
        {
            if(ID == listPartyPeople.Count - 1)
            {
                listPartyPeople[0].FlipBack();
            }
            else
            {
                listPartyPeople[ID+1].FlipBack();
            }
        }


        UpdateCharacterBar(); ;
    }
    public override void FlipBackPartyPeople()
    {
        UpdateCharacterBar();
    }

    public void UpdateCharacterBar()
    {
        int numPeopleFlip = 0;
        foreach (ItemPartyPeople people in listPartyPeople)
        {
            if (people.isFlip)
            {
                numPeopleFlip++;
            }
        }
        itemCharacter.SetBar(numPeopleFlip);
    }

    public IEnumerator IE_FlipGoalDeal()
    {
        InitVigEffect();
        PublicTool.StopMusic();
        PublicTool.PlaySound(SoundType.HeartBeat, true, true, 7f);
        yield return new WaitForSeconds(5f);
        isInitVignette = false;
        imgBlack.gameObject.SetActive(true);
        imgBlack.DOFade(0, 0);
        imgBlack.DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.effectManager.ClearPostProcess();
        NextLevel();
    }

    #region Vignette
    private void InitVigEffect()
    {
        GameObject objVip = PublicTool.PostProcessEffect(PostProcessType.SecondPartyVig);
        Volume volume = objVip.GetComponent<Volume>();
        Vignette tmp;
        if (volume.profile.TryGet<Vignette>(out tmp))
        {
            efVignette = tmp;
            efVignette.intensity.value = 0;
            isInitVignette = true;
        }
    }

    private void CheckVigEffect()
    {
        if (isInitVignette)
        {
            timerVignette += Time.deltaTime;

            efVignette.intensity.value = timerVignette * 0.18f;
        }
    }

    #endregion
}
