using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusGroup))]
public class Enemy : MonoBehaviour
{
    //
    // ─── MOVEMENT ───────────────────────────────────────────────────────────────────
    //

    public float moveSpeed;
    public GameObject targetCircle;
    private GameObject model; // 
    private Animator animator;

    //
    // ─── BATTLE ─────────────────────────────────────────────────────────────────────
    //

    public bool inBattle; // has the battle started
    public bool targetable;
    private bool isBoss; // is boss or regular
    public bool casting; // if casting, boss cannot move
    [SerializeField]
    private int normalAtkRawDamage;
    private int[] aggro;
    public int healthPoint
    {
        get; set;
    }
    public List<StatusGroup> statusGroups;


    void Init(int num_players)
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void MovePerFrame()
    {
        // dont follow the dead players (if he fell off the edge)
    }

    void NormalAtk()
    {

    }

    void CastingAtk()
    {

    }

    private void ResetVariable()
    {

    }

    public void ApplyEffect()
    {
        foreach (StatusGroup statusGroup in statusGroups)
        {
            statusGroup.ApplyEffect();
        }
    }
}

