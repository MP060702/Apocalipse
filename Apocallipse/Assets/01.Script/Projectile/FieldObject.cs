using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FieldObject : MonoBehaviour
{
    public Vector3 playerPos;
    private Vector3 _direction;
    public GameObject ExplodeFX;
    private Vector3 Axis = new Vector3(0, 0, 0);

    [SerializeField]
    private float _lifeTime = 60f;


    void Start()
    {
        Destroy(gameObject, _lifeTime);
        Vector3 pos = _direction  * 1;
        transform.Translate(pos);
    }

    void Update()
    {        
        
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {   
 
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
