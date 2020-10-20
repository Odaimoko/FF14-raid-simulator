using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move_IceBrand : SingleStatus
{
    public Move_IceBrand(GameObject from, GameObject target) :
        base(from, target, 4)
    {
        name = "冰印剑";
        effectiveAtOnce = true;
        showIcon = false;
    }

    protected override void NormalEffect()
    {
        if (effectTimes == 0)
        {
            base.NormalEffect();
            // eT will increase in base class
            Enemy en = target.GetComponent<Enemy>();
            en.movable = false;
            Debug.Log("Move_IceBrand: 冰印剑生效！");
        }
    }

    protected override void ExpireEffect()
    {
        base.ExpireEffect();
        Debug.Log("Move_IceBrand: 冰印剑结束！");
        Enemy en = target.GetComponent<Enemy>();
        en.movable = true;
    }
}
