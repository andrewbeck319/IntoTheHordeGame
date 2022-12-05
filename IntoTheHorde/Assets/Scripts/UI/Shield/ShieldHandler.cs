using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{

    public ShieldBarScript shieldBar;
    [HideInInspector] public ShieldSystem shieldSystem;

    private void Awake()
    {
        shieldSystem = new ShieldSystem(100);
        shieldBar.SetUp(shieldSystem);
    }
}
