using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, GotDamage
{
    public bool inBattle = false; // has the battle started
    public bool dead = false;
    public List<StatusGroup> statusGroups = new List<StatusGroup>();

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
    protected virtual void Start()
    {
        StartCoroutine(AutoAttack());
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
        currentHP -= dmg;
    }

    public void AddStatusGroup(StatusGroup sg)
    {
        statusGroups.Add(sg);
    }

    public void RegisterEffect()
    {
        Debug.Log($"Entity ({this}) RegisterEffect", this.gameObject);
        foreach (StatusGroup statusGroup in statusGroups)
        {
            statusGroup.RegisterEffect();
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
        Debug.Log($"{this} prepares to AutoAttack target: " + target);
        if (target != null)
        {
            // TODO: Damage calculation
            Debug.Log($"Entity AutoAttack: From {this} to {target}");
            int damage = 10; // How much damage will be dealt. Calculated by the target and this object's statistics
            if (Vector3.Distance(target.transform.position, gameObject.transform.position) < minAtkDistance)
            {
                AddStatusGroup(new DealDamageGroup(gameObject, target.gameObject, damage));
            }
        }
        else
        {
            Debug.Log($"Entity AutoAttack ({this}): No Target.");
        }
    }
}
