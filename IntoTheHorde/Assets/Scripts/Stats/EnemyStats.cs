using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keeps track of enemy stats, loosing health and dying. */

public class EnemyStats : CharacterStats
{
	private int _difficultyLevel = 0;

	public override void Die()
	{
		base.Die();

		// Add ragdoll effect / death animation

		Destroy(gameObject);
	}

	private void _increaseDifficulty()
	{
		this._difficultyLevel++;
		
		this.armor.AddModifier(1);
		this.damage.AddModifier(1);
		
		Debug.Log("Difficulty level set to " + this._difficultyLevel);
	}
	private void Update()
	{
		float gameTime = GameManager.Instance.StatsController.GameTime;
		if (gameTime > 10 * 2 && this._difficultyLevel == 0) this._increaseDifficulty();
		else if (gameTime > 60 * 4 && this._difficultyLevel == 1) this._increaseDifficulty();
		else if (gameTime > 60 * 6 && this._difficultyLevel == 2) this._increaseDifficulty();
		else if (gameTime > 60 * 8 && this._difficultyLevel == 3) this._increaseDifficulty();
	}
}
