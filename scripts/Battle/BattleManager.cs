using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Enemy[] enemies;
    public SinglePlayer[] players;
    // private Queue effectQueue; // Pending Effects 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ApplyStatusEffect Started");
        StartCoroutine("ApplyStatusEffect");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ApplyStatusEffect()
    {
        while (true)
        {
            Debug.Log("BattleManager ApplyStatusEffect Enter");
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
