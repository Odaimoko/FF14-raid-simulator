using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slowdown : SingleStatus
{
    private ControllerSystem controller;
    public Slowdown(GameObject from, GameObject target) :
        base(from, target)
    {
        controller = this.target.GetComponent<ControllerSystem>();
        duration = 10;
        countdown = 10;
    }

    protected override void NormalEffect()
    {
        base.NormalEffect();
        controller.moveSpeedMultiplier = 0.4f;
    }
    
    protected override void ExpireEffect()
    {
        base.ExpireEffect();
        controller.moveSpeedMultiplier = 1f;
    }
}
