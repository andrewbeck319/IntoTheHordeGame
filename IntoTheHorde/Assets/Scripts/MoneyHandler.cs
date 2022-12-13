using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyHandler : MonoBehaviour
{
    [SerializeField] public int gold { get; set; }
    [SerializeField] private TMP_Text text;
    public int totalGold = 0;

    // Start is called before the first frame update
    void Start()
    {
        gold = 0;
        text.SetText("Gold: 0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addGold(int gold)
    {
        this.gold += gold;
        if (gold > 0)
            this.totalGold += gold;
        text.SetText("Gold: " + this.gold);
    }
}
