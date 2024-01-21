using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPattern3 : MonoBehaviour
{
    Enemy enemy;
    public float MoveSpeed;
    public GameObject Projectile;
    public float ProjectileMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if(enemy.blsfreeze == false)
        {
            GameObject manager = GameObject.Find("Managers");
            BaseCharacter character = manager.GetComponent<CharacterManager>().Player;

            Vector3 playerPos = character.GetComponent<Transform>().position;
            Vector3 direction = playerPos - transform.position;
            direction.Normalize();

            transform.Translate(direction * MoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (enemy.blsfreeze == false)
        {
            if (collision.tag == "Player")
            {
                for (int i = 0; i < 360; i += 45)
                {
                    float angle = i * Mathf.Deg2Rad;
                    Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

                    var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
                    projectile.GetComponent<Projectile>().SetDirection(direction);
                    projectile.GetComponent<Projectile>().MoveSpeed = ProjectileMoveSpeed;

                }
                Destroy(gameObject);
            }
        }
        
    }

}
