using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Animator SecretDoor; // Asigna el Animator de la puerta en el Inspector
    public float interactionDistance = 3f; // Distancia para interactuar
    public GameObject SecretButton;
    private GameObject player;

    private void Start()
    {
        // Busca el objeto jugador por su etiqueta
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        // Verifica si el jugador está presionando "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Verifica si el jugador está cerca del cubo
            if (player != null && Vector3.Distance(player.transform.position, transform.position) <= interactionDistance)
            {
                // Ejecuta la animación de abrir la puerta
                SecretDoor.SetTrigger("Open"); // Asegúrate de tener un trigger llamado "Open" en el Animator
            }
        }
    }
}