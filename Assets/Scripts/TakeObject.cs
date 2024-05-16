using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObject : MonoBehaviour
{
    public GameObject handPoint;
    private GameObject pickedObject = null;
    public GameObject TextDetected;

    private void Start()
    {
        TextDetected.SetActive(false);
    }

    private void Update()
    {
        if (pickedObject != null)
        {
            if (Input.GetKey("r"))
            {
                ReleaseObject();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Object") && pickedObject == null)
        {
            TextDetected.SetActive(true);

            if (Input.GetKey("e"))
            {
                PickUpObject(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Object") && pickedObject != null)
        {
            TextDetected.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            TextDetected.SetActive(false);
        }
    }

    private void PickUpObject(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().useGravity = false;
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.position = handPoint.transform.position;
        obj.transform.SetParent(handPoint.transform);
        pickedObject = obj;
        TextDetected.SetActive(false);
    }

    private void ReleaseObject()
    {
        pickedObject.GetComponent<Rigidbody>().useGravity = true;
        pickedObject.GetComponent<Rigidbody>().isKinematic = false;
        pickedObject.transform.SetParent(null);
        pickedObject = null;
    }
}