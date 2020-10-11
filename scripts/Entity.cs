using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, GotDamage
{
    public bool inBattle = false; // has the battle started
    public bool dead = false;


    //
    // ─── BATTLE ─────────────────────────────────────────────────────────────────────
    //

    public abstract GameObject target { get; set; }
    [SerializeField]
    protected float autoAtkInterval = 3f;
    public int maxHP = 10;
    [SerializeField]
    protected int currentHP;
    public int healthPoint
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = value;
        }
    }

    // if casting, boss wont move and AA
    public CastGroup castingStatus; // only contains one single status
    public bool casting
    {
        get { return castingStatus != null; }
    }

    public HashSet<StatusGroup> statusGroups = new HashSet<StatusGroup>();

    protected virtual void Start()
    {
        StartCoroutine(AutoAttack());
        StartCoroutine(CheckStatusExpiration());
    }

    protected virtual void Update()
    {
        foreach (StatusGroup statusGroup in statusGroups)
        {
            statusGroup.Update();
        }
    }

    public virtual void GotDamage(int dmg)
    {
        // TODO: Check Magic/Physical Vulnerability
        if (currentHP > dmg)
            currentHP -= dmg;
        else
        {
            currentHP = 0;
            OnDead();
        }
    }
    public virtual void GotHealed(int hp)
    {

    }

    public void AddStatusGroup(StatusGroup sg)
    {
        statusGroups.Add(sg);
        BattleManager bm = GameObject.FindGameObjectWithTag(Constants.BM.Tag).GetComponent<BattleManager>();
        bm.AddStatusIconToUI(sg);
        Debug.Log($"Entity {this.name} added {sg.ToString()}, has {statusGroups.Count} statuses.");
    }

    public void RemoveStatusGroup(StatusGroup sg)
    {
        Debug.Log($"Entity {this.name} removes {sg.ToString()}, has {statusGroups.Count} statuses.");
        statusGroups.Remove(sg);
    }

    public void RegisterEffect()
    {
        if (!dead)
        {

            Debug.Log($"Entity ({this.name}) RegisterEffect", this.gameObject);
            foreach (StatusGroup statusGroup in statusGroups)
            {
                statusGroup.RegisterEffect();
            }
        }
    }

    IEnumerator CheckStatusExpiration()
    {
        // if some statusGroup is expired, remove it from the Hashset
        // Lazy remove
        while (true)
        {

            List<StatusGroup> toremove = new List<StatusGroup>();
            foreach (StatusGroup statusGroup in statusGroups)
            {
                if (statusGroup.expired) toremove.Add(statusGroup);
            }
            foreach (StatusGroup item in toremove)
            {
                RemoveStatusGroup(item);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    protected IEnumerator AutoAttack()
    {
        while (!dead)
        {
            AA();
            yield return new WaitForSeconds(autoAtkInterval);
        }
    }

    protected void AA()
    {
        if (casting)
        {
            Debug.Log($"Entity {this.name} AutoAttack: Casting.");
            return;
        }
        if (target != null)
        {
            if (target.GetComponent<Entity>().dead)
            {
                Debug.Log($"Entity AutoAttack: Target {target.name} already dead. Set {this.name}'s target to NULL.");
                target = null;
                return;
            }
            // TODO: Damage calculation
            int damage = 10; // How much damage will be dealt. Calculated by the target and this object's statistics
            Debug.Log($"Entity AutoAttack Prepares: From {this.name} to {target.name}");
            AddStatusGroup(new DealDamageGroup(gameObject, target.gameObject, damage));
        }
        else
        {
            Debug.Log($"Entity AutoAttack: {this} Has No Target.");
        }
    }
    public virtual void OnDead()
    {
        Debug.Log($"Entity OnDead: {this.name}.");
        target = null;
        dead = true;
    }
}
