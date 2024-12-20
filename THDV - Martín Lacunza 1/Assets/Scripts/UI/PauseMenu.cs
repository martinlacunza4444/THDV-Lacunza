using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; 
    private bool IsInPause = false;
    public FirstPersonLook fpsLook;
    public Jump playerJump;
    public Crouch playerCrouch;
    public Zoom playerZoom;
    public FirstPersonAudio playerAudio;

    // Referencia al GameObject que deseas desactivar/activar
    public GameObject targetGameObject; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsInPause = !IsInPause;
            pauseMenu.SetActive(IsInPause);       

            if (IsInPause)
            {
                // Desactivar los componentes del jugador
                fpsLook.enabled = false;
                playerJump.enabled = false;
                playerCrouch.enabled = false;
                playerZoom.enabled = false;
                // Desactivar el objeto completo que contiene el audio
                if (playerAudio != null)
                {
                    playerAudio.gameObject.SetActive(false);
                }
                Time.timeScale = 0;

                // Desactivar el GameObject
                if (targetGameObject != null)
                {
                    targetGameObject.SetActive(false);
                }
            }
            else
            {
                // Reactivar los componentes del jugador
                fpsLook.enabled = true;
                playerJump.enabled = true;
                playerCrouch.enabled = true;
                playerZoom.enabled = true;
                // Reactivar el objeto completo que contiene el audio
                if (playerAudio != null)
                {
                    playerAudio.gameObject.SetActive(true);
                }
                Time.timeScale = 1;

                // Activar el GameObject
                if (targetGameObject != null)
                {
                    targetGameObject.SetActive(true);
                }
            }
        }
    }
}