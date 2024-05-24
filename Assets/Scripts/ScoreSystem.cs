using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public GameObject objScore;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            objScore.GetComponent<Score>().incrementScore();
            Destroy(gameObject);
        }
    }
}
