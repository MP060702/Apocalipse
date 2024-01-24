using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOn : MonoBehaviour
{
    public Transform SpawnPos;
    public GameObject Projectile;

    void Awake()
    {
        StartCoroutine(Attack());
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, SpawnPos.position, Time.deltaTime);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetDirection(Vector2.up);
            projectile.GetComponent<Projectile>().MoveSpeed = 3;

        }
    }
}
