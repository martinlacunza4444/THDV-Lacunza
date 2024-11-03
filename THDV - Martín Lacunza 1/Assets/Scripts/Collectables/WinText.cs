using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Importa el espacio de nombres para la gestión de escenas

public class WinText : MonoBehaviour
{
    public Collectable[] collectables; // Cambiado a plural
    public TextMeshProUGUI tmpText;
    public int score;
    public GameObject winCube; // Referencia al cubo donde se gana
    public float interactionDistance = 3f; // Distancia para interactuar
    private bool canWin = false; // Si se puede ganar

    void Start()
    {
        if (tmpText == null)
        {
            tmpText = GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        // Actualiza el texto de puntuación
        if (tmpText != null && collectables != null)
        {
            tmpText.text = "Plane Parts: " + score + " / " + collectables.Length;
        }

        // Verifica si el jugador está cerca del cubo y ha recolectado 3 objetos
        if (canWin && score >= 3 && Input.GetKeyDown(KeyCode.E))
        {
            RestartScene(); // Llama al método para reiniciar la escena
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto es el cubo específico
        if (other.gameObject == winCube)
        {
            canWin = true; // Permitir ganar
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si el jugador se aleja del cubo, desactivar la posibilidad de ganar
        if (other.gameObject == winCube)
        {
            canWin = false;
        }
    }

    public void CollectItem()
    {
        score++;
        // Aquí podrías desactivar o destruir el coleccionable
        if (score >= 3)
        {
            // Lógica adicional si necesitas cuando se recolectan 3 objetos
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene("SampleScene"); // Reinicia la escena llamada "SampleScene"
    }
}