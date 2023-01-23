using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPartyPeople : MonoBehaviour
{
    public List<Sprite> listSpPeople = new List<Sprite>();
    public Image imgPartyPeople;
    public Button btnPartyPeople;
    public bool isFlip = false;
    private float timerFlip = 0f;

    public void Init(LevelFirstMeet parent)
    {
        isFlip = false;
        imgPartyPeople.sprite = listSpPeople[0];
        btnPartyPeople.onClick.RemoveAllListeners();
        btnPartyPeople.onClick.AddListener(delegate ()
        {
            if (!isFlip)
            {
                Shake();
                Flip();
                parent.FlipPeople();
            }
        });
    }

    private void Update()
    {
        if (isFlip)
        {
            timerFlip -= Time.deltaTime;
            if (timerFlip <= 0)
            {
                FlipBack();
            }
        }
    }


    public void Shake()
    {
        //Shake
    }

    public void Flip()
    {
        timerFlip = 2f;
        imgPartyPeople.sprite = listSpPeople[1];
        isFlip = true;
    }
    public void FlipBack()
    {
        imgPartyPeople.sprite = listSpPeople[0];
        isFlip = false;
    }

}
