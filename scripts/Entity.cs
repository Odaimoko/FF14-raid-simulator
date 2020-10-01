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
    protected float minAtkDistance = 3f, autoAtkInterval = 3f;
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
            // check dead or not
        }
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

    public void GotDamage(int dmg)
    {
        // TODO: Check Magic/Physical Vulnerability
        currentHP -= dmg;
    }

    public void AddStatusGroup(StatusGroup sg)
    {
        statusGroups.Add(sg);
        Debug.Log($"Entity {this} adds {sg.ToString()}, has {statusGroups.Count} statuses.");
    }

    public void RemoveStatusGroup(StatusGroup sg)
    {
        Debug.Log($"Entity {this} removes {sg.ToString()}, has {statusGroups.Count} statuses.");
        statusGroups.Remove(sg);
    }

    public void RegisterEffect()
    {
        Debug.Log($"Entity ({this}) RegisterEffect", this.gameObject);
        foreach (StatusGroup statusGroup in statusGroups)
        {
            statusGroup.RegisterEffect();
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
        while (true)
        {
            AutoAttack(target);
            yield return new WaitForSeconds(3f);
        }
    }

    protected void AutoAttack(GameObject target)
    {
        // Debug.Log($"{this} prepares to AutoAttack target: " + target);
        if (target != null)
        {
            // TODO: Damage calculation
            Vector3 towards = target.transform.position - gameObject.transform.position;
            towards.y = 0;
            int damage = 10; // How much damage will be dealt. Calculated by the target and this object's statistics
            if (towards.magnitude <= minAtkDistance)
            {
                Debug.Log($"Entity AutoAttack Prepares: From {this} to {target}");
                AddStatusGroup(new DealDamageGroup(gameObject, target.gameObject, damage));
            }
        }
        else
        {
            Debug.Log($"Entity AutoAttack ({this}): No Target.");
        }
    }
}
