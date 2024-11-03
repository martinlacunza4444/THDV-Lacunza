using UnityEngine;

public class ObjectToggle : MonoBehaviour
{
    public GameObject[] objectsToToggle; // Array de objetos a ocultar
    public GameObject redOverlay; // Panel rojo de UI

    private void Update()
    {
        // Comprueba si se mantiene presionada la tecla "F"
        if (Input.GetKey(KeyCode.F))
        {
            ToggleObjects(false); // Ocultar objetos
            SetRedOverlay(true); // Mostrar overlay rojo
        }
        else
        {
            ToggleObjects(true); // Mostrar objetos
            SetRedOverlay(false); // Ocultar overlay rojo
        }
    }

    private void ToggleObjects(bool show)
    {
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(show); // Activa o desactiva el objeto
        }
    }

    private void SetRedOverlay(bool show)
    {
        if (redOverlay != null)
        {
            redOverlay.SetActive(show); // Activa o desactiva el panel rojo
        }
    }
}