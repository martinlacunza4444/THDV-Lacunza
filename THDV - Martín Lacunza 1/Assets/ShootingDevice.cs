using UnityEngine;

public class ShootingDevice : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform bulletSpawnPoint; // Punto donde se generarán las balas
    public float shootingInterval = 0.5f; // Intervalo entre disparos

    private void Start()
    {
        // Comienza a disparar
        InvokeRepeating(nameof(Shoot), 0f, shootingInterval);
    }

    private void Shoot()
    {
        // Instancia una nueva bala
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawnPoint.forward * 10f, ForceMode.Impulse); // Cambia la fuerza según sea necesario
    }
}
