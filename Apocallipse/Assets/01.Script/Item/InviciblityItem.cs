using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class InvincibilityItem : BaseItem
{
    public override void OnGetItem(CharacterManager characterManager)
    {
        
        characterManager.Player.GetComponent<PlayerCharacter>().SetInvincibility(true);
    }
}