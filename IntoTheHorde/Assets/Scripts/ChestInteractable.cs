using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable
{
    private HealthHandler hh;
    private HealthSystem hs;
    private PlayerStats ps;
    private CharacterCombat cc;

    public void Start()
    {
        hh = player.gameObject.GetComponent<HealthHandler>();
        hs = hh.healthSystem;
        ps = player.gameObject.GetComponent<PlayerStats>();
        cc = player.gameObject.GetComponent<CharacterCombat>();
    }
    public override void Interact()
    {
        base.Interact();
        System.Random rnd = new System.Random();
        int rndNumber = rnd.Next(0, 101);

        switch(rndNumber)
        {
            case int n when (n <= 30):
                healPlayer();
                break;
            case int n when (n > 30 && n <= 40):
                healthIncrease();
                break;
            case int n when (n > 40 && n <= 45):
                ps.BuffDamage(1.05f);
                Debug.Log("Damage buffed by 5 percent");
                break;
            case int n when (n > 45 && n <= 50):
                cc.atkSpdBuff(1.03f);
                Debug.Log("Attack Speed buffed by 3 percent");
                break;
            default:
                Debug.Log("You Opened a Chest Full of Nothing :(");
                break;
        }

    }

    private void healPlayer()
    {
        hs.Heal(10);
        Debug.Log("Healed player by 10 HP");
    }

    private void healthIncrease()
    {
        int increase = (int)(0.05f * hs.GetMaxHealth());
        int newMaxHP = hs.GetMaxHealth() + increase;
        hs.SetMaxHealth(newMaxHP);
        Debug.Log("Health Increased to: " + newMaxHP);
    }

}
