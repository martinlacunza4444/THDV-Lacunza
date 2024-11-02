using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Cargar la siguiente escena
    }

    public void QuitGame()
    {
        Application.Quit(); // Cerrar la aplicaci�n
    }

    public void ControlsGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); // Cargar la escena de controles
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0); // Cargar la escena 0
    }
}