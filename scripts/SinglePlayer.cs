using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControllerSystem))]
public class SinglePlayer : Entity, GotDamage
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
    public GameObject targetBoss;
    public int manaPoints;

    //
    // ─── STRAT ──────────────────────────────────────────────────────────────────────
    //
    public string job;
    public StratPosition stratPosition; // D1234 H12 MST
    public bool controllable = true;


    void Start()
    {
        controller = GetComponent<ControllerSystem>();
        GameObject shiva = GameObject.Find("Shiva");
        AddStatusGroup(new SlowdownGroup(shiva, gameObject));
        DealDamageTest();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (controllable)
            controller.Control();
    }



    public void RegisterEntities()
    {
        foreach (GameObject en in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(en.GetComponent<Enemy>());
        }
    }

    public void DealDamageTest()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.AddStatusGroup(new DealDamageGroup(gameObject, enemy.gameObject, 4));
        }
    }

}
