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
}
