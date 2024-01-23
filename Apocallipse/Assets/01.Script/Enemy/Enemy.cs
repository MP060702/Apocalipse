using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    public float Health = 3f;
    public float AttackDamage = 1f;
    bool bIsDead = false;
    
   
    public bool bMustSpawnItem = false;

    public GameObject ExplodeFX;

    public bool blsfreeze = false;

    void Start()
    {
    }

    void Update()
    {
        if(blsfreeze == true)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(2, 0, 0, 0.5f);
            Invoke("FreezingClear", 2f);
        }
    }

    public void Dead()
    {
        if (!bIsDead)
        {
            GameManager.Instance.EnemyDies();

            if (!bMustSpawnItem)
                GameManager.Instance.ItemManager.SpawnRandomItem(0, 3, transform.position);
            else
                GameManager.Instance.ItemManager.SpawnRandomItem(transform.position);

            bIsDead = true;

            Instantiate(ExplodeFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Health -= 1f;
            //GameManager.Instance.SoundManager.PlaySFX("EnemyHit");

            if (Health <= 0f)
            {
                Dead();
            }

            StartCoroutine(HitFlick());
            Destroy(collision.gameObject);
        }
    }

    public void FreezingClear()
    {
        blsfreeze = false;
        GetComponentInChildren<SpriteRenderer>().color = new Color(2, 1, 1, 1);
    }

    IEnumerator HitFlick()
    {
        int flickCount = 0; // 깜박인 횟수를 기록하는 변수

        while (flickCount < 1) // 1번 깜박일 때까지 반복
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);

            yield return new WaitForSeconds(0.1f); // 0.1초 대기

            GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            yield return new WaitForSeconds(0.1f); // 0.1초 대기

            flickCount++; // 깜박인 횟수 증가
        }
    }

    
}