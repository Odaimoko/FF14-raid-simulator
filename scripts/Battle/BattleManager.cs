using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public List<SinglePlayer> players = new List<SinglePlayer>();
    public Scenario scenario;
    // private Queue effectQueue; // Pending Effects 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ApplyStatusEffect");
        scenario = GetComponent<Scenario>();
        RegisterEntities();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO Check if somebody's dead, reset Enemy's cached MT
        
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
    IEnumerator ApplyStatusEffect()
    {
        while (true)
        {
            Debug.Log("BattleManager ApplyStatusEffect Enter", this.gameObject);
            foreach (Enemy e in enemies)
            {
                e.ApplyEffect();
            }
            foreach (SinglePlayer p in players)
            {
                p.ApplyEffect();
            }
            yield return new WaitForSeconds(3f);
        }

    }
}
