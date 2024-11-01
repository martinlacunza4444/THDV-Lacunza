using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public AmmoDataSO ammoData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Busca el arma equipada en el jugador
            Gun gun = other.GetComponentInChildren<Gun>();
            if (gun != null)
            {
                gun.AddAmmo(ammoData.ammoAmount); // A�ade munici�n solo al arma equipada
                Destroy(gameObject); // Destruye el objeto de munici�n
            }
        }
    }
}