using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public PlayerDataSO playerData;
    public int KeyAmount;
    public HealthBar healthBar;
  
    private void Start()
    {
        playerData.currentHealth = playerData.maxHealth;

        healthBar.SetSliderMax(playerData.maxHealth);
    }
    private void Update()
    {
        if (playerData.currentHealth > playerData.maxHealth)
        {
            playerData.currentHealth = playerData.maxHealth;
        }
        if (playerData.currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float amount)
    {
        playerData.currentHealth -= amount;
        healthBar.SetSlider(playerData.currentHealth);
    }
    public void HealPlayer(float amount)
    {
        playerData.currentHealth += amount;
        healthBar.SetSlider(playerData.currentHealth);
    }
    private void Die()
    {
        Debug.Log("You died!");

        // Reiniciar la escena
        SceneManager.LoadScene("SampleScene");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            KeyAmount += 1;
            Destroy(other.gameObject);
        }
    }
}