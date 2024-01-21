using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ��ȯ�ϰ� Ư�� ī��Ʈ ���� ������ �������� �ִ� Ŭ����
public class EnemySpawnManager : BaseManager
{
    // ���͵��� ��� ���ӿ�����Ʈ ������ �迭 ����
    public GameObject[] Enemys;

    public GameObject Meteor;
    //���͵��� ��ȯ��ų ��ġ���� ���� ��� Ʈ������ ������ �迭 ����
    public Transform[] EnemySpawnTransform;
    // ���͸� ��ȯ��ų �� �� ��Ÿ���� ���� ��� �Ǽ��� ���� ����
    public float CoolDownTime;

    public float MeteoCoolDownTime;

    // ???
    public int MaxSpawnEnemyCount;

    public int StageLavel;

    //������ ������ ������ ������ 1�� �߰����ֱ� ���� ���� ��� ������ ���� ����
    private int _spawnCount = 0;
    //������ ��ȯ�Ǳ� ���� �����Ǿ�� �ϴ� ������ ������ ��� ������ �Լ� ����
    public int BossSpawnCount = 10;
    //������ �����Ǿ������� �Ǵ����ִ� ���� ������ ���� �������� ����
    private bool _bSpawnBoss = false;
    // ������ ���� ������Ʈ�� �������ִ� ���ӿ�����Ʈ�� ���� ����
    public GameObject BossA;
    public GameObject BossB;

    private GameObject SpawnBoss;


    void Start()
    {
        StageLavel = GameInstance.instance.CurrentStageLevel;
    }
    /*Init�� BaseManager�� ���󺯼� Init �������̵� ���ְ� Init�� base�Ͽ� ��ӹ޴� �θ��� ����� �����ϸ� 
    ���̽� �Ŵ����� Init�Լ��� �Ű������� �ִ� ���� �Ŵ����� �ҷ����� ��Ȱ�� �ϰ�, StartCoruntine �Լ��� ����
    �ڷ�ƾ SpawnEnemy�Լ��� �������ش�.*/

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

        // ������ �����Ǵ� ���ΰ� true�� �Ǳ� ���� ��� �ȿ� �ڵ带 �ݺ��ϴ� �ݺ���
        while (!_bSpawnBoss)
        {
            // WaitForSeconds�� ���� �ڷ�ƾ�� CoolDownTime�� ��ŭ�� ��� �ð��� ��
            yield return new WaitForSeconds(CoolDownTime);
            /*������ ���� spawnCount�� �����Լ��� ���� 1���� EnemySpawnTransform�迭�� ���̰� ������ ���� ��������
            ���� �־� �������ش�*/
            int spawnCount = Random.Range(1, EnemySpawnTransform.Length);
            /*List Ŭ������ ����Ÿ������ ���¸� �����ϰ�, �뷮�� EnemySpawnTransform�迭�� ���� ������ �����Ѵ�.*/
            List<int> availablePositions = new List<int>(EnemySpawnTransform.Length);
            // ���� i�� EnemySpawnTransform �迭�� ���� ������ Ŀ�������� 1�� �����ִ� �ݺ���
            for (int i = 0; i < EnemySpawnTransform.Length; i++)
            {
                //����Ʈ�� ��� i���� �߰����ش�.
                availablePositions.Add(i);
            }

            for (int i = 0; i < spawnCount; i++)
            {
                //������ ���� randomEnemy ������ 0���� Enemy�迭�氪�� ���������� ����
                int randomEnemy = Random.Range(0, Enemys.Length);
                /* ������ ���� randomPositionindex�� ������ �� 0���� availblePostion list�������� 1��ŭ ���� ���ȿ���
                �����Ѵ�*/
                int randomPositionIndex = Random.Range(0, availablePositions.Count - 1);
                // randomPosition�� list���� ������ ��ġ �ε������� �����Ѵ�.
                int randomPosition = availablePositions[randomPositionIndex];
                // ����Ʈ availablePositions�� randomindex��° ���Ҹ� �����Ѵ�.
                availablePositions.RemoveAt(randomPositionIndex);
                //���� �迭 ���ʹ��� ������ �ε��� ���� ������Ʈ��, �� ���� ��ġ �迭�� ������ �ε��� ���� ��ġ�� ��ȯ��Ű�� �ڵ�
                Instantiate(Enemys[randomEnemy], EnemySpawnTransform[randomPosition].position, Quaternion.identity);
            }

            // �ݺ����� �ݺ� �ɶ����� ����ī��Ʈ�� �����ش�.
            _spawnCount += spawnCount;

            //���� ����ī��Ʈ�� ���� ī��Ʈ���� �������ٸ� ���� ���� ������ true�� ���ְ�
            //������ Vector�� ��ġ�� �������ش�. 


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