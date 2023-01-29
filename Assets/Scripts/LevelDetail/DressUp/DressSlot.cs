using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DressSlot : MonoBehaviour,IDropHandler
{
    public bool isDressed = false;
    
    //character wares the dress
    public void OnDrop(PointerEventData eventData)

    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            isDressed = true;
        }
        Debug.Log("OnDrop");
    }

    
    public void LoadNextLevel()
    {
        if (isDressed == true)
        {
            Debug.Log("is Dressed");
           // nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        }
    }
    


}
