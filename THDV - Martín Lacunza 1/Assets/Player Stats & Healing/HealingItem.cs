using UnityEngine;
public class HealingItem : MonoBehaviour
{
    public HealDataSO healData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().HealPlayer(healData.healAmount);
            Destroy(gameObject);
        }
    }
}
