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
    private MoneyHandler mh;
    private bool interactable = true;
    private static int chestCost = 15;
    [SerializeField] private TMP_Text text;

    public void Start()
    {
        player = GameObject.Find("Player").transform;
        hh = player.gameObject.GetComponent<HealthHandler>();
        hs = hh.healthSystem;
        ps = player.gameObject.GetComponent<PlayerStats>();
        cc = player.gameObject.GetComponent<CharacterCombat>();
        mh = FindObjectOfType<MoneyHandler>();
    }

    public override void Interact()
    {
        if(interactable && mh.gold >= chestCost)
        {
            mh.addGold(-15);
            base.Interact();
            System.Random rnd = new System.Random();
            int rndNumber = rnd.Next(0, 56); // 
            healPlayer(20);
            switch (rndNumber)
            {
                case int n when (n <= 30):
                    healPlayer(100);
                    break;
                case int n when (n > 30 && n <= 40):
                    healthIncrease();
                    break;
                case int n when (n > 40 && n <= 45):
                    ps.BuffDamage(1.10f);
                    StartCoroutine(setText("Damage buffed by 10 percent"));
                    Debug.Log("Damage buffed by 5 percent");
                    break;
                case int n when (n > 45 && n <= 50):
                    cc.atkSpdBuff(1.10f);
                    StartCoroutine(setText("Attack Speed buffed by 10 percent"));
                    Debug.Log("Attack Speed buffed by 3 percent");
                    break;
                case int n when (n > 50 && n <= 55):
                    dashDistanceIncrease();
                    break;
                default:
                    StartCoroutine(setText("You opened a chest full of nothing :("));
                    Debug.Log("You Opened a Chest Full of Nothing :(");
                    break;
            }
            chestCost += 5;
            interactable = false;
            StartCoroutine(delayedDestroy(1));
            this.enabled = false;
            
        }
        else if(mh.gold < chestCost)
        {
            StartCoroutine(setText("You need at least " + chestCost +  " gold to open this chest"));
        }
    }

    private void healPlayer(int heal)
    {
        hs.Heal(heal);
        StartCoroutine(setText("Healed player by " + heal + " HP")); 
        //Debug.Log("Healed player by 10 HP");
    }

    private void healthIncrease()
    {
        int increase = (int)(0.05f * hs.GetMaxHealth());
        int newMaxHP = hs.GetMaxHealth() + increase;
        hs.SetMaxHealth(newMaxHP);
        StartCoroutine(setText("Health increased to " + newMaxHP));
        //Debug.Log("Health Increased to: " + newMaxHP);
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
        Destroy(gameObject);
    }

    private void dashDistanceIncrease()
    {
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();
        pc.dashingPower = pc.dashingPower * 0.05f;
        StartCoroutine(setText("Dashing range increased by 5 percent"));
    }
}
