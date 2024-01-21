using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//적들을 소환하고 특정 카운트 이후 보스를 스폰시켜 주는 클래스
public class EnemySpawnManager : BaseManager
{
    // 몬스터들을 담는 게임오브젝트 형식의 배열 선언
    public GameObject[] Enemys;

    public GameObject Meteor;
    //몬스터들을 소환시킬 위치들의 값을 담는 트렌스폼 형식의 배열 선언
    public Transform[] EnemySpawnTransform;
    // 몬스터를 소환시킬 때 줄 쿨타임의 값을 담는 실수형 변수 선언
    public float CoolDownTime;

    public float MeteoCoolDownTime;

    // ???
    public int MaxSpawnEnemyCount;

    public int StageLavel;

    //몬스터의 스폰될 때마다 갯수를 1씩 추가해주기 위한 값을 담는 정수형 변수 선언
    private int _spawnCount = 0;
    //보스가 소환되기 위해 스폰되어야 하는 몬스터의 개수을 담는 정수형 함수 선언
    public int BossSpawnCount = 10;
    //보스가 스폰되었는지를 판단해주는 논리형 변수의 값을 거짓으로 선언
    private bool _bSpawnBoss = false;
    // 스폰될 보스 오브젝트를 가지고있는 게임오브젝트형 변수 선언
    public GameObject BossA;
    public GameObject BossB;

    private GameObject SpawnBoss;


    void Start()
    {
        StageLavel = GameInstance.instance.CurrentStageLevel;
    }
    /*Init을 BaseManager에 가상변수 Init 오버라이드 해주고 Init을 base하여 상속받는 부모의 기능을 유지하며 
    베이스 매니저의 Init함수에 매개변수로 있는 게임 매니저를 불러오는 역활을 하고, StartCoruntine 함수를 통해
    코룬틴 SpawnEnemy함수를 시작해준다.*/

    void Update()
    {
        if (StageLavel > 1)
        {
            BossSpawnCount = 20;
        }
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnMeteor());
    }

    IEnumerator SpawnEnemy()
    {

        // 보스가 스폰되는 여부가 true가 되기 까지 계속 안에 코드를 반복하는 반복문
        while (!_bSpawnBoss)
        {
            // WaitForSeconds를 통해 코룬틴에 CoolDownTime초 만큼의 대기 시간을 줌
            yield return new WaitForSeconds(CoolDownTime);
            /*정수형 변수 spawnCount에 램덤함수를 통해 1부터 EnemySpawnTransform배열의 길이값 까지의 수를 랜덤으로
            값을 넣어 정의해준다*/
            int spawnCount = Random.Range(1, EnemySpawnTransform.Length);
            /*List 클래스를 정수타입으로 형태를 지정하고, 용량을 EnemySpawnTransform배열의 길이 값으로 선언한다.*/
            List<int> availablePositions = new List<int>(EnemySpawnTransform.Length);
            // 정수 i가 EnemySpawnTransform 배열의 길이 값보다 커질때까지 1씩 더해주는 반복문
            for (int i = 0; i < EnemySpawnTransform.Length; i++)
            {
                //리스트에 요소 i값을 추가해준다.
                availablePositions.Add(i);
            }

            for (int i = 0; i < spawnCount; i++)
            {
                //정수형 변수 randomEnemy 랜덤값 0에서 Enemy배열길값의 랜덤값으로 선언
                int randomEnemy = Random.Range(0, Enemys.Length);
                /* 정수형 변수 randomPositionindex에 랜덤한 값 0에서 availblePostion list갯수에서 1만큼 빼준 값안에서
                선언한다*/
                int randomPositionIndex = Random.Range(0, availablePositions.Count - 1);
                // randomPosition에 list에서 랜덤한 위치 인덱스값을 선언한다.
                int randomPosition = availablePositions[randomPositionIndex];
                // 리스트 availablePositions의 randomindex번째 원소를 삭제한다.
                availablePositions.RemoveAt(randomPositionIndex);
                //적을 배열 에너미의 랜덤한 인덱스 값의 오브젝트로, 적 스폰 위치 배열의 랜덤한 인덱스 값의 위치로 소환시키는 코드
                Instantiate(Enemys[randomEnemy], EnemySpawnTransform[randomPosition].position, Quaternion.identity);
            }

            // 반복문이 반복 될때마다 스폰카운트를 높여준다.
            _spawnCount += spawnCount;

            //만약 스폰카운트가 몬스터 카운트보다 높아졌다면 보스 스폰 논리형을 true로 해주고
            //보스를 Vector값 위치에 스폰해준다. 


            if (_spawnCount >= BossSpawnCount)
            {
                switch (StageLavel)
                {
                    case 1:
                        SpawnBoss = BossA;
                        break;
                    case 2:
                        SpawnBoss = BossB;
                        break;
                }

                _bSpawnBoss = true;
            }
            //Instantiate(SpawnBoss, new Vector3(EnemySpawnTransform[1].position.x, EnemySpawnTransform[1].position.y + 1, 0f), Quaternion.identity);
        }
    }

    IEnumerator SpawnMeteor()
    {   
        while(true) 
        {   
            yield return new WaitForSeconds(MeteoCoolDownTime);

            List<int> availablePositions = new List<int>(EnemySpawnTransform.Length);

            for (int i = 0; i < EnemySpawnTransform.Length; i++)
            {
                availablePositions.Add(i);
            }          

            int randomPositionIndex = Random.Range(0, availablePositions.Count - 1);
            int randomPosition = availablePositions[randomPositionIndex];
            availablePositions.RemoveAt(randomPositionIndex);
            
            Instantiate(Meteor, EnemySpawnTransform[randomPosition].position, Quaternion.identity);;
        }
    }

}