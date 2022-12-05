using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    private ShieldSystem shieldSystem;

    public void SetUp(ShieldSystem shieldSystem)
    {
        this.shieldSystem = shieldSystem;
        shieldSystem.OnShieldChanged += ShieldSystem_OnShieldChanged;
    }
    private void ShieldSystem_OnShieldChanged(object sender, System.EventArgs e)
    {
        transform.Find("SBar").localScale = new Vector3(shieldSystem.GetShieldPercent(), 1);
    }
}
