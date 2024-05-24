using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectNPC : MonoBehaviour
{
    public GameObject InteractivityNPCCanvas;
    public GameObject IgnoreNPCCanvas;
    public DialogScript dialogScript; // Referencia al script de diálogo
    bool isInteractingWithNPC;
    private bool canInteract = true;
    public float debounceTime = 0.5f; // Tiempo de debounce en segundos

    private void Start()
    {
        isInteractingWithNPC = false;
        IgnoreNPCCanvas.SetActive(false);
        InteractivityNPCCanvas.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        // Cuando estoy en frente del NPC
        if (other.gameObject.CompareTag("NPC"))
        {
            if (!isInteractingWithNPC)
            {
                InteractivityNPCCanvas.SetActive(true);
            }
            else
            {
                InteractivityNPCCanvas.SetActive(false);
                IgnoreNPCCanvas.SetActive(true);
            }

            if (Input.GetButtonDown("Interactive") && canInteract)
            {
                canInteract = false; // Deshabilitar la interacción temporalmente
                isInteractingWithNPC = !isInteractingWithNPC;

                if (isInteractingWithNPC)
                {
                    InteractivityNPCCanvas.SetActive(false);
                    IgnoreNPCCanvas.SetActive(true);
                    InteractiveWithNPC(other.gameObject);
                }
                else
                {
                    IgnoreNPCCanvas.SetActive(false);
                    StopInteractingWithNPC(other.gameObject);
                }

                StartCoroutine(Debounce());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            InteractivityNPCCanvas.SetActive(false);
            IgnoreNPCCanvas.SetActive(false);
            isInteractingWithNPC = false;
            StopInteractingWithNPC(other.gameObject);
        }
    }

    private void InteractiveWithNPC(GameObject npc)
    {
        npc.GetComponent<MoveNPC>().SetWaitAndLookAtPlayer(transform.position);
        dialogScript.StartDialog(); // Iniciar el diálogo
    }

    private void StopInteractingWithNPC(GameObject npc)
    {
        npc.GetComponent<MoveNPC>().ResumeActivities();
        dialogScript.StopDialog();
    }

    public void EndDialog()
    {
        isInteractingWithNPC = false;
        IgnoreNPCCanvas.SetActive(false);
    }

    private IEnumerator Debounce()
    {
        yield return new WaitForSeconds(debounceTime);
        canInteract = true;
    }
}
