using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public float lookRadius = 10f;
    private EnemyManager enemyManager;
    private EnemyStats enemyStats;
    private HealthHandler healthHandler;
    private CharacterCombat characterCombat;

    Transform target;
    Transform mainCamera;
    NavMeshAgent agent;

    private float stoppingDistance;
    enum FacingDirection
    {
        Left,
        Right
    }

    private FacingDirection facingDirection = FacingDirection.Left;
    private FacingDirection lastFacingDirection = FacingDirection.Left;

    private void Start() //This has got to be start
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyStats = GetComponent<EnemyStats>();
        healthHandler = GetComponent<HealthHandler>();
        characterCombat = GetComponent<CharacterCombat>();

        mainCamera = PlayerManager.instance.mainCamera.transform;
        target = PlayerManager.instance.player.GetComponent<BoxCollider>().transform;
        agent = GetComponent<NavMeshAgent>();

        stoppingDistance = agent.stoppingDistance;

        healthHandler.healthSystem.SetMaxHealth(enemyStats.maxHealth);
        healthHandler.healthSystem.SetHealthPercent(100);
    }
    // Make sure this gets called when player attacks enemy

    private void Update()
    {
        FaceCamera();
        FaceTarget();

        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= stoppingDistance)
            {
                //there's a bug where once an enemy is within stopping distance,
                //the player can rotate and get the enenmy to stop hitting them,
                //the fix is to get them to re-route to either the front or back of the player
                //by finding the attack position and temporarily disabling stopping distance until they're there.

                float EPSILON = 0.1f;
                Vector3 attackPosition = FindAttackPosition();
                if (Vector3.Distance(transform.position, attackPosition) >= EPSILON)
                {
                    agent.SetDestination(attackPosition);
                    agent.stoppingDistance = 0.0f;
                }
                else
                {
                    agent.stoppingDistance = stoppingDistance;
                }
                characterCombat.Attack();
            }
        }

    }

    private void FaceCamera()
    {
        //do camera relative rotation
        transform.rotation = mainCamera.rotation;
        transform.rotation *= Quaternion.Euler(-mainCamera.rotation.eulerAngles.x, 0, 0);
        if (facingDirection == FacingDirection.Right) //we need to manually reset as FaceCamera() intrinsically makes enemy face left
        {
            transform.RotateAround(transform.position, transform.up, -180f);
        }
    }

    private void FaceTarget()
    {
        Vector3 mainCameraNormalized = mainCamera.position;
        mainCameraNormalized.y = target.position.y;
        Vector3 originVector = (target.position - mainCameraNormalized);

        Vector3 enemyNormalized = this.transform.position;
        enemyNormalized.y = target.position.y;
        Vector3 directionVector = (enemyNormalized - mainCameraNormalized);

        float angleFromCam = Vector3.SignedAngle(originVector, directionVector, Vector3.up);

        if (angleFromCam > 0) facingDirection = FacingDirection.Left;
        else facingDirection = FacingDirection.Right;
        lastFacingDirection = facingDirection;
    }

    private Vector3 FindAttackPosition()
    {
        return target.position + target.transform.right.normalized * stoppingDistance * ((facingDirection == FacingDirection.Left) ? 1.0f:-1.0f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(FindAttackPosition(), 2.0f);
        //Gizmos.color = Color.blue;
        //Vector3 mainCameraNormalized = mainCamera.position;
        //mainCameraNormalized.y = target.position.y;
        //Vector3 originVector = (target.position - mainCameraNormalized);
        //Gizmos.DrawLine(mainCameraNormalized, target.position);
        //Gizmos.color = Color.red;
        //Vector3 enemyNormalized = this.transform.position;
        //enemyNormalized.y = target.position.y;
        //Vector3 directionVector = (enemyNormalized - mainCameraNormalized);
        //
        //Gizmos.DrawLine(mainCameraNormalized, enemyNormalized);
    }

    public void TakeDamage(CharacterStats stats)
    {
        healthHandler.healthSystem.Damage(enemyStats.TakeDamage(stats.damage.GetValue()));
        if (enemyStats.NeedsToDie())
        {
            enemyManager.OnEnemyDestroyed();
            Destroy(gameObject);
        }
    }
}
