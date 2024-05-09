using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertial = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertial).normalized;

        if (direction.magnitude >= 0.1f) {
            float targetAngule = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angule = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngule, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDiretion = (Quaternion.Euler(0f, targetAngule, 0f) * Vector3.forward).normalized;

            transform.rotation = Quaternion.Euler(0f, angule, 0f);
            controller.Move(speed * Time.deltaTime * moveDiretion);

            animator.SetBool("walking", true);
        } else {
            animator.SetBool("walking", false);
        }
    }
}
