﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slowdown : SingleStatus
{
    private ControllerSystem controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ControllerSystem>();
        duration = 10;
        countdown=10;
    }
 
    public override void ApplyEffect()
    {
        Debug.Log("APPLIED: Slowdown effect to " + gameObject.ToString());
        controller.moveSpeedMultiplier = 0.4f;
        base.ApplyEffect();
    }
    // As the name says
    public override void OnStatusExpire()
    {
        Debug.Log("EXPIRED: SLOWDOWN");
        base.OnStatusExpire();
        controller.moveSpeedMultiplier = 1f;
    }
}
