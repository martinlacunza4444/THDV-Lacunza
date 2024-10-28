using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; 
    private bool IsInPuase = false;
    public FirstPersonLook fpsLook;
    public Jump playerJump;
    public Crouch playerCrouch;
    public Gun playerGun;
    public Zoom playerZoom;
    public FirstPersonAudio playerAudio;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsInPuase = !IsInPuase;
            pauseMenu.SetActive(IsInPuase);       

            if(IsInPuase)
            {
                fpsLook.enabled = false;
                playerJump.enabled = false;
                playerCrouch.enabled = false;
                playerGun.enabled = false;
                playerZoom.enabled = false;
                playerAudio.enabled = false;
                Time.timeScale = 0;
            }
            else
            {
                fpsLook.enabled = true;
                playerJump. enabled = true;
                playerCrouch.enabled = true;
                playerGun.enabled = true;
                playerAudio.enabled = true;
                playerZoom.enabled = true;
                Time.timeScale = 1;
            }
        }
    }
}