using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public float lookRadius = 10f;
    private EnemySpawning enemySpawning;
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

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        mainCamera = PlayerManager.instance.mainCamera.transform;
        agent = GetComponent<NavMeshAgent>();
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
        FaceCamera();
        FaceTarget();

        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            
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
    }

    /*
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
    */
}
