using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public int damage = 10; // Daño que hace el enemigo
    private float attackCooldown = 1f; // Tiempo entre ataques
    private float nextAttackTime = 0f;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("First Person Controller").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);
    }

    // Método para manejar el daño recibido
    public void TakeDamage(int damage)
    {
        health -= damage; // Resta la vida
        if (health <= 0)
        {
            Die(); // Si la vida es 0 o menos, destruye el enemigo
        }
    }

    private void Die()
    {
        Destroy(gameObject); // Destruye el objeto enemigo
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó es una bala
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1); // Supongamos que cada bala hace 1 de daño
            Destroy(other.gameObject); // Destruye la bala
        }
        if (other.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            // Supongamos que el jugador tiene un script que maneja la vida
            PlayerStats PlayerStats = other.GetComponent<PlayerStats>();
            if (PlayerStats != null)
            {
                PlayerStats.TakeDamage(damage); // Aplica el daño
            }

            nextAttackTime = Time.time + attackCooldown; // Establece el tiempo para el próximo ataque
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
