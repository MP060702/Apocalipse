using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOn : MonoBehaviour
{

    void Update()
    {
        GameObject manager = GameObject.Find("Managers");
        BaseCharacter character = manager.GetComponent<CharacterManager>().Player;

        PlayerCharacter character1 = character.GetComponent<PlayerCharacter>();     
        transform.position = Vector3.Lerp(transform.position, character1.
            AddOnPos[character1.CurrentPlayerAddCount].transform.position, Time.deltaTime * 100f);
    }
}
