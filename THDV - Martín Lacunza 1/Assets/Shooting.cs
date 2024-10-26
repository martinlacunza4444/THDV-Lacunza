using System;
using System.Diagnostics;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour{


    public int damage = 10;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public TextMeshProUGUI ammoDisplay;
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpscamera;

    public Animator animator;

    private float nextTimeToFire = 0f;

    void Start ()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable ()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    // Update is called once per frame
    void Update ()
    {
        if (isReloading)
        {
            return;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) 
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload ()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(1f);


        currentAmmo = maxAmmo;
        isReloading = false;
    }


    void Shoot ()
    {

        currentAmmo--;
        ammoDisplay.text = maxAmmo.ToString();
        RaycastHit hit;
        if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
