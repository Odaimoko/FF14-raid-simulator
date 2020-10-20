using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move_IceBrand : SingleStatus
{
    public Move_IceBrand(GameObject from, GameObject target, float dur) :
        base(from, target, dur)
    {
        name = "冰印剑";
        effectiveAtOnce = true;
        showIcon = false;
    }

    protected override void NormalEffect()
    {
        if (effectTimes == 0)
        {
            // TODO: Sharing AOE
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
