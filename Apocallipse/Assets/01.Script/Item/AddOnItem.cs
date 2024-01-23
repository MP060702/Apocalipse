using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnItem : BaseItem
{
    public GameObject AddOnPrefabs;
    

    public override void OnGetItem(CharacterManager characterManager)
    {   
        
        PlayerCharacter playerCharacter = characterManager.GetComponent<PlayerCharacter>();
        AddOn addOn = AddOnPrefabs.GetComponent<AddOn>();

        for(int i = 0; i < playerCharacter.AddOnPos.Length; i++)    
        { 
            Instantiate(AddOnPrefabs, playerCharacter.AddOnPos[i].transform.position, Quaternion.identity);
            addOn.num++;
        }
        
    }
}