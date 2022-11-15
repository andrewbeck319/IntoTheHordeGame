using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private HealthSystem healthSystem;

    // Assign our healthsystem to the one passed from HealthHandler
    public void SetUp(HealthSystem healthSystem){
        this.healthSystem = healthSystem;

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    // Used to increase healthbar performance so we aren't calculating the healthbar every frame, healthbar updates only when change occurs
    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e){
        // Find the healthbar and adjust it's x position by the health percent * 10
        transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }
}