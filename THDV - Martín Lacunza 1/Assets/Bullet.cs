using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletDataSO bulletData;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().TakeDamage(bulletData.damage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}