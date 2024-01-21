using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelToHPSkill : BaseSkill
{
    public float Fuel;
    public float Health;
    public float FuelToHP = 10f;

    public override void Activate()
    {
        Fuel = GameInstance.instance.CurrentPlayerFuel;
        Health = GameInstance.instance.CurrentPlayerHP;

        if(Fuel > FuelToHP)
        {
            Fuel -= FuelToHP;
            Health += 1;
        }
    }

}
