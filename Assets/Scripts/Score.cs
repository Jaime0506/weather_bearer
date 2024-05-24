using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public float score;
    public TextMeshProUGUI score_text;

    public void incrementScore() {
        score++;
    }

    private void Update() {
        score_text.text = "SCORE: " + score.ToString();
    }
}
