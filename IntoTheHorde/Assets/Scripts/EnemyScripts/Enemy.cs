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
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

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
            
            if(distance <= agent.stoppingDistance)
            {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
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
