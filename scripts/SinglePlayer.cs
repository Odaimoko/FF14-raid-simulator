using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControllerSystem))]
public class SinglePlayer : MonoBehaviour
{

    // Start is called before the first frame update
    public List<StatusGroup> statusGroups;
    private ControllerSystem controller;
    void Start()
    {
        controller = GetComponent<ControllerSystem>();
        SlowdownGroup slowDownGroup = gameObject.AddComponent<SlowdownGroup>();
        statusGroups.Add(slowDownGroup);
    }

    // Update is called once per frame
    void Update()
    {
        controller.Control();
        // foreach (StatusGroup statusGroup in statusGroups)
        //     statusGroup.CountDown(Time.deltaTime);
    }

    public void ApplyEffect()
    {
        foreach (StatusGroup statusGroup in statusGroups)
        {
            Debug.Log("Player Apply");
            statusGroup.ApplyEffect();
        }

    }
}
