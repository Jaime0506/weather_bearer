using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    public float velMovement, velRotation, lookDistance;
    public Animator animator;
    float delay_time = 1f;
    int movement;

    bool wait, walk, rotate, isInteractingWithPlayer = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Actions();
    }
    private void Update()
    {
        if (isInteractingWithPlayer) return;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, lookDistance))
        {
            if (hit.collider)
            {
                rotate = true;
                StartCoroutine(timeRotate());
            }
        }

        if (wait)
        {
            animator.SetBool("walk", false);
        }

        if (walk)
        {
            animator.SetBool("walk", true);
            transform.position += transform.forward * velMovement * Time.deltaTime;
        }

        if (rotate)
        {
            transform.Rotate(Vector3.up * velRotation * Time.deltaTime);
        }
    }

    void Actions()
    {
        if (isInteractingWithPlayer) return;

        movement = Random.Range(1, 4);

        if (movement == 1)
        {
            walk = true;
            wait = false;
        }

        if (movement == 2)
        {
            wait = true;
            walk = false;
        }

        if (movement == 3)
        {
            rotate = true;
            StartCoroutine(timeRotate());
        }

        Invoke("Actions", delay_time);
    }

    IEnumerator timeRotate()
    {
        yield return new WaitForSeconds(2);
        rotate = false;
    }

    public void SetWaitAndLookAtPlayer(Vector3 playerPosition)
    {
        wait = true;
        walk = false;
        rotate = false;

        animator.SetBool("walk", false);
        isInteractingWithPlayer = true;

        Vector3 direction = (playerPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * velRotation);
    }

    public void ResumeActivities()
    {
        isInteractingWithPlayer = false;
        Actions();
    }
}
