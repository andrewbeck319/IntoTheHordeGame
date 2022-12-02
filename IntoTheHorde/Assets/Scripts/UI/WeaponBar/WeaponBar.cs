using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponBar : MonoBehaviour
{
    public List<UI.Weapon> Weapons = new List<UI.Weapon>();
    public int WeaponSelectedIndex = -1;
    public UI.WeaponSelector WeaponSelector;

    public Color ColorItemSelected;
    public Color ColorItemNotSelected;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var weapon in this.Weapons)
        {
            weapon.SetColor(this.ColorItemNotSelected);
        }
        
        this.WeaponSelectedIndex = -1;
        this.SelectWeapon(0);
        this.GetWeaponSelected().CancelCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.SelectWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.SelectWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.SelectWeapon(2);
        }
    }

    public UI.Weapon GetWeaponSelected()
    {
        return this.Weapons[this.WeaponSelectedIndex];
    }

    public void SelectWeapon(int index)
    {
        if (index == this.WeaponSelectedIndex) return;
        if (!this.Weapons[index].IsInCooldown)
        {
            if (this.WeaponSelectedIndex >= 0)
            {
                this.Weapons[this.WeaponSelectedIndex].Unselect();
                this.Weapons[this.WeaponSelectedIndex].SetColor(this.ColorItemNotSelected);
            }
            this.WeaponSelectedIndex = index;
            this.Weapons[this.WeaponSelectedIndex].Select();
            this.Weapons[this.WeaponSelectedIndex].SetColor(this.ColorItemSelected);
            this.WeaponSelector.SetPositionIndex(index);
        }
    }
}
