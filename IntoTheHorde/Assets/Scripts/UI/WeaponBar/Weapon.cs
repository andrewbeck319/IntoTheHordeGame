using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Weapon : MonoBehaviour
    {
        public bool IsInCooldown = false;
        public float CooldownTime = 10;
        public float CooldownTimeRemaining;
        public bool IsSelected = false;

        public GameObject CooldownObj;

        public void Select()
        {
            this.IsSelected = true;
            this.GetComponent<Image>().material.color = new Color(this.GetComponent<Image>().material.color.r,
                this.GetComponent<Image>().material.color.g, this.GetComponent<Image>().material.color.b, 0.8f);
        }

        public void SetColor(Color color)
        {
            this.GetComponent<Image>().color = color;
        }

        public void Unselect()
        {
            this.IsSelected = false;
            this.StartCooldown();
            
            this.GetComponent<Image>().material.color = new Color(this.GetComponent<Image>().material.color.r,
                this.GetComponent<Image>().material.color.g, this.GetComponent<Image>().material.color.b, 0.18f);
        }

        private void StartCooldown()
        {
            this.CooldownTimeRemaining = this.CooldownTime;
            this.IsInCooldown = true;
            this.CooldownObj.SetActive(true);
        }

        public void CancelCooldown()
        {
            this.CooldownTimeRemaining = 0;
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if (this.IsInCooldown)
            {
                this.CooldownTimeRemaining -= Time.deltaTime;

                if (this.CooldownTimeRemaining <= 0)
                {
                    this.IsInCooldown = false;
                    this.CooldownTimeRemaining = 0;
                    this.CooldownObj.SetActive(false);
                }

                this.CooldownObj.GetComponent<RectTransform>().sizeDelta = new Vector2(this.CooldownObj.GetComponent<RectTransform>().sizeDelta.x, (100f * this.CooldownTimeRemaining / this.CooldownTime));
            }
        }
    }
   
}