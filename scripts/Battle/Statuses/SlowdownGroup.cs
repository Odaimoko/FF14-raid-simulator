using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlowdownGroup : StatusGroup
{
    // Wrapper
    private ControllerSystem controller;
    // Start is called before the first frame update
    void Start()
    {
        Slowdown slowdown = gameObject.AddComponent<Slowdown>();
        Add(slowdown);
    }

    public override void ApplyEffect()
    {
        Debug.Log("APPLIED SLOWDOWNGROUP");
        base.ApplyEffect();
    }
}
