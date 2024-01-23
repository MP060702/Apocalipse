using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOn : MonoBehaviour
{
    public int num = 0;
    public Vector3 position;

    void Update()
    {
        GameObject manager = GameObject.Find("Managers");
        CharacterManager character = manager.GetComponent<CharacterManager>();
        PlayerCharacter playerCharacter = character.GetComponent<PlayerCharacter>();
        position = playerCharacter.AddOnPos[num].transform.position;

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 2f);
    }
}
