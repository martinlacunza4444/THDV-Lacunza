using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 30; // Cantidad de munici�n que otorgar�

   private void OnTriggerEnter(Collider other)
   {
        if (other.CompareTag("Player"))
        {
            Gun gun = other.GetComponentInChildren<Gun>();
            if (gun != null)
            {
                gun.AddAmmo(ammoAmount);
                Destroy(gameObject); // Destruye el objeto de munici�n
             }
        }
   }
}