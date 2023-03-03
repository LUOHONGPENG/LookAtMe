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

    [Header("MainCharacter")]
    public Image imgLight;
    public Image imgRibbon;

    [Header("Photo")]
    public GameObject pfPhoto;
    public Transform tfContentPhoto;
    private ItemInsPhoto itemPhoto;

    public override void Init(LevelManager parent)
    {
        base.Init(parent);

        isTaskDoneExtra = false;

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

    public override void FlipPartyPeople(int ID)
    {
        int numPeopleFlip = 0;
        foreach (ItemPartyPeople people in listPartyPeople)
        {
            if (people.isFlip)
            {
                numPeopleFlip++;
            }
        }

        //check whether 4 
        if (numPeopleFlip >= 4 && !isTaskDoneExtra)
        {
            isTaskDoneExtra = true;
            StartCoroutine(IE_FlipGoalDeal());
        }
    }

    public IEnumerator IE_FlipGoalDeal()
    {
        imgLight.DOFade(1f, GameGlobal.timerFP_light);
        imgRibbon.transform.DOLocalMoveY(100, GameGlobal.timerFP_light);
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
        yield return new WaitForSeconds(2f);
        NextLevel();
    }

    #endregion

}
