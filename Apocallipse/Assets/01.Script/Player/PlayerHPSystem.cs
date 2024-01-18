using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

//�÷��̾��� ü�� �ý����� �����ϴ� Ŭ������ GameInstace ��ũ��Ʈ���� �÷��̾��� ü�� ������ ������ ü�½ý����� �����ϸ�,
//����� ��ų�� ��ũ��Ʈ�� ����� ��ų�� ü�� ������ �� Ŭ������ ���Ͽ� ������.
public class PlayerHPSystem : MonoBehaviour
{   
    
    public int Health; // �÷��̾��� ü�°��� ������ �����μ� GameInstance �ȿ� �÷��̾� ü�������� ������ ���� �ִ´�. �ش� ������ ���� �����Ͽ� ü���� ������ �� ����.
    public int MaxHealth; // �÷��̾��� �ִ� ü�°��� �����ϴ� �Լ��μ� �÷��̾��� ü���� ȸ���Ǵ� �������� �ִ� ü�°��� ����� �ʵ��� ���ִ� �Ӱ����� ��Ȱ�� �Ѵ�.

    void Start()
    {
        Health = GameInstance.instance.CurrentPlayerHP;
    }
    //InitHealth�� Health�� ���� MaxHealth�� ���� �־��ְ�, GameInstance�� �÷��̾��� ü�°��� �ٽ� Health���� ����־� GameInstance�� �÷��̾��� ü�� ������ ������ �ִ� ��Ȱ�� �Ѵ�.
    public void InitHealth()
    {
        Health = MaxHealth;
        GameInstance.instance.CurrentPlayerHP = Health;
    }

    //�Լ� HitFlick�� �ڷ�ƾ ��������, �÷��̾ ���ݴ����� ��� �÷��̾� ��������Ʈ�� ������ �����ϸ� �� 5������ �����̴� ���� ���������μ� �ð������� �ǰݴ��� ���¶�� ���� ���ش�.
    IEnumerator HitFlick()
    {
        int flickCount = 0; // ������ Ƚ���� ����ϴ� ����

        while (flickCount < 5) // 5�� ������ ������ �ݺ�
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f); // ��������Ʈ�� ���� 0.5�� ����

            yield return new WaitForSeconds(0.1f); // 0.1�� ���

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); // ��������Ʈ�� ���� ������ ����

            yield return new WaitForSeconds(0.1f); // 0.1�� ���

            flickCount++; // ������ Ƚ�� ����
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