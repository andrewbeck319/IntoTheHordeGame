// Class is system that will manage a player or enemies health: https://www.youtube.com/watch?v=0T5ei9jN63M
using System;
public class HealthSystem 
{
    public event EventHandler OnHealthChanged; 
    // Variable to store an ememies or players current health
    private int Health; 

    // Varaible to store player or enemies max health
    private int MaxHealth; 

    // Health constructor
    public HealthSystem(int MaxHealth){
        this.MaxHealth = MaxHealth;
        Health = MaxHealth;
    }

    // Function to get health of player or enemy
    public int GetHealth(){
        return Health;
    }

    // Return health as a percent to adjust health bar by
    public float GetHealthPercent(){
        return (float)Health / MaxHealth;
    }

    // Enables damage to be done to the player or enemy 
    public void Damage(int DamageAmt){
        Health -= DamageAmt;
        if (Health < 0){
            Health = 0; 
        }
        
        // Used to optimize healthbar code so that changes are only fired when something occurs
        if (OnHealthChanged != null){
            OnHealthChanged(this, EventArgs.Empty);
        }
    }

    // Enables player or enemy to heal by specified amount
    public void Heal(int HealAmt){
        Health += HealAmt;
        if (Health > MaxHealth){
            Health = MaxHealth; 
        }
        
        // Used to optimize healthbar code so that changes are only fired when something occurs
        if (OnHealthChanged != null){
            OnHealthChanged(this, EventArgs.Empty);
        }
    }

}
