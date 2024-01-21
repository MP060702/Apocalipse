using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern2 : MonoBehaviour
{

    Enemy enemy;
    public float MoveSpeed;
    public float AttackStopTime;
    public float MoveTime;
    public GameObject Projectile;
    public float ProjectileMoveSpeed;
    private Vector3 _direction;
    private bool _isAttack = false;
    

    void Start()
    {   
        enemy = GetComponent<Enemy>();
        StartCoroutine(Attack());
    }

    void Update()
    {
        if (false == _isAttack)
            Move();
    }

    IEnumerator Attack()
    {
        while (true)
        {   
            yield return new WaitForSeconds(1f); // 1초 기다림

            GameObject manager = GameObject.Find("Managers");
            BaseCharacter character = manager.GetComponent<CharacterManager>().Player;
            if (character is null)
            {
                Debug.Log("Player is null");
                break;
            }

            Vector3 playerPos = character.GetComponent<Transform>().position;
            Vector3 direction = playerPos - transform.position;
            direction.Normalize();
            if(enemy.blsfreeze == false)
            {
                var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().SetDirection(direction);
                projectile.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;
            }
            

            _isAttack = true;

            yield return new WaitForSeconds(AttackStopTime); // 1초 기다림

            _isAttack = false;

            yield return new WaitForSeconds(MoveTime); // 3초 동안 움직임
        }
    }

    void Move()
    {   
        if(enemy.blsfreeze == false) 
        {
            transform.position -= new Vector3(0f, MoveSpeed * Time.deltaTime, 0f);
        }
        
    }
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}
