using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weapon;
    [HideInInspector] public bool CanAttack = false;
    private float AttackCooldown = 1.0f;

    private void Start()
    {
        GetComponentInChildren<BoxCollider>().isTrigger = false;
    }
    public void Attack()
    {
        CanAttack = false;
        GetComponentInChildren<BoxCollider>().isTrigger = true;
        Animator anim = weapon.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }
    public void SetAttackCooldown(float cooldown)
    {
        AttackCooldown = cooldown;
    }
    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        GetComponentInChildren<BoxCollider>().isTrigger = false;
        CanAttack = true;
    }
    // Update is called once per frame
}
