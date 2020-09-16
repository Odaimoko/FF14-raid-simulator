using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool isInBattle; // has the battle started
    public bool targetable;
    private bool isBoss; // is boss or regular
    private Status[] statuses;
    private int normalAtkRawDamage;
    private int[] aggro;
    private int healthPoint;

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

    private void ResetVariable() {
        
    }
}
