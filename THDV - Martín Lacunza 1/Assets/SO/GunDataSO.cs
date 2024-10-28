using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "ScriptableObjects/GunDataSO", order = 1)]
public class GunDataSO: ScriptableObject
{
    public int maxAmmo = 30; // Máximo de munición en el cargador
    public int currentAmmo; // Munición actual en el cargador
    public int reserveAmmo = 60; // Munición de reserva
    public float reloadTime = 1f;
    public int damage = 10;
    public float range = 100f;
    public float fireRate = 15f;
}

