﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slowdown : SingleStatus
{
    private ControllerSystem controller;
    public Slowdown(GameObject from, GameObject target, float dur) :
        base(from, target, dur)
    {
        controller = this.target.GetComponent<ControllerSystem>();
        effectiveAtOnce = true;
        icon = Resources.Load<Sprite>("battle_status/status_slowdown");
        statusName = "Slowdown";
        
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
