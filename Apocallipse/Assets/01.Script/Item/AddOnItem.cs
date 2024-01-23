using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnItem : BaseItem
{
    public GameObject AddonPrefab;

    public override void OnGetItem(CharacterManager characterManager)
    {   
        base.OnGetItem(characterManager);
        var player = characterManager.GameManager.GetPlayerCharacter();
        Debug.Log(AddonPrefab);
        SpawnAddOn(player.AddOnPos[GameInstance.instance.CurrentPlayerAddOnCount].transform.position, AddonPrefab);
        
             
    }

    public static void SpawnAddOn(Vector3 position, GameObject prefabs)
    {
        Instantiate(prefabs, position, Quaternion.identity);
    }
}