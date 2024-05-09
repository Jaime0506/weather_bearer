using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;

    [Header("Movement")]
    [SerializeField]
    public float speed = 3f;
    [SerializeField]
    public float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    void movePlayer() {

        // Retorna 1 si se preciona D y -1 con A en el eje X
        // y lo mismo en el eje Y
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Se usa 0 en Y, ya que Z es la que se encarga de controlar el movimiento
        // sobre el plano, Y en cambio sube o baja su altura
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        movementDirection.Normalize();

        transform.position = transform.position + (speed * Time.deltaTime * movementDirection);
        if (movementDirection != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), rotationSpeed * Time.deltaTime);

        if(horizontalInput != 0 || verticalInput != 0) {
            animator.SetBool("walking", true);
        } else {
            animator.SetBool("walking", false);
        }
    }
}
