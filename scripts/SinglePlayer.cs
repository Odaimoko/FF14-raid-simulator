using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControllerSystem))]
public class SinglePlayer : MonoBehaviour
{
    public enum StratPosition
    {
        MT, ST, H1, H2, D1, D2, D3, D4
    }

    private ControllerSystem controller;
    //
    // ─── BATTLE ─────────────────────────────────────────────────────────────────────
    //
    public bool dead = false;
    public List<StatusGroup> statusGroups = new List<StatusGroup>();
    public GameObject targetBoss;
    public int healthPoints, manaPoints;

    //
    // ─── STRAT ──────────────────────────────────────────────────────────────────────
    //
    public string job;
    public StratPosition stratPosition; // D1234 H12 MST
    void Start()
    {
        controller = GetComponent<ControllerSystem>();
        AddStatusGroup(new SlowdownGroup(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        controller.Control();
        foreach (StatusGroup statusGroup in statusGroups)
        {
            statusGroup.Update();
        }
    }

    void AddStatusGroup(StatusGroup sg)
    {
        statusGroups.Add(sg);
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
