using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelFirstParty : LevelBasic
{
    public CanvasGroup canvasGroupParty;
    [Header("PartyPeople")]
    public GameObject pfPartyPeople;
    public Transform tfPartyPeople;
    public List<Vector2> listPosPartyPeople = new List<Vector2>();
    //A list that contains 4 partypeople prefabs
    private List<ItemPartyPeople> listPartyPeople = new List<ItemPartyPeople>();
    public ItemCharacterParty itemCharacter;

    [Header("MainCharacter")]
    public Image imgLight;
    public Image imgRibbon;
    public Animator aniStar;

    [Header("Photo")]
    public GameObject pfPhoto;
    public Transform tfContentPhoto;
    private ItemInsPhoto itemPhoto;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);
        PublicTool.PlayMusic(MusicType.Party);
        PublicTool.ShowMouseTip(TipType.Click, 3f);


        isTaskDoneExtra = false;

        itemCharacter.Init(DressType.Black);
        InitCharacterGroup();
        InitPrefab();
    }

    public void InitCharacterGroup()
    {
        canvasGroupParty.alpha = 1f;
        imgLight.DOFade(0, 0);
        imgRibbon.transform.DOLocalMoveY(900f, 0);
    }

    public void InitPrefab()
    {
        listPartyPeople.Clear();
        PublicTool.ClearChildItem(tfPartyPeople);
        for(int i = 0; i < 4; i++)
        {
            GameObject objPeople = GameObject.Instantiate(pfPartyPeople, tfPartyPeople);
            ItemPartyPeople itemPartyPeople = objPeople.GetComponent<ItemPartyPeople>();
            listPartyPeople.Add(itemPartyPeople);
            itemPartyPeople.Init(i,this);
            itemPartyPeople.transform.localPosition = listPosPartyPeople[i];
        }

        PublicTool.ClearChildItem(tfContentPhoto);

    }

    #region FlipPeople
    public override void ChangePose()
    {
        itemCharacter.UpdatePose();
        PublicTool.HideMouseTip();

    }

    public override void FlipPartyPeople(int ID)
    {
        itemCharacter.UpdatePose();

        int numPeopleFlip = 0;
        foreach (ItemPartyPeople people in listPartyPeople)
        {
            if (people.isFlip)
            {
                numPeopleFlip++;
            }
        }

        UpdateCharacterBar();

        //check whether 4 
        if (numPeopleFlip >= 4 && !isTaskDoneExtra)
        {
            isTaskDoneExtra = true;
            foreach(var item in listPartyPeople)
            {
                item.hoverBtnPeople.enabled = false;
            }
            StartCoroutine(IE_FlipGoalDeal());
        }
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
        PublicTool.PlaySound(SoundType.Wow);
        imgLight.DOFade(1f, GameGlobal.timerFP_light);
        imgRibbon.transform.DOLocalMoveY(100, GameGlobal.timerFP_light);
        aniStar.Play("Star", 0, -1);
        yield return new WaitForSeconds(GameGlobal.timerFP_light);

        yield return new WaitForSeconds(2f);

        InitShootPhoto();
        yield break;
    }

    

    #endregion

    #region ShootPhoto

    public void InitShootPhoto()
    {
        GameObject objShoot = GameObject.Instantiate(pfPhoto, tfContentPhoto);
        itemPhoto = objShoot.GetComponent<ItemInsPhoto>();
        itemPhoto.Init(this,PhotoType.Manual);
    }

    public override void AfterShoot()
    {
        StartCoroutine(IE_AfterShoot());
    }

    public IEnumerator IE_AfterShoot()
    {
        canvasGroupParty.DOFade(0, 0.5f);
        yield return new WaitForSeconds(1f);
        PublicTool.StopMusic(false);
        yield return new WaitForSeconds(1.1f);
        NextLevel();
    }

    #endregion

}
