using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FieldSkill : BaseSkill
{   
    public GameObject Field;
    
    
    public override void Activate()
    {
        base.Activate();
        for (int i = 0; i < 360; i += 30)
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            ShootProjectile(transform.position, direction);
        }           
        
    }

    public void ShootProjectile(Vector3 position, Vector3 direction)
    {
        GameObject instance = Instantiate(Field, position, Quaternion.identity);
        FieldObject projectile = instance.GetComponent<FieldObject>();
        instance.transform.parent = gameObject.transform;
        if (projectile != null)
        {
            projectile.SetDirection(direction);
        }
    }



}
