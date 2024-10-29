using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
   public AmmoDataSO ammoData;

   private void OnTriggerEnter(Collider other)
   {
        if (other.CompareTag("Player"))
        {
            Gun gun = other.GetComponentInChildren<Gun>();
            if (gun != null)
            {
                gun.AddAmmo(ammoData.ammoAmount);
                Destroy(gameObject); // Destruye el objeto de munición
             }
        }
   }
}