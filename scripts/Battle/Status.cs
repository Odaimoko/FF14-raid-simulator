using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public enum DurationType
    {
        Ephemeral, //  last for a period
        LongLasting, // no countdown
    }
    public enum BuffType
    {
        Buff,
        Debuff
    }
    // Status on Player or Enemy
    private float duration, // how long it will remain
     countdown; // remaining time
    private string statusName, statusDescription;
    private GameObject icon; // prefab
    // TODO: Effect variable


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyEffect()
    {

    }
}
