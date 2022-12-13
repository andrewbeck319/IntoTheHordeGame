using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles the players stats and adds/removes modifiers when equipping items. */

public class PlayerStats : CharacterStats {

	public int healthOnKill = 0;
	// Use this for initialization
	void Start () {
		//EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged; equipment is todo
	}

    private void Update()
    {

    }
    // Called when an item gets equipped/unequipped
    void OnEquipmentChanged (Equipment newItem, Equipment oldItem)
	{
		// Add new modifiers
		if (newItem != null)
		{
			armor.AddModifier(newItem.armorModifier);
			damage.AddModifier(newItem.damageModifier);
		}

		// Remove old modifiers
		if (oldItem != null)
		{
			armor.RemoveModifier(oldItem.armorModifier);
			damage.RemoveModifier(oldItem.damageModifier);
		}
		
	}

	public void BuffDamage(float percent)
    {
		int currentDamage = this.damage.GetValue();
		int newDamage = (int)System.Math.Ceiling(currentDamage * percent);
		this.damage.SetStat(newDamage);
    }

    public override int TakeDamage(int damage)
    {
		Debug.Log("Player Health: " + currentHealth);
		return base.TakeDamage(damage);
    }
}
