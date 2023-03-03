using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPartyPeople : MonoBehaviour
{
    //View
    public List<Sprite> listSpPeople = new List<Sprite>();
    public Image imgPartyPeople;
    public Button btnPartyPeople;
    //Data
    public bool isFlip = false;
    private int peopleID = 0;
    private float timerFlip = 0f;
    private LevelBasic parent;

    public void Init(int ID,LevelBasic parent)
    {
        this.peopleID = ID;
        this.parent = parent;
        isFlip = false;
        imgPartyPeople.sprite = listSpPeople[0];
        btnPartyPeople.onClick.RemoveAllListeners();
        btnPartyPeople.onClick.AddListener(delegate ()
        {
            if (!isFlip)
            {
                Flip();
                parent.FlipPartyPeople(peopleID);
            }
        });
    }

    private void Update()
    {
        if (isFlip && parent!=null && !parent.isTaskDoneExtra)
        {
            timerFlip -= Time.deltaTime;
            if (timerFlip <= 0)
            {
                FlipBack();
            }
        }
    }


    #region FlipFunction
    public void Flip()
    {
        timerFlip = GameGlobal.timerFP_notice;
        imgPartyPeople.sprite = listSpPeople[1];
        isFlip = true;
    }
    public void FlipBack()
    {
        timerFlip = 0;
        imgPartyPeople.sprite = listSpPeople[0];
        isFlip = false;
    }
    #endregion
}
