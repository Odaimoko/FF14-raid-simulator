using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private GameObject animationFx;    // Prefab
    private AudioClip soundFx; // sound 
    public string moveName;

    private EnemyMoveType[] moveTypes; // Might be the combination of these types
    private GameObject areaOfEffect; //  the area of this move

    private GameObject[] target; // who to apply to


    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
