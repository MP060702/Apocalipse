using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern1 : MonoBehaviour
{
    Enemy enemy;
    public float MoveSpeed;
    public float Amplitude; // ������ ����(���Ʒ� �̵� �Ÿ�)

    private bool movingUp = true;
    private Vector3 startPosition;

    private Vector3 _direction;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        startPosition = transform.position;
    }

    void Update()
    {   
        if(enemy.blsfreeze == false)
        {
            float verticalMovement = MoveSpeed * Time.deltaTime;

            // ���� �̵� ���̸鼭 ���� ��ġ�� ���� ��ġ���� �������� ���� ���
            if (movingUp && transform.position.x < startPosition.x + Amplitude)
            {
                transform.position += new Vector3(verticalMovement, 0f, 0f);
            }
            // �Ʒ��� �̵� ���̸鼭 ���� ��ġ�� ���� ��ġ���� �������� ū ���
            else if (!movingUp && transform.position.x > startPosition.x - Amplitude)
            {
                transform.position -= new Vector3(verticalMovement, 0f, 0f);
            }
            // ���� ������ ��� ��� �̵� ������ �ݴ�� ����
            else
            {
                movingUp = !movingUp;
            }

            transform.position -= new Vector3(0f, MoveSpeed * Time.deltaTime, 0f);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}