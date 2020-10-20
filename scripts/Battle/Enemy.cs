using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{



    //
    // ─── INFORMATION ────────────────────────────────────────────────────────────────
    //

    public List<SinglePlayer> players = new List<SinglePlayer>();

    //
    // ─── MOVEMENT ───────────────────────────────────────────────────────────────────
    //
    private Transform parentTransform;
    [SerializeField]
    public float moveSpeed = .05f;

    private Animator animator;

    //
    // ─── BATTLE ─────────────────────────────────────────────────────────────────────
    //

    public bool targetable = true, movable = true;
    private bool isBoss; // is boss or regular
    public EntityBattleStatus battleStatus
    {
        get
        {
            if (inBattle) return EntityBattleStatus.InBattle;
            else
            {
                if (dead)
                    return EntityBattleStatus.PlayerWin;
                else if (healthPoint == maxHP) return EntityBattleStatus.PreBattle;
                else return EntityBattleStatus.PlayerLose;
            }
        }
    }
    [SerializeField]
    private int normalAtkRawDamage;
    public Dictionary<GameObject, int> aggro = new Dictionary<GameObject, int>();
    public GameObject cachedMT { get; protected set; }
    public override GameObject target
    {
        get
        {
            GameObject go = GetFirstAggroPlayer();
            return go;
        }
        set
        {
            cachedMT = value;
        }
    }
    //
    // ─── UI ─────────────────────────────────────────────────────────────────────────
    //

    public bool shownInEnemyList;
    private Transform targetCircleGO;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targetCircleGO = transform.Find("target circle");
        parentTransform = transform.parent;
    }

    public void RegisterEntities()
    {
        players.Clear();
        Debug.Log("Enemy RegisterEntities Started.", this.gameObject);
        foreach (GameObject pl in GameObject.FindGameObjectsWithTag(Constants.BM.PlayerTag))
        {
            players.Add(pl.GetComponent<SinglePlayer>());
            Debug.Log($"Enemy RegisterEntities: Player Added: {pl}", this.gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!inBattle && DetectInBattle()) inBattle = true;
    }

    void FixedUpdate()
    {
        if (!dead && movable)
            MovePerFrame();
    }

    bool DetectInBattle()
    {
        // if more than one (inclusive) player is near this enemy
        foreach (SinglePlayer p in players)
        {
            if (!p.dead)
                if ((this.gameObject.transform.position - p.gameObject.transform.position).magnitude < Constants.Battle.InBattleDistance)
                {
                    return true;
                }
        }
        return false;
    }

    private GameObject GetFirstAggroPlayer()
    {
        int hi_aggro = 0;
        if (cachedMT == null || !cachedMT.GetComponent<SinglePlayer>().dead)
        {
            // if mt is not dead
            foreach (SinglePlayer p in players)
            {
                // Debug.Log($"GetFirstAggroPlayer: Enemy Aggro: Check if {p} is MT. Aggro: {aggro[p.gameObject]}. Position: {p.stratPosition}", this.gameObject);
                if (!p.dead)
                {
                    if (aggro[p.gameObject] > hi_aggro)
                    {
                        hi_aggro = aggro[p.gameObject];
                        cachedMT = p.gameObject;
                    }
                }
                else
                {
                    Debug.Log($"GetFirstAggroPlayer: Enemy Aggro:  {p} dead.");
                }
            }
        }
        // Debug.Log("GetFirstAggroPlayer: MT is " + cachedMT, this.gameObject);
        return cachedMT;
    }

    public void ResetMT()
    {
        cachedMT = null;
    }

    protected virtual void MovePerFrame()
    {
        // dont follow the dead players (if he fell off the edge)
        if (casting || !inBattle) return;
        GameObject mt = GetFirstAggroPlayer();
        if (mt)
        {
            // if not null
            Vector3 towards = mt.transform.position - parentTransform.position;
            towards.y = 0;
            // Rotate Target Circle
            float angle = Vector3.Angle(Vector3.forward, towards);
            if (towards.x > 0)
            {
                angle = 360 - angle;
            }
            Vector3 eulerAngle = targetCircleGO.transform.eulerAngles;
            eulerAngle.z = angle;
            eulerAngle.y = 0;
            targetCircleGO.transform.eulerAngles = eulerAngle;
            // move
            if (towards.magnitude < Constants.Battle.MinAtkDistance)
                return;

            towards = towards.normalized;
            parentTransform.Translate(towards * moveSpeed, Space.World);
            // Debug.Log($"Enemy Move: {this} Move Towards {mt}, Direction: {towards}. Angle {angle}", this.gameObject);
            // Debug.Log($"Tachie Circle  eulerAngle {eulerAngle}, {targetCircleGO.transform.eulerAngles}", this.gameObject);
        }
    }


    void CastingAtk()
    {

    }

    private void ResetVariable()
    {

    }

    public void OnBattleStart()
    {
        inBattle = true;
    }

    public override void OnDead()
    {
        base.OnDead();
    }
}

