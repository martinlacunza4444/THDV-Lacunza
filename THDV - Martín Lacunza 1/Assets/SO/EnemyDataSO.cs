using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyDataSO", order = 1)]
public class EnemyDataSO: ScriptableObject
{
    public float health;
    public int damage = 10; // Daño que hace el enemigo
    public float attackCooldown = 1f; // Tiempo entre ataques
    public float nextAttackTime = 0f;
}