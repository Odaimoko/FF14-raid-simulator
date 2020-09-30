using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, GotDamage
{
    public bool inBattle = false; // has the battle started
    public bool dead = false;
    public List<StatusGroup> statusGroups = new List<StatusGroup>();

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

    protected virtual void Update() {
        
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
}
