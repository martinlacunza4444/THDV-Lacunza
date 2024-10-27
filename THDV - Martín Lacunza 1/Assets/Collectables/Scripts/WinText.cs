using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Ensure this namespace is used

public class WinText : MonoBehaviour
{
    public Collectable[] Collectable;
    public TextMeshProUGUI tmpText; // Use TextMeshProUGUI for UI text elements
    public int score;

    // Start is called before the first frame update
    void Start()
    {
       
        if (tmpText == null)
        {
            // Attempt to get the TextMeshProUGUI component if not assigned in the Inspector
            tmpText = GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tmpText != null && Collectable != null)
        {
            tmpText.text = "Score: " + score + " / " + Collectable.Length; // Update the text
        }
    }
}