using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyHandler : MonoBehaviour
{
    [SerializeField] public int gold { get; set; }
    [SerializeField] private TMP_Text text;

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
        text.SetText("Gold: " + this.gold);
    }
}
