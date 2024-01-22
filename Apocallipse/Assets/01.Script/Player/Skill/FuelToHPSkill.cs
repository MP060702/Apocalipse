using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelToHPSkill : BaseSkill
{
    public float Fuel;
    public int Health;
    public float FuelToHP = 10f;

    public override void Activate()
    {   
        PlayerHPSystem playerHPSystem = GetComponent<PlayerHPSystem>();
        InitFuel();
        Health += 1;
        Fuel -= FuelToHP;
    }

    public void InitFuel()
    {
        GameInstance.instance.CurrentPlayerHP = Health;

        GameInstance.instance.CurrentPlayerFuel = Fuel;
    }

}
