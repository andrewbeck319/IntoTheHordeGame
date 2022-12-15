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
    private PlayerController pc;
    private bool interactable = true;
    private static int chestCost = 15;
    [SerializeField] private TMP_Text text;
    private static System.Random rnd;

    public void Start()
    {
        player = GameObject.Find("Player").transform;
        hh = player.gameObject.GetComponent<HealthHandler>();
        hs = hh.healthSystem;
        ps = player.gameObject.GetComponent<PlayerStats>();
        cc = player.gameObject.GetComponent<CharacterCombat>();
        mh = FindObjectOfType<MoneyHandler>();
        pc = player.gameObject.GetComponent<PlayerController>();
        rnd = new System.Random();
    }

    private void Awake()
    {
        chestCost = 15;
    }

    public override void Interact()
    {
        if(interactable && mh.gold >= chestCost)
        {
            mh.addGold(chestCost * -1);
            base.Interact();
            
            int rndNumber = rnd.Next(0, 91); // 
            healPlayer(20);
            switch (rndNumber)
            {
                case int n when (n <= 15):
                    healPlayer(100);
                    break;
                case int n when (n > 15 && n <= 30):
                    healthIncrease();
                    break;
                case int n when (n > 30 && n <= 45):
                    ps.BuffDamage(1.10f);
                    StartCoroutine(setText("Damage buffed by 10 percent"));
                    Debug.Log("Damage buffed by 5 percent");
                    break;
                case int n when (n > 45 && n <= 55):
                    cc.atkSpdBuff(1.10f);
                    StartCoroutine(setText("Attack Speed buffed by 10 percent"));
                    //Debug.Log("Attack Speed buffed by 10 percent");
                    break;
                case int n when (n > 55 && n <= 65):
                    dashDistanceIncrease();
                    break;
                case int n when (n > 65 && n <= 70):
                    invulnerabilityTimeBuff();
                    break;
                case int n when (n > 70 && n <= 75):
                    leapHeightBuff();
                    break;
                case int n when (n > 75 && n < 90):
                    healOnKillBuff();
                    break;
                default:
                    StartCoroutine(setText("You opened a chest full of nothing :("));
                    //Debug.Log("You Opened a Chest Full of Nothing :(");
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
        updatePlayerHealth();
        StartCoroutine(setText("Healed player by " + heal + " HP")); 
        //Debug.Log("Healed player by 10 HP");
    }

    private void healthIncrease()
    {
        int increase = (int)(0.05f * hs.GetMaxHealth());
        int newMaxHP = hs.GetMaxHealth() + increase;
        hs.SetMaxHealth(newMaxHP);
        updatePlayerHealth();
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
        pc.dashingPower = pc.dashingPower * 1.05f;
        StartCoroutine(setText("Dashing range increased by 5 percent"));
    }

    private void updatePlayerHealth()
    {
        ps.maxHealth = hs.GetMaxHealth();
        ps.currentHealth = hs.GetHealth();
    }

    private void invulnerabilityTimeBuff()
    {
        pc.invulnerabilityBuff(1.03f);
        StartCoroutine(setText("Invulnerability time increased by 3 percent"));
    }

    private void healOnKillBuff()
    {
        ps.healthOnKill += 1;
        StartCoroutine(setText("Gain " + ps.healthOnKill + " health every kill"));
    }
    private void leapHeightBuff()
    {
        ps.leapHeight.AddModifier(5);
        StartCoroutine(setText("Leap Height increased"));
    }
}
