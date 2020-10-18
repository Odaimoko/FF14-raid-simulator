using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    private GlobalGameManager gameManager;
    public Dictionary<GameObject, int> aggro = new Dictionary<GameObject, int>();
    protected List<bool> playersArrived = new List<bool>();

    public virtual void Init()
    {
        gameManager = GameObject.Find("Global Manager GO").GetComponent<GlobalGameManager>();
        GenerateEntities();
        SetAggro();
        for (int i = 0; i < Constants.Battle.NumPlayers; i++)
        {
            playersArrived.Add(false);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    public virtual void GenerateEntities()
    {
        Camera battleCam = GameObject.Find("battle main camera").GetComponent<Camera>();
        // Init Controlled Player 
        GameObject playerPrefab = Resources.Load<GameObject>(Constants.Battle.PlayerPrefabPath);
        for (int i = 0; i < Constants.Battle.NumPlayers; i++)
        {
            GameObject pObj = Instantiate(playerPrefab, new Vector3(3, 1, -4), Quaternion.identity);
            // Debug.Log($"Scenario GenerateEntities: Generate Player {i} in Scene {pObj.scene.name}.");
            pObj.GetComponent<ControllerSystem>().mainCam = battleCam;
            SinglePlayer p = pObj.transform.Find("Player 0").GetComponent<SinglePlayer>();
            p.stratPosition = (SinglePlayer.StratPosition)i;
            p.gameObject.name = Constants.Battle.PlayerGOPrefix + p.stratPosition.ToString();
            pObj.name = $"{Constants.Battle.PlayerGOPrefix} parent {p.stratPosition.ToString()}";
            // make the canvas face the camera
            p.moveInfoCanvas.worldCamera = battleCam;
            // Set Position text
            Transform stratPosTextGO = p.moveInfoCanvas.transform.Find("strat pos text");
            TextMeshProUGUI text = stratPosTextGO.GetComponent<TextMeshProUGUI>();
            text.text = p.stratPosition.ToString();
            text.color = Constants.Battle.GetPosColor(p.stratPosition);
            SceneManager.MoveGameObjectToScene(pObj, SceneManager.GetSceneByName("Battle"));
            if (p.stratPosition == gameManager.playerPos)
            {
                p.controllable = true;
                controlledPlayer = p;
            }
            else
                p.controllable = false;
            players.Add(p);
        }
    }


    protected virtual void SetAggro()
    {
        Debug.Log("Scenario Set Aggro.", this.gameObject);
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
    }

    protected bool MovePlayerToDestination(SinglePlayer player, Vector3 destination)
    {
        if (player.dead)
        {
            Debug.Log($"Scenario MovePlayerToDestination: {player.name} dead, cannot move it.");
            return true; // arrived at heaven
        }
        return players[4].controller.MoveToPoint(destination);

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
