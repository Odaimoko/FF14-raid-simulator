using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShivaStanceWand : BaseStance
{
    public ShivaStanceWand(GameObject from, GameObject target) :
        base(from, target)
    {
        icon = LoadStatusSprite("status_shiva_wand");
        name = "冰霜之杖";
    }

}
