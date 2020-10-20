using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Volpi.ObjectyPool;
public abstract class Entity : MonoBehaviour, GotDamage
{
    public bool inBattle = false; // has the battle started
    public bool dead = false;


    //
    // ─── BATTLE ─────────────────────────────────────────────────────────────────────
    //

    public abstract GameObject target { get; set; }
    [SerializeField]
    public int maxHP = 10;
    [SerializeField]
    protected float currentHP;
    public float healthPoint
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
    public float basicDamage { get; set; } = 100;

    // if casting, boss wont move and AA
    public CastGroup castingStatus; // only contains one single status
    public bool casting
    {
        get { return castingStatus != null; }
    }

    public Dictionary<int, StatusGroup> statusGroups = new Dictionary<int, StatusGroup>();
    public GameObject damageInfoPrefab;

    //
    // ─── UI ─────────────────────────────────────────────────────────────────────────
    //

    public Canvas moveInfoCanvas;


    private void Awake()
    {
        moveInfoCanvas = transform.parent.Find("move info canvas").GetComponent<Canvas>();
    }

    protected virtual void Start()
    {
        StartCoroutine("AutoAttack");
        StartCoroutine("CheckStatusExpiration");
    }

    protected virtual void Update()
    {
        foreach (StatusGroup statusGroup in statusGroups.Values)
        {
            statusGroup.Update();
        }
    }

    public virtual void GotDamage(float dmg)
    {
        if (currentHP > dmg)
        {
            currentHP -= dmg;
        }
        else
        {
            currentHP = 0;
            OnDead();
        }
        // TODO: Show damage queue
        ShowDamangeNumber((int)dmg);
    }

    public virtual void GotHealed(float amount)
    {
        if (currentHP + amount < maxHP)
        {
            currentHP += amount;
        }
        else
        {
            currentHP = maxHP;
        }
        ShowDamangeNumber(-(int)amount);
    }

    private void ShowDamangeNumber(int amount)
    {
        Debug.Log($"Entity {gameObject.name} ShowDamangeNumber: {amount}");
        // GameObject damageInfoGO = Instantiate(damageInfoPrefab, transform.position, transform.rotation);
        GameObject damageInfoGO = ObjectyManager.Instance.ObjectyPools[Constants.UI.DamageInfoPoolName].Spawn(Constants.UI.DamageInfoPoolSpawningName);
        DamageTextFollower damageTextFollower = damageInfoGO.GetComponent<DamageTextFollower>();
        damageTextFollower.isDamageInfo = true;
        damageTextFollower.Init(par: moveInfoCanvas.transform, dmg: amount);
    }


    public void AddStatusGroup(StatusGroup sg)
    {
        if (statusGroups.ContainsKey(sg.GetHashCode()))
        {
            statusGroups[sg.GetHashCode()].MergeStatus(sg);
        }
        else
        {
            statusGroups.Add(sg.GetHashCode(), sg);

            BattleManager bm = GameObject.FindGameObjectWithTag(Constants.BM.Tag).GetComponent<BattleManager>();
            if (sg.showIcon)
                bm.AddStatusIconToUI();
            Debug.Log($"Entity {this.name} added {sg.ToString()}, has {statusGroups.Count} statuses.");
        }
    }

    public void RemoveStatusGroup(StatusGroup sg)
    {
        Debug.Log($"Entity {this.name} removes {sg.ToString()}, has {statusGroups.Count} statuses.");
        statusGroups.Remove(sg.GetHashCode());
    }

    public void RegisterEffect()
    {
        if (!dead)
        {
            Debug.Log($"Entity ({this.name}) RegisterEffect", this.gameObject);
            foreach (StatusGroup statusGroup in statusGroups.Values)
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
            foreach (StatusGroup statusGroup in statusGroups.Values)
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
            if (casting)
            {
                Debug.Log($"Entity {this.name} AutoAttack: Casting.");

            }
            else
            {
                if (target != null)
                {
                    if (target.GetComponent<Entity>().dead)
                    {
                        Debug.Log($"Entity AutoAttack: Target {target.name} already dead. Set {this.name}'s target to NULL.");
                        target = null;
                    }
                    else
                    {
                        AA();
                    }
                }
                else
                {
                    Debug.Log($"Entity AutoAttack: {this} Has No Target.");
                }

            }
            yield return new WaitForSeconds(Constants.Battle.AutoAtkInterval);
        }
    }

    protected virtual void AA()
    { 
        Debug.Log($"Entity AutoAttack Prepares: From {this.name} to {target.name}");
        AddStatusGroup(new DealDamageGroup(gameObject, target.gameObject, basicDamage));

    }

    public virtual void OnDead()
    {
        Debug.Log($"Entity OnDead: {this.name}.");
        // target = null;
        StopAllCoroutines();
        dead = true;
    }

}
