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
    public float moveSpeed = .05f, minAtkDistance = 3f;
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
    public Dictionary<GameObject, int> aggro = new Dictionary<GameObject, int>();
    public GameObject cachedMT { get; protected set; }
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
        Debug.Log("Enemy Register: Started.", this.gameObject);
        foreach (GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(pl.GetComponent<SinglePlayer>());
            Debug.Log($"Enemy Register: Player Added: {pl}", this.gameObject);
        }
    }
    void FixedUpdate()
    {
        // Debug.Log("Enemy Fixed Update: " + movable, this.gameObject);
        MovePerFrame();
    }

    private GameObject GetFirstAggroPlayer()
    {
        int hi_aggro = 0;
        // Return MT directly
        if (cachedMT == null)
        {
            // if mt is not dead
            foreach (SinglePlayer p in players)
            {
                Debug.Log($"Enemy Aggro: Check if {p} is MT. Aggro: {aggro[p.gameObject]}. Position: {p.stratPosition}", this.gameObject);
                if (!p.dead)
                {
                    if (aggro[p.gameObject] > hi_aggro)
                    {
                        cachedMT = p.gameObject;
                    }
                }
            }
        }
        Debug.Log("MT is " + cachedMT, this.gameObject);
        return cachedMT;
    }

    public void ResetMT()
    {
        cachedMT = null;
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
            Debug.Log($"Enemy Move: {this} Move Towards {mt}, Direction: {towards}", this.gameObject);
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
        Debug.Log($"Enemy Apply effect: {this}", this.gameObject);
        foreach (StatusGroup statusGroup in statusGroups)
        {
            statusGroup.ApplyEffect();
        }
    }
}

