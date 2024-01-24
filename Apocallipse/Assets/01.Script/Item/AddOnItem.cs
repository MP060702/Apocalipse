using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnItem : BaseItem
{
    public GameObject AddonPrefab;

    public override void OnGetItem(CharacterManager characterManager)
    {
        base.OnGetItem(characterManager);

        if (GameInstance.instance.CurrentPlayerAddOnCount < 2)
        {   
            
            PlayerCharacter player = characterManager.Player.GetComponent<PlayerCharacter>();


            SpawnAddOn(player.AddOnPos[GameInstance.instance.CurrentPlayerAddOnCount].transform.position, AddonPrefab
                , player.AddOnPos[GameInstance.instance.CurrentPlayerAddOnCount]);
            //Debug.Log(GameInstance.instance.CurrentPlayerAddOnCount);
            GameInstance.instance.CurrentPlayerAddOnCount++;
        }
    }

    public static void SpawnAddOn(Vector3 position, GameObject prefabs, Transform transform)
    {   
        GameObject inst = Instantiate(prefabs, position, Quaternion.identity);
        inst.GetComponent<AddOn>().SpawnPos = transform;
    }

    
}