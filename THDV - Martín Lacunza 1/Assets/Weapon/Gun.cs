using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public TextMeshProUGUI ammoDisplay; // Referencia al texto de munición
    private bool isReloading = false;
    public  GunDataSO gunData;
    public Camera fpscamera;
    public Animator animator;

    private float nextTimeToFire = 0f;

    void Start()
    {
        gunData.currentAmmo = gunData.maxAmmo;
        UpdateAmmoUI(); // Actualiza la UI al inicio
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        // Permitir recargar si hay balas en la reserva
        if (Input.GetKeyDown(KeyCode.R) && gunData.reserveAmmo > 0)
        {
            StartCoroutine(Reload());
            return; // Salimos aquí para evitar disparar
        }

        // Si no hay munición, no se puede disparar
        if (gunData.currentAmmo <= 0)
        {
            Debug.Log("Sin munición. No puedes disparar.");
            return; // Sale del método Update si no hay munición
        }

        // Disparar si hay munición
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / gunData.fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(gunData.reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(1f);

        // Rellenar el cargador desde la reserva
        int ammoNeeded = gunData.maxAmmo - gunData.currentAmmo;
        if (gunData.reserveAmmo >= ammoNeeded)
        {
            gunData.currentAmmo += ammoNeeded;
            gunData.reserveAmmo -= ammoNeeded;
        }
        else
        {
            gunData.currentAmmo += gunData.reserveAmmo; // Usar toda la reserva si no hay suficiente
            gunData.reserveAmmo = 0;
        }

        UpdateAmmoUI(); // Actualiza la UI al recargar
        isReloading = false;
    }

    void Shoot()
    {
        if (gunData.currentAmmo > 0) // Asegúrate de que todavía haya munición
        {
            gunData.currentAmmo--;
            Debug.Log("Disparo realizado. Munición actual: " + gunData.currentAmmo); // Log para depuración
            UpdateAmmoUI(); // Actualiza la UI al disparar

            RaycastHit hit;
            if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, gunData.range))
            {
                Debug.Log(hit.transform.name);
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(gunData.damage);
                }
            }
        }
    }

    private void UpdateAmmoUI()
    {
        ammoDisplay.text = $"{gunData.currentAmmo} / {gunData.reserveAmmo}"; // Actualiza el texto de la UI
    }

    public void AddAmmo(int amount)
    {
        gunData.reserveAmmo += amount;
        UpdateAmmoUI(); // Actualiza la UI para mostrar la nueva munición
        Debug.Log("Munición de reserva aumentada. Reserva actual: " + gunData.reserveAmmo);
    }
}