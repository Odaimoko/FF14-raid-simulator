using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    // How a battle goes
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public List<Enemy> enemies = new List<Enemy>();
    public List<SinglePlayer> players = new List<SinglePlayer>();
    protected List<BattlePhase> phases;
    protected SinglePlayer controlledPlayer;
    protected BattlePhase currentPhase;
    protected Animator animator;
    // Start is called before the first frame update
    public virtual void Init()
    {
        // Init Enemy and Player
        GenerateEntities();
        RegisterEntities();
        SetAggro();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public virtual void GenerateEntities()
    {

    }
    protected virtual void RegisterEntities()
    {
        // Find enemies and players in the scene, should be called after entity generation
        enemies.Clear();
        players.Clear();
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
    }

    protected virtual void SetAggro()
    {
        Debug.Log("Scenario Set Aggro.", this.gameObject);
        Dictionary<GameObject, int> aggro = new Dictionary<GameObject, int>();
        foreach (SinglePlayer p in players)
        {
            int agg = 0;
            switch (p.stratPosition)
            {
                case SinglePlayer.StratPosition.MT:
                    agg = 1000000;
                    break;
                case SinglePlayer.StratPosition.ST:
                    agg = 600000;
                    break;
                case SinglePlayer.StratPosition.D4:
                case SinglePlayer.StratPosition.D1:
                    agg = 100000;
                    break;
                case SinglePlayer.StratPosition.D2:
                case SinglePlayer.StratPosition.D3:
                    agg = 60000;
                    break;
                case SinglePlayer.StratPosition.H1:
                    agg = 50000;
                    break;
                case SinglePlayer.StratPosition.H2:
                    agg = 30000;
                    break;

                default:
                    agg = 0;
                    break;
            }
            try
            {
                aggro.Add(p.gameObject, agg);
            }
            catch (System.ArgumentException)
            {
                aggro[p.gameObject] = agg;
            }
        }
        foreach (Enemy e in enemies)
        {
            Debug.Log($"Scenario: Init Aggro {e}.");
            e.aggro = new Dictionary<GameObject, int>(aggro);
        }
    }


    public virtual void StartPhase(BattlePhase phase = null)
    {
        // TODO: init actor's statuses
        //  Set animation   
        // Assign actors to scenario animation clip
    }

    public void _StartPhase(BattlePhase phase = null)
    {
        if (phase != null)
        {
            Debug.Log($"Scenario {this.name} StartPhase: {phase.name}");
            StartPhase(phase);
        }
        else
        {
            Debug.Log($"Scenario StartPhase: Phase is Null.");
        }
    }

    public virtual void StopPhase()
    {
        // Stop current phase
        Debug.Log($"Scenario Stop: {currentPhase.name}");
    }
}
