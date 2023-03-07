using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBasic : MonoBehaviour
{
    private LevelManager parent;
    public Button btnCommonFullClick;

    #region Basic
    public virtual void Init(LevelManager parent)
    {
        this.parent = parent;

        if (btnCommonFullClick != null)
        {
            btnCommonFullClick.onClick.RemoveAllListeners();
            btnCommonFullClick.onClick.AddListener(delegate ()
            {
                NextLevel();
            });
        }
    }

    public virtual void NextLevel()
    {
        parent.NextLevel();
    }
    #endregion

    public bool isTaskDoneExtra = false;

    #region ExtraFunc_Shoot
    public virtual void AfterShoot() { }

    #endregion

    #region ExtraFunc_Drag
    public virtual void SetCurrentDragging(int typeID) { }
    public virtual void ReleaseDragging() { }

    public virtual void DragFinishCheck() { }
    #endregion

    #region ExtraFunc_Party
    public virtual void FlipPartyPeople(int ID) { }

    public virtual void FlipBackPartyPeople() { }

    #endregion
}
