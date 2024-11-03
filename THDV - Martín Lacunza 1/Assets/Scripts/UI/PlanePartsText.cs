using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class PlanePartsTextScript: MonoBehaviour
{
    public GameObject PlanePartsText;

    public GameObject playerCamera;




    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlanePartsText.SetActive(true);


        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlanePartsText.SetActive(false);

        }
    }

}