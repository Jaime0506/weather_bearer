using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float horizontal;
    public float vertical;

    public float speed = 5f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public Transform cam;

    public float jumpForce = 5f; // Fuerza del salto

    private bool isJumping = false; // Bandera para controlar el salto

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngule = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angule = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngule, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDiretion = (Quaternion.Euler(0f, targetAngule, 0f) * Vector3.forward).normalized;

            transform.rotation = Quaternion.Euler(0f, angule, 0f);
            controller.Move(speed * Time.deltaTime * moveDiretion);

            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }

        if (controller.isGrounded)
        {
            // Salto cuando se presiona el botón y no está saltando
            if (Input.GetButton("Jump") && !isJumping)
            {
                isJumping = true;
                StartCoroutine(Jump());
            }
        } 
    }

    IEnumerator Jump()
    {
        float timer = 0f;

        while (timer < 0.5f) // Tiempo de salto
        {
            float jumpHeight = Mathf.Sqrt(2 * jumpForce * -Physics.gravity.y);
            controller.Move(Vector3.up * jumpHeight * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        isJumping = false;
    }
}