using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //
    // ─── INFORMATION ────────────────────────────────────────────────────────────────
    //

    public List<SinglePlayer> players = new List<SinglePlayer>();

    //
    // ─── MOVEMENT ───────────────────────────────────────────────────────────────────
    //
    [SerializeField]
    public float moveSpeed = .1f, minAtkDistance = 3f;
    public GameObject targetCircle;
    private GameObject model; // 

    private Animator animator;

    //
    // ─── BATTLE ─────────────────────────────────────────────────────────────────────
    //

    public bool inBattle; // has the battle started
    public bool targetable;
    private bool isBoss; // is boss or regular
    public bool movable { get; set; }  // if movable, boss will follow MT
    [SerializeField]
    private int normalAtkRawDamage;
    private Dictionary<GameObject, int> aggro;
    private GameObject cachedMT;
    public int healthPoint
    {
        get; set;
    }
    public List<StatusGroup> statusGroups = new List<StatusGroup>();

    //
    // ─── UI ─────────────────────────────────────────────────────────────────────────
    //

    public bool shownInEnemyList;

    // Start is called before the first frame update
    void Start()
    {
        movable = true;
    }

    public void RegisterEntities()
    {
        Debug.Log("Enemy Register: Started.");
        foreach (GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(pl.GetComponent<SinglePlayer>());
            Debug.Log("Enemy Register: Player Added: " + pl);
        }
    }
    void FixedUpdate()
    {
        Debug.Log("Enemy Fixed Update: " + movable);
        MovePerFrame();
    }

    private GameObject GetFirstAggroPlayer()
    {
        // int hi_aggro = 0;
        // for 
        // Return MT directly

        if (cachedMT == null)
        {
            // if mt is not dead
            foreach (SinglePlayer p in players)
            {
                Debug.Log("Check if " + p + " is MT: " + p.stratPosition);
                if (p.stratPosition == SinglePlayer.StratPosition.MT)
                {
                    cachedMT = p.gameObject;
                }
            }
        }
        Debug.Log("MT is " + cachedMT);
        return cachedMT;
    }

    void MovePerFrame()
    {
        // dont follow the dead players (if he fell off the edge)
        if (!movable) return;
        GameObject mt = GetFirstAggroPlayer();
        if (mt)
        {
            // if not null
            Vector3 towards = mt.transform.position - gameObject.transform.position;
            towards.y = 0;
            if (towards.magnitude < minAtkDistance) return;

            towards = towards.normalized;
            Debug.Log(this + " Move Towards " + mt + " " + towards);
            transform.Translate(towards * moveSpeed, Space.World);
        }
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
        Debug.Log("Enemy Apply effect: " + this);
        foreach (StatusGroup statusGroup in statusGroups)
        {
            statusGroup.ApplyEffect();
        }
    }
}

