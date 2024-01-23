using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelToHPSkill : BaseSkill
{   

    public float Fuel;
    public int Health;
    public float FuelToHPValue = 10f;

    public override void Activate()
    {   
        PlayerHPSystem playerHPSystem = GetComponentInParent<PlayerHPSystem>();
        PlayerFuelSystem playerFuelSystem = GetComponentInParent< PlayerFuelSystem>();
        Fuel = playerFuelSystem.Fuel;
        Health = playerHPSystem.Health;
        if (Fuel > FuelToHPValue)
        {
            Fuel -= FuelToHPValue;
            Health += 1;
        }
    }
}
