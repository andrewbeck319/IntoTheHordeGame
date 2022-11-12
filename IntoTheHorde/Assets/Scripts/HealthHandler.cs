// HealthHandler script is meant to be used to handle all our different systems within one place
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public HealthBarScript healthBar;
    [HideInInspector] public HealthSystem healthSystem;
    // Start is called before the first frame update
    private void Start()
    {
        // Create our new health system with a max health amount
        healthSystem = new HealthSystem(100); 
        
        // Setup the health bar for this health system
        healthBar.SetUp(healthSystem);

        // In case testing needed
        // Debug.Log("Health: " + healthSystem.GetHealthPercent());
        // healthSystem.Damage(1);
        // Debug.Log("Health: " + healthSystem.GetHealth());
        // healthSystem.Heal(10);
        // Debug.Log("Health: " + healthSystem.GetHealth());
        // healthSystem.Damage(50);
        // Debug.Log("Health: " + healthSystem.GetHealth());

    }

}