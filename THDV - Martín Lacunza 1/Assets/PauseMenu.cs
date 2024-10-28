using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; 
    private bool IsInPuase = false;
    public FirstPersonLook fpsLook;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsInPuase = !IsInPuase;
            pauseMenu.SetActive(IsInPuase);       

            if(IsInPuase)
            {
                fpsLook.enabled = false;
                Time.timeScale = 0;
            }
            else
            {
                fpsLook.enabled = true;
                Time.timeScale = 1;
            }
        }
    }
}