using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;
using System.Linq;


public class BattleManager : MonoBehaviour
{
    //
    // ─── GM ─────────────────────────────────────────────────────────────────────────
    //

    private GlobalGameManager gameManager;
    public enum BattleStatus
    {
        PreBattle,
        InBattle,
        PlayerWin,
        PlayerLose
    }
    public List<Enemy> enemies = new List<Enemy>();
    public List<SinglePlayer> players = new List<SinglePlayer>();
    // default MT
    public SinglePlayer controlledPlayer;
    public Scenario scenario;
    public BattleStatus prevBattleStatus = BattleStatus.PreBattle;
    public BattleStatus battleStatus
    {
        // Check enemies status
        // if all pre -> pre
        // if all post -> post
        // if more than 0 in battle -> in
        get
        {
            bool prebattle = true;
            bool enemyAllDead = true, playerAllDead = true;
            foreach (Enemy e in enemies)
            {
                if (!e.dead)
                    enemyAllDead = false;
                if (e.inBattle) prebattle = false;
            }
            foreach (SinglePlayer pl in players)
            {
                if (!pl.dead) playerAllDead = false;
                if (pl.inBattle) prebattle = false;
            }
            if (prebattle) return BattleStatus.PreBattle;
            if (enemyAllDead)
                return BattleStatus.PlayerWin;
            else if (playerAllDead)
                return BattleStatus.PlayerLose;
            else
                return BattleStatus.InBattle;
        }
    }

    // Pending Effects 
    private List<SingleStatus> eventQueue = new List<SingleStatus>();


    //
    // ─── BUFF DEBUFF MANAGEMENT ──────────────────────────────────────────────────────
    //


    //
    // ─── UI ─────────────────────────────────────────────────────────────────────────
    //

    private UIManager uIManager;
    private void Awake()
    {
        uIManager = GetComponent<UIManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // init a lot of things

        // Init scenario
        GameObject gm = GameObject.Find(Constants.GameSystem.GMObjectName);
        if (gm == null)
        {
            Debug.Log($"BM Start: No GM object found, stop initializing.");
            return;
        }
        gameManager = GameObject.Find("Global Manager GO").GetComponent<GlobalGameManager>();
        Type t = Constants.GameSystem.boss2meta[SupportedBoss.ShivaUnreal].scenarioType;
        var methods = typeof(GameObject).GetMethods().Where(m => m.Name == "AddComponent");
        foreach (MethodInfo methodInfo in methods)
        {
            if (methodInfo.IsGenericMethod)
            {
                MethodInfo method = methodInfo;
                MethodInfo generic = method.MakeGenericMethod(t);
                generic.Invoke(gameObject, null);
                break;
            }
        }
        scenario = GetComponent<Scenario>();
        scenario.Init(); // generate players/enemies, set up animation
        // TODO Init animation
        //  correctly link objects to animation.  
        // Only StartPhase When in Battle
        // scenario.StartPhase(gameManager.phase);
        StartCoroutine("RegisterStatusEffect");
        RegisterEntities();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBattleStatus();
        ApplyEventQueue();
        // if (battleStatus == BattleStatus.InBattle)
        // {
        //     ApplyEventQueue();
        // }
    }

    void RegisterEntities()
    {
        enemies.Clear();
        players.Clear();
        controlledPlayer = null;
        // Find enemies and players in the scene
        foreach (GameObject en in GameObject.FindGameObjectsWithTag(Constants.BM.EnemyTag))
        {
            enemies.Add(en.GetComponent<Enemy>());
        }
        foreach (GameObject pl in GameObject.FindGameObjectsWithTag(Constants.BM.PlayerTag))
        {
            SinglePlayer sp = pl.GetComponent<SinglePlayer>();
            if (sp.controller.controllable)
            {
                controlledPlayer = sp;
            }
            players.Add(sp);
        }
        Debug.Log("BM register enemy " + enemies.Count, this.gameObject);
        Debug.Log("BM register players " + players.Count, this.gameObject);
        foreach (Enemy e in enemies)
        {
            Debug.Log("BM register enemy " + e, this.gameObject);
            e.RegisterEntities();
        }
        foreach (SinglePlayer p in players)
            p.RegisterEntities();
    }

    public void AddStatusIconToUI()
    {
        // only called when a status is shown at the first time
        uIManager.OnStatusListChange();
    }

    public void AddEvent(SingleStatus status)
    {
        // Debug.Log($"BM AddEvent: {status}");
        eventQueue.Add(status);
    }

    void ApplyEventQueue()
    {
        // TODO: Lock?
        // Debug.Log("BM: Apply all effects.");
        foreach (SingleStatus s in eventQueue)
        {
            s.Apply();
        }
        eventQueue.Clear();
    }

    // called every Interval seconds
    IEnumerator RegisterStatusEffect()
    {
        while (true)
        {
            Debug.Log("BM RegisterStatusEffect", this.gameObject);
            foreach (Enemy e in enemies)
            {
                e.RegisterEffect();
            }
            foreach (SinglePlayer p in players)
            {
                p.RegisterEffect();
            }
            yield return new WaitForSeconds(Constants.Battle.StatusRegisterInterval);
        }
    }


    void CheckBattleStatus()
    {
        if (prevBattleStatus != battleStatus)
        {
            if (prevBattleStatus == BattleStatus.PreBattle && battleStatus == BattleStatus.InBattle)
                OnBattleStart();
            else if (prevBattleStatus == BattleStatus.InBattle && (battleStatus == BattleStatus.PlayerWin || battleStatus == BattleStatus.PlayerLose))
                OnBattleEnd();
            prevBattleStatus = battleStatus;
        }
    }

    // only deal with players' status
    public void OnBattleStart()
    {
        // pre->in
        // Start Scenario
        // Set active enemies' inbattle=true. this happens if only one enemy detects the player, but not the others.
        Debug.Log("BM OnBattleStart: All in Battle!!!", this.gameObject);
        foreach (Enemy e in enemies)
        {
            e.inBattle = true;
        }
        foreach (SinglePlayer p in players)
        {
            p.inBattle = true;
        }
    }

    public void OnBattleEnd()
    {
        // in->end
        // Stop all casting
        // UI transition
        // Reset CD, HP,
        // Reset Scenario
        // reset enemy status to prepare
        GameObject touchtext = GameObject.Find("TouchText");
        TextMeshProUGUI t = touchtext.GetComponent<TextMeshProUGUI>();

        if (battleStatus == BattleStatus.PlayerWin)
        {
            Debug.Log("BM OnBattleEnd: PlayerWin.");
            t.text = "PLAYER WIN";
        }
        else
        {
            Debug.Log("BM OnBattleEnd: PlayerLose.");
            t.text = "PLAYER LOSE";
        }
    }

}
