using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerDataSO", order = 1)]
public class PlayerDataSO: ScriptableObject
{
    public float speed = 5;
    public float jumpStrength = 2;
    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    public float maxHealth;
    public float currentHealth;

}
