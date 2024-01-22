using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FieldObject : MonoBehaviour
{
    
    private Vector3 _direction;
    public GameObject ExplodeFX;
    

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
