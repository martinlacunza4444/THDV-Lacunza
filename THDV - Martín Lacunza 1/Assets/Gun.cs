using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public float fireRate = 15f;
    public TextMeshProUGUI ammoDisplay; // Referencia al texto de munici�n
    public int maxAmmo = 30; // M�ximo de munici�n en el cargador
    private int currentAmmo; // Munici�n actual en el cargador
    public int reserveAmmo = 60; // Munici�n de reserva
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
            return; // Salimos aqu� para evitar disparar
        }

        // Si no hay munici�n, no se puede disparar
        if (currentAmmo <= 0)
        {
            Debug.Log("Sin munici�n. No puedes disparar.");
            return; // Sale del m�todo Update si no hay munici�n
        }

        // Disparar si hay munici�n
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
        if (currentAmmo > 0) // Aseg�rate de que todav�a haya munici�n
        {
            currentAmmo--;
            Debug.Log("Disparo realizado. Munici�n actual: " + currentAmmo); // Log para depuraci�n
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
        UpdateAmmoUI(); // Actualiza la UI para mostrar la nueva munici�n
        Debug.Log("Munici�n de reserva aumentada. Reserva actual: " + reserveAmmo);
    }
}