using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

//플레이어의 체력 시스템을 관장하는 클래스로 GameInstace 스크립트에서 플레이어의 체력 정보를 가져와 체력시스템을 조정하며,
//리페어 스킬과 스크립트와 리페어 스킬의 체력 조정도 이 클래스를 통하여 조정함.
public class PlayerHPSystem : MonoBehaviour
{   
    
    public int Health; // 플레이어의 체력값을 가지는 변수로서 GameInstance 안에 플레이어 체력정보를 가져와 값을 넣는다. 해당 변수의 값을 조정하여 체력을 조정할 수 있음.
    public int MaxHealth; // 플레이어의 최대 체력값을 지정하는 함수로서 플레이어의 체력이 회복되는 과정에서 최대 체력값을 벗어나지 않도록 해주는 임계점의 역활을 한다.

    void Start()
    {
        Health = GameInstance.instance.CurrentPlayerHP;
    }
    //InitHealth는 Health의 값에 MaxHealth의 값을 넣어주고, GameInstance의 플레이어의 체력값에 다시 Health값을 집어넣어 GameInstance의 플레이어의 체력 정보를 전달해 주는 역활을 한다.
    public void InitHealth()
    {
        Health = MaxHealth;
        GameInstance.instance.CurrentPlayerHP = Health;
    }

    //함수 HitFlick은 코룬틴 형식으로, 플레이어가 공격당헀을 경우 플레이어 스프라이트의 투명도를 조정하며 총 5번동안 깜박이는 것을 보여줌으로서 시각적으로 피격당한 상태라는 것을 들어내준다.
    IEnumerator HitFlick()
    {
        int flickCount = 0; // 깜박인 횟수를 기록하는 변수

        while (flickCount < 5) // 5번 깜박일 때까지 반복
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f); // 스프라이트를 투명도 0.5로 설정

            yield return new WaitForSeconds(0.1f); // 0.1초 대기

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); // 스프라이트를 원래 투명도로 설정

            yield return new WaitForSeconds(0.1f); // 0.1초 대기

            flickCount++; // 깜박인 횟수 증가
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")
            && !GameManager.Instance.GetPlayerCharacter().Invincibility
            && !GameManager.Instance.bStageCleared)
        {
            Health -= 1;
            StartCoroutine(HitFlick());

            //GameManager.Instance.SoundManager.PlaySFX("Hit");

            Destroy(collision.gameObject);

            if (Health <= 0)
            {
                GameManager.Instance.GetPlayerCharacter().DeadProcess();
            }
        }

        if (collision.gameObject.CompareTag("Boss") && !GameManager.Instance.GetPlayerCharacter().Invincibility
            && !GameManager.Instance.bStageCleared)
        {
            Health -= 1;
            StartCoroutine(HitFlick());

            if (Health <= 0)
            {
                GameManager.Instance.GetPlayerCharacter().DeadProcess();
            }
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            //GameManager.Instance.SoundManager.PlaySFX("GetItem");
            
            Destroy(collision.gameObject);
        }

        GameInstance.instance.CurrentPlayerHP = Health;
    }
}