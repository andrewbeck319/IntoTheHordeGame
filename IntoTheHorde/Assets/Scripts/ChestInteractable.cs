using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChestInteractable : Interactable
{
    private HealthHandler hh;
    private HealthSystem hs;
    private PlayerStats ps;
    private CharacterCombat cc;
    private bool interactable = true;
    [SerializeField] private TMP_Text text;

    public void Start()
    {
        hh = player.gameObject.GetComponent<HealthHandler>();
        hs = hh.healthSystem;
        ps = player.gameObject.GetComponent<PlayerStats>();
        cc = player.gameObject.GetComponent<CharacterCombat>();
    }

    public override void Interact()
    {
        if(interactable)
        {
            base.Interact();
            System.Random rnd = new System.Random();
            int rndNumber = rnd.Next(0, 51); // 

            switch (rndNumber)
            {
                case int n when (n <= 30):
                    healPlayer();
                    break;
                case int n when (n > 30 && n <= 40):
                    healthIncrease();
                    break;
                case int n when (n > 40 && n <= 45):
                    ps.BuffDamage(1.05f);
                    StartCoroutine(setText("Damage buffed by 5 percent"));
                    Debug.Log("Damage buffed by 5 percent");
                    break;
                case int n when (n > 45 && n <= 50):
                    cc.atkSpdBuff(1.03f);
                    StartCoroutine(setText("Attack Speed buffed by 3 percent"));
                    Debug.Log("Attack Speed buffed by 3 percent");
                    break;
                default:
                    StartCoroutine(setText("You opened a chest full of nothing :("));
                    Debug.Log("You Opened a Chest Full of Nothing :(");
                    break;
            }
            delayedDestroy(5);
            interactable = false;
        }
    }

    private void healPlayer()
    {
        hs.Heal(10);
        StartCoroutine(setText("Healed player by 10 HP")); 
        Debug.Log("Healed player by 10 HP");
    }

    private void healthIncrease()
    {
        int increase = (int)(0.05f * hs.GetMaxHealth());
        int newMaxHP = hs.GetMaxHealth() + increase;
        hs.SetMaxHealth(newMaxHP);
        StartCoroutine(setText("Health increased to " + newMaxHP));
        Debug.Log("Health Increased to: " + newMaxHP);
    }

    private IEnumerator setText(string text)
    {
        this.text.SetText(text);
        this.text.enabled = true;
        yield return new WaitForSeconds(5);
        this.text.enabled = false;
    }

    private IEnumerator delayedDestroy(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this);
    }
}
