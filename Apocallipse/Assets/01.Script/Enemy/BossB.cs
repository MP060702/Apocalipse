using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;

public class BossB : MonoBehaviour
{   //게임오브젝트 형식의 프로젝타일 변수를 선언하여 사용할 총알을 받아올 수 있다.
    public GameObject Projectile;
    
    public float ProjectileMoveSpeed = 5.0f;
    public float FireRate = 2.0f;
    public float MoveSpeed = 2.0f;
    public float MoveDistance = 5.0f;
    public float EnemyMoveSpeed = 0f;
   
    public GameObject Turret;
    private int _currentPatternIndex = 0;
    private bool _movingRight = true;
    private bool _bCanMove = false;
    private Vector3 _originPosition;
    GameObject ForDestroy;
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
        // 패턴 인덱스를 증가시키고, 마지막 패턴일 경우 다시 처음 패턴으로 돌아감
        _currentPatternIndex = (_currentPatternIndex + 1) % 4;

        // 현재 패턴 실행
        switch (_currentPatternIndex)
        {
            case 0:
                //Pattern1();
                break;
            case 1:
                StartCoroutine(Pattern2());
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

        GameObject instance = Instantiate(Turret, position, Quaternion.identity);
        Turret Turrets = instance.GetComponent<Turret>();
        instance.transform.parent = gameObject.transform;

        if (Turrets != null)
        {
            Turrets.SetDirection(direction);
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
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject turret in turrets)
        {
            Destroy(turret);
        }

        for (int i = 0; i < 360; i += 45)
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            SpawnTurret(transform.position, direction);
        }
    }
     private IEnumerator Pattern2()
     {
        gameObject.layer = 6;
        GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);

        yield return new WaitForSeconds(4);

        gameObject.layer = 0;
        GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1f);

    }

    private IEnumerator Pattern3()
    {
        // 패턴 3: 몇 초 간격으로 플레이어에게 하나씩 발사
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