using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LevelManager
{
    #region ShootPhotoData
    [HideInInspector]
    public Sprite spLastShoot;
    [HideInInspector]
    public Vector2 posLastShoot;

    public void SaveShoot(Sprite sp, Vector2 pos)
    {
        this.spLastShoot = sp;
        this.posLastShoot = pos;
    }
    #endregion

    #region Dress
    public DressType savedDressType = DressType.None;



    #endregion
}
