using System.Collections;
using UnityEngine;

public class BossB : MonoBehaviour
{   //���ӿ�����Ʈ ������ ������Ÿ�� ������ �����Ͽ� ����� �Ѿ��� �޾ƿ� �� �ִ�.
    public GameObject Projectile;
    
    public float ProjectileMoveSpeed = 5.0f;
    public float FireRate = 2.0f;
    public float MoveSpeed = 2.0f;
    public float MoveDistance = 5.0f;
    public float EnemyMoveSpeed = 0f;
    public GameObject EnemyB;

    private int _currentPatternIndex = 0;
    private bool _movingRight = true;
    private bool _bCanMove = false;
    private Vector3 _originPosition;

    public GameObject SpawnObjs;

    private void Start()
    {
        Debug.Log(Mathf.Rad2Deg);
        _originPosition = transform.position;
        StartCoroutine(MoveDownAndStartPattern());
    }

    private IEnumerator MoveDownAndStartPattern()
    {
        while (transform.position.y > _originPosition.y - 3f)
        {
            transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
            yield return null;
        }

        _bCanMove = true;
        InvokeRepeating("NextPattern", 2.0f, FireRate);
    }

    private void Update()
    {
        if (_bCanMove)
            MoveSideways();
    }

    private void NextPattern()
    {
        // ���� �ε����� ������Ű��, ������ ������ ��� �ٽ� ó�� �������� ���ư�
        _currentPatternIndex = (_currentPatternIndex + 1) % 4;

        // ���� ���� ����
        switch (_currentPatternIndex)
        {
            case 0:
                Pattern1();
                break;
            case 1:
                //Pattern2();
                break;
            case 2:
                //StartCoroutine(Pattern3());
                break;
            case 3:
                //Pattern4();
                break;
          
        }
    }

    private void MoveSideways()
    {
        if (_movingRight)
        {
            transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
            if (transform.position.x > MoveDistance)
            {
                _movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
            if (transform.position.x < -MoveDistance)
            {
                _movingRight = true;
            }
        }
    }

    private void StartMovingSideways()
    {
        StartCoroutine(MovingSidewaysRoutine());
    }

    private IEnumerator MovingSidewaysRoutine()
    {
        while (true)
        {
            MoveSideways();
            yield return null;
        }
    }

    
    public void ShootProjectile(Vector3 position, Vector3 direction)
    {
        GameObject instance = Instantiate(Projectile, position, Quaternion.identity);
        Projectile projectile = instance.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.MoveSpeed = ProjectileMoveSpeed;
            projectile.SetDirection(direction.normalized);
        }
    }

    public void SpawnTurret(Vector3 position, Vector3 direction)
    {
        GameObject instance = Instantiate(EnemyB, position, Quaternion.identity);
        EnemyPattern2 Turret = instance.GetComponent<EnemyPattern2>();
        instance.transform.parent = gameObject.transform;

        if (Turret != null)
        {   
            Turret.SetDirection(direction.normalized);
        }
    }

    public void SpawnObjects(Vector3 position, Vector3 direction)
    {
        GameObject instance1 = Instantiate(SpawnObjs, position, Quaternion.identity);
        EnemyPattern1 Enemy1 = instance1.GetComponent<EnemyPattern1>();

        if(Enemy1 != null)
        {
            Enemy1.MoveSpeed = EnemyMoveSpeed;
            Enemy1.SetDirection(direction.normalized);
        }

    }

    private void Pattern1()
    {
        SpawnTurret(transform.position, transform.position);
    }
        private void Pattern2()
    {
        // ���� 2: ��������� �Ѿ� �߻�
        int numBullets2 = 12;
        float angleStep2 = 360.0f / numBullets2;

        for (int i = 0; i < numBullets2; i++)
        {
            float angle2 = i * angleStep2;
            float radian2 = angle2 * Mathf.Deg2Rad;
            Vector3 direction2 = new Vector3(Mathf.Cos(radian2), Mathf.Sin(radian2), 0);

            ShootProjectile(transform.position, direction2);
        }


    }

    private IEnumerator Pattern3()
    {
        // ���� 3: �� �� �������� �÷��̾�� �ϳ��� �߻�
        int numBullets = 5;
        float interval = 1.0f;

        for (int i = 0; i < numBullets; i++)
        {
            Vector3 playerDirection = (PlayerPosition() - transform.position).normalized;
            ShootProjectile(transform.position, playerDirection);
            yield return new WaitForSeconds(interval);
        }
    }

    private void Pattern4()
    {
        SpawnObjects(transform.position, transform.position);
    }


    private Vector3 PlayerPosition()
    {
        return GameManager.Instance.GetPlayerCharacter().transform.position;
    }

    private void OnDestroy()
    {
        //GameManager.Instance.StageClear();
    }
}