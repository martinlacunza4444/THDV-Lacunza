using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGun : MonoBehaviour
{
    public Gun gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        // Inicializar el arma como no equipada
        ResetPickupState();
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
            transform.SetParent(null);
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) 
        {
            PickUp();
        }

        if (equipped && Input.GetKeyDown(KeyCode.Q) && slotFull) 
        {
            Drop();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        // Posicionar y rotar el arma en el contenedor del jugador
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero; // Ajustar según sea necesario
        transform.localRotation = Quaternion.Euler(Vector3.zero); // Ajustar según sea necesario
        transform.localScale = Vector3.one;

        rb.isKinematic = true; // Hacer que el Rigidbody sea cinemático
        coll.isTrigger = true;

        gunScript.enabled = true;
        gunScript.Equip(); // Activa la UI del arma al recogerla
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null); // Soltar el arma

        rb.isKinematic = false; // Hacer que el Rigidbody no sea cinemático
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse); 
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        gunScript.enabled = false;
        gunScript.Unequip(); // Desactiva la UI del arma al soltarla
    }

    private void ResetPickupState()
    {
        equipped = false;
        slotFull = false;
        rb.isKinematic = false;
        coll.isTrigger = false;
    }
}