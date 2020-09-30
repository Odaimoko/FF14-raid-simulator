using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControllerSystem))]
public class SinglePlayer : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();

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
    public bool controllable = true;


    void Start()
    {
        controller = GetComponent<ControllerSystem>();
        AddStatusGroup(new SlowdownGroup(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (controllable)
            controller.Control();
        foreach (StatusGroup statusGroup in statusGroups)
        {
            statusGroup.Update();
        }
    }

    public void AddStatusGroup(StatusGroup sg)
    {
        statusGroups.Add(sg);
    }

    public void ApplyEffect()
    {
        foreach (StatusGroup statusGroup in statusGroups)
        {
            Debug.Log("Player Apply", this.gameObject);
            statusGroup.ApplyEffect();
        }
    }

    public void RegisterEntities()
    {
        foreach (GameObject en in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(en.GetComponent<Enemy>());
        }
    }
}
