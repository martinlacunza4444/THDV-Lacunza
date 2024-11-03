using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public TextMeshProUGUI ammoDisplay; // Referencia al texto de munici�n
    private bool isReloading = false;
    public GunDataSO gunData;
    public Camera fpscamera;
    public Animator animator;

    private float nextTimeToFire = 0f;

    void Start()
    {
        gunData.currentAmmo = gunData.maxAmmo; // Inicializa currentAmmo
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

        // Permitir recargar si hay balas en la reserva y currentAmmo no est� en su m�ximo
        if (Input.GetKeyDown(KeyCode.R) && gunData.reserveAmmo > 0 && gunData.currentAmmo < gunData.maxAmmo)
        {
            StartCoroutine(Reload());
            return; // Salimos aqu� para evitar disparar
        }

        // Si no hay munici�n, disparar deber�a recargar autom�ticamente
        if (gunData.currentAmmo <= 0)
        {
            Debug.Log("Sin munici�n. Recargando autom�ticamente.");
            StartCoroutine(Reload());
            return; // Sale del m�todo Update si no hay munici�n
        }

        // Disparar si hay munici�n
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
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
            gunData.currentAmmo += ammoNeeded; // Solo actualiza currentAmmo
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

    public void Equip() // M�todo para equipar el arma
    {
        ammoDisplay.gameObject.SetActive(true); // Activa la UI del arma
        UpdateAmmoUI(); // Actualiza la UI al equipar
        Debug.Log("Arma equipada."); // Mensaje de depuraci�n
    }

    public void Unequip() // M�todo para desequipar el arma
    {
        ammoDisplay.gameObject.SetActive(false); // Desactiva la UI al desequipar
        Debug.Log("Arma desequipada."); // Mensaje de depuraci�n
    }

    public void AddAmmo(int amount)
    {
        if (gameObject.activeInHierarchy) // Aseg�rate de que el arma est� activa
        {
            // Solo actualizar la reserva
            gunData.reserveAmmo += amount;

            UpdateAmmoUI(); // Actualiza la UI para mostrar la nueva munici�n
            Debug.Log("Munici�n de reserva aumentada. Reserva actual: " + gunData.reserveAmmo);
        }
    }
}