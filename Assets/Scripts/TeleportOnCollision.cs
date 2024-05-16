using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que se colisionó es la caja 3D
        if (other.CompareTag("TeleportBox"))
        {
            Debug.Log("Deberia tp");
            // Teletransporta al personaje a la posición (0, 0, 0)
            characterController.enabled = false; // Desactiva el CharacterController antes de mover
            transform.position = new Vector3(46.58f, 25.28638f, 470.12f); // Cambia la posición del personaje
            characterController.enabled = true; // Vuelve a activar el CharacterController
        }
    }
}
