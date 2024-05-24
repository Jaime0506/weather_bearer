using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogScript : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public string[] lines;
    public float textSpeed = 0.1f;
    int index;

    private void Start()
    {
        dialogText.text = string.Empty;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogText.text = lines[index];
            }
        }
    }

    public void StartDialog()
    {
        index = 0;
        dialogText.text = string.Empty;
        gameObject.SetActive(true);
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            gameObject.SetActive(false);
            FindObjectOfType<DetectNPC>().EndDialog(); // Notificar a DetectNPC cuando termine el diálogo
        }
    }

    public void StopDialog() {
        gameObject.SetActive(false);
        FindObjectOfType<DetectNPC>().EndDialog(); // Notificar a DetectNPC cuando termine el diálogo
    }
}
