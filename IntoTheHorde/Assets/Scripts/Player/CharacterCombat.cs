using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

	public float attackSpeed = 1f;
	private float attackCooldown = 0f;

	public float attackDelay = .6f;

	CharacterStats myStats;
	private GameObject weaponHolder;
	private BoxCollider weaponHitbox;
	private Animator weaponAnimator;

	void Start ()
	{
		myStats = GetComponent<CharacterStats>();
        foreach (Transform child in transform)
		{
			if (child.name == "WeaponHolder")
			{
				weaponHolder = child.gameObject;
				break;
			}
        }

        weaponHitbox = weaponHolder.GetComponentInChildren<BoxCollider>();
		weaponAnimator = weaponHolder.GetComponentInChildren<Animator>();
        
		// weaponHitbox.isTrigger = false; //set weapon hitbox to off
    }

	void Update ()
	{
		attackCooldown -= Time.deltaTime;
	}

	public void Attack ()
	{
		if (attackCooldown <= 0f)
		{
			StartCoroutine(AttackDelay(attackDelay));
            attackCooldown = 1f / attackSpeed;
		}
		
	}
	private void DoAttack()
	{
		// weaponHitbox.gameObject.SetActive(true);

		// Check each obj thats within weapon hitbox and determine whether or not damage should be induced or not
		List<GameObject> objects = weaponHitbox.gameObject.GetComponent<HitDetection>().hitableObjectsConsumer; //call this once and get the list, not on every iteration
        foreach (GameObject obj in objects)
		{
			if (obj) //gotta check if the thing is still alive, let
			{
				if ((this.gameObject.tag == "Player") && (obj.tag == "Enemy"))
				{
					Debug.Log("Enemy hit");
					Enemy enemyController = obj.GetComponent<Enemy>();
					enemyController.TakeDamage(GetComponentInParent<PlayerStats>());
				}
				if (obj.tag == "Player")
				{
					Debug.Log("Player hit");
					PlayerController playerController = obj.GetComponent<PlayerController>();
					playerController.TakeDamage(GetComponentInParent<EnemyStats>());
				}
			}
		}
        weaponAnimator.SetFloat("AttackSpeed", attackSpeed);
        weaponAnimator.SetTrigger("Attack");
        // StartCoroutine(TurnOffHitBox(weaponAnimator.GetCurrentAnimatorStateInfo(0).length));

    }
    IEnumerator AttackDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
		DoAttack();
    }
	IEnumerator TurnOffHitBox(float attackDuration)
	{
        yield return new WaitForSeconds(attackDuration);
        // weaponHitbox.gameObject.SetActive(false);
    }

	public void atkSpdBuff(float percent)
    {
		attackSpeed = attackSpeed * percent;
    }
}
