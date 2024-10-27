using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public float fireRate = 15f;
    public TextMeshProUGUI ammoDisplay; // Referencia al texto de munición
    public int maxAmmo = 30; // Máximo de munición en el cargador
    private int currentAmmo; // Munición actual en el cargador
    public int reserveAmmo = 60; // Munición de reserva
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpscamera;
    public Animator animator;

    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
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
        if (Input.GetKeyDown(KeyCode.R) && reserveAmmo > 0)
        {
            StartCoroutine(Reload());
            return; // Salimos aquí para evitar disparar
        }

        // Si no hay munición, no se puede disparar
        if (currentAmmo <= 0)
        {
            Debug.Log("Sin munición. No puedes disparar.");
            return; // Sale del método Update si no hay munición
        }

        // Disparar si hay munición
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(1f);

        // Rellenar el cargador desde la reserva
        int ammoNeeded = maxAmmo - currentAmmo;
        if (reserveAmmo >= ammoNeeded)
        {
            currentAmmo += ammoNeeded;
            reserveAmmo -= ammoNeeded;
        }
        else
        {
            currentAmmo += reserveAmmo; // Usar toda la reserva si no hay suficiente
            reserveAmmo = 0;
        }

        UpdateAmmoUI(); // Actualiza la UI al recargar
        isReloading = false;
    }

    void Shoot()
    {
        if (currentAmmo > 0) // Asegúrate de que todavía haya munición
        {
            currentAmmo--;
            Debug.Log("Disparo realizado. Munición actual: " + currentAmmo); // Log para depuración
            UpdateAmmoUI(); // Actualiza la UI al disparar

            RaycastHit hit;
            if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }

    private void UpdateAmmoUI()
    {
        ammoDisplay.text = $"{currentAmmo} / {reserveAmmo}"; // Actualiza el texto de la UI
    }

    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;
        UpdateAmmoUI(); // Actualiza la UI para mostrar la nueva munición
        Debug.Log("Munición de reserva aumentada. Reserva actual: " + reserveAmmo);
    }
}