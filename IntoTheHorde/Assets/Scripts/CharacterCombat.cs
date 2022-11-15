using System.Collections;
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
        
		weaponHitbox.isTrigger = false; //set weapon hitbox to off
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
        weaponHitbox.isTrigger = true;
        weaponAnimator.SetFloat("AttackSpeed", attackSpeed);
        weaponAnimator.SetTrigger("Attack");
        StartCoroutine(TurnOffHitBox(weaponAnimator.GetCurrentAnimatorStateInfo(0).length));
    }
    IEnumerator AttackDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
		DoAttack();
    }
	IEnumerator TurnOffHitBox(float attackDuration)
	{
        yield return new WaitForSeconds(attackDuration);
        weaponHitbox.isTrigger = false;
    }

}
