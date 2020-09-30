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
    // Start is called before the first frame update
    void Start()
    {
        // Init Enemy and Player
        GenerateEntities();
        RegisterEntities();
        SetAggro();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void GenerateEntities()
    {

    }
    void RegisterEntities()
    {
        // Find enemies and players in the scene
        foreach (GameObject en in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(en.GetComponent<Enemy>());
        }
        foreach (GameObject en in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(en.GetComponent<SinglePlayer>());
        }
    }

    protected virtual void SetAggro()
    {
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
            e.aggro = new Dictionary<GameObject, int>(aggro);
        }
    }
}
