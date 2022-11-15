// HealthHandler script is meant to be used to handle all our different systems within one place
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public HealthBarScript healthBar;
    [HideInInspector] public HealthSystem healthSystem;

    private void Awake()
    {
        // Create our new health system with a max health amount
        healthSystem = new HealthSystem(100); 
        
        // Setup the health bar for this health system
        healthBar.SetUp(healthSystem);


    }

}