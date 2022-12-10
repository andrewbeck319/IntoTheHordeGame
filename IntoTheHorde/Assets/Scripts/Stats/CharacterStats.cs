using UnityEngine;

/* Base class that player and enemies can derive from to include stats. */

public class CharacterStats : MonoBehaviour {

	// Health
	public int maxHealth = 100;
	public int currentHealth { get; private set; }

	public Stat damage;
	public Stat shield;
	public Stat armor;

	public Stat shieldRegenRate;
	public Stat shieldRegenDelay;

	public Stat leapMaxCount;
	public Stat leapRegenRate;
	public Stat leapHeight;

	private bool shouldDie = false;
    // Set current health to max health
    // when starting the game.
    void Awake ()
	{
		currentHealth = maxHealth;
	}

	// Damage the character
	public int TakeDamage (int damage)
	{

		// Subtract the armor value
		damage -= armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		// Damage the character
		currentHealth -= damage;
		Debug.Log(transform.name + " takes " + damage + " damage.");

		// If health reaches zero
		if (currentHealth <= 0)
		{
			Die();
		}
		return damage;
	}
	public bool NeedsToDie()
	{
		return shouldDie;
	}
	public virtual void Die ()
	{
		// Die in some way
		// This method is meant to be overwritten
		Debug.Log(transform.name + " died.");
		shouldDie = true;
	}

}
