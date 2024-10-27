using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	private Renderer objectRenderer;
    private Collider objectCollider;
    public int score;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
	   if (other.gameObject.CompareTag("Player"))
	   {
		   other.GetComponent<WinText>().score++;
           Destroy(gameObject);
	   }
    }
}
