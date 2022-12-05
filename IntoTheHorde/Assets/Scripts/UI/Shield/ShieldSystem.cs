using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShieldSystem
{
    public event EventHandler OnShieldChanged;

    private int Shield;
    private int MaxShield;

    public ShieldSystem(int MaxShield)
    {
        this.MaxShield = MaxShield;
        this.Shield = MaxShield;
    }
    
    public int GetShield()
    {
        return Shield;
    }
    public int GetMaxShield()
    {
        return MaxShield;
    }
    public float GetShieldPercent()
    {
        return (float)this.Shield / this.MaxShield;
    }
    public void SetShield(int shield)
    {
        if (shield <= MaxShield) this.Shield = shield;
    }
    public void SetMaxShield(int maxShield)
    {
        MaxShield = maxShield;
    }
    public void SetShieldPercent(float shieldPct)
    {
        Shield = (int)((shieldPct / 100) * MaxShield);
    }
    public void Damage(int DamageAmt)
    {
        Shield -= DamageAmt;
        if(Shield < 0)
        {
            Shield = 0;
        }

        if (OnShieldChanged != null)
        {
            OnShieldChanged(this, EventArgs.Empty);
        }
    }

    public void Regen(int RegenAmt)
    {
        Shield += RegenAmt;
        if(Shield > MaxShield)
        {
            Shield = MaxShield;
        }

        if (OnShieldChanged != null)
        {
            OnShieldChanged(this, EventArgs.Empty);
        }
    }
}
