using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable
{
    private HealthHandler hh;
    private HealthSystem hs;

    public void Start()
    {
        HealthHandler hh = player.gameObject.GetComponent<HealthHandler>();
        hs = hh.healthSystem;
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
        hh.healthSystem.SetMaxHealth(newMaxHP);
        Debug.Log("Health Increased to: " + newMaxHP);
    }
}
