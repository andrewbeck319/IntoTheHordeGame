using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float lookRadius = 10f;
    private EnemySpawning enemySpawning;
    private SpriteRenderer spriteRenderer;
    Transform target;
    NavMeshAgent agent;
    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Make sure this gets called when player attacks enemy

    public void TakeDamage()
    {
        Destroy(gameObject);
        // May cause problems with multiple spawners
        enemySpawning = FindObjectOfType<EnemySpawning>();
        enemySpawning.enemyCount--;
        if(enemySpawning.spawnTime <= 0 && enemySpawning.enemyCount <= 0)
        {
            enemySpawning.spawnerDone = true;
        }
    }
    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            posRelativePlayer();
        
            if(distance <= agent.stoppingDistance)
            {
                //attack
                //Right now there's no transform for "front" that keeps in mind sprite direction or player direction (think sphere at the tip of the weapon)
                //Once there is and it's standardized into a prefab, we can look into facing

                //face target probably will need to be offset by the 90 degress in either direction?
                //FaceTarget();
                //lets try this for when we create attacking ability first
                //transform.LookAt(target, Vector3.left);
            }
        }
    }
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 0.5f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void posRelativePlayer()
    {
        Vector3 relativePos = this.transform.InverseTransformPoint(target.position);
        if(target.position.x - relativePos.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
