using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyDataSO", order = 1)]
public class EnemyDataSO: ScriptableObject
{
    public float StartHealth;
    public int damage = 10; // Daño que hace el enemigo
    public float attackCooldown = 1f; // Tiempo entre ataques
    public float speed;
}