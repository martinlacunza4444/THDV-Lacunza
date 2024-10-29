using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public TextMeshProUGUI ammoDisplay; // Referencia al texto de munici�n
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
            return; // Salimos aqu� para evitar disparar
        }

        // Si no hay munici�n, no se puede disparar
        if (gunData.currentAmmo <= 0)
        {
            Debug.Log("Sin munici�n. No puedes disparar.");
            return; // Sale del m�todo Update si no hay munici�n
        }

        // Disparar si hay munici�n
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
        if (gunData.currentAmmo > 0) // Aseg�rate de que todav�a haya munici�n
        {
            gunData.currentAmmo--;
            Debug.Log("Disparo realizado. Munici�n actual: " + gunData.currentAmmo); // Log para depuraci�n
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
        UpdateAmmoUI(); // Actualiza la UI para mostrar la nueva munici�n
        Debug.Log("Munici�n de reserva aumentada. Reserva actual: " + gunData.reserveAmmo);
    }
}