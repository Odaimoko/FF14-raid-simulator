using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
public class capsuleAnimEvent : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.y < .5)
        {
            animator.SetBool("Y_lt_half", true);
        }
    }

    void hi_(int mgs)
    {
        Debug.Log(mgs);
    }
}
