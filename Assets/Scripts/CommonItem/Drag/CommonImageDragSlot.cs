using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommonImageDragSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DropDeal(eventData);
    }

    public virtual void DropDeal(PointerEventData eventData)
    {

    }
}
