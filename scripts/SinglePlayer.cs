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

    //
    // ─── BATTLE ─────────────────────────────────────────────────────────────────────
    //
    // ver 0: player cannot choose target. this will be assgined in Scenario.

    private GameObject _target;
    public override GameObject target
    {
        get
        {
            if (_target)
            {
                Debug.Log($"SinglePlayer ({this.name}) Get Target {_target.name}", gameObject);
            }
            else
            {
                Debug.Log($"SinglePlayer ({this.name}) Has No Target.", gameObject);
            }
            return _target;
        }
        set
        {
            _target = value;
        }
    }
    private int manaPoints;
 
    //
    // ─── STRAT ──────────────────────────────────────────────────────────────────────
    //
    public ControllerSystem controller;
    public string job;
    public StratPosition stratPosition; // D1234 H12 MST
    private bool _controllable = true;

    public bool controllable
    {
        get
        {
            return _controllable;
        }
        set
        {
            controller.controllable = _controllable = value;
            // Debug.Log($"SinglePlayer set Controllable. {value}. _controllable: {_controllable}. controller.controllable: {controller.controllable}");
        }
    }

    private void Awake()
    {
        // controller should be init here so UI manager can find controller.
        controller = GetComponent<ControllerSystem>();
        // GameObject shiva = GameObject.Find("Shiva");
        // AddStatusGroup(new SlowdownGroup(shiva, gameObject));

    }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!dead)
            controller.Control();
        else
        {
            controllable = false;
        }
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
