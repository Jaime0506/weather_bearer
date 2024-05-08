using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransitions : MonoBehaviour
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
        if(Input.GetKey("w")) {
            animator.SetBool("walking", true);
        } else {
            animator.SetBool("walking", false);
        }
    }
}
