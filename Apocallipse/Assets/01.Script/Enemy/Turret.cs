using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Turret : MonoBehaviour
{
    public Vector3 _direction;
    public GameObject ExplodeFX;
    public GameObject Projectile;
    public float ProjectileMoveSpeed = 5f;

    public Transform parentObject; // �θ� ������Ʈ�� ����Ű�� ����
    public float rotationSpeed = 5f; // ȸ�� �ӵ�

    [SerializeField]
    private float _lifeTime = 20f;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
        Vector3 pos = _direction * 3;
        transform.Translate(pos);
        parentObject = transform.parent;
        StartCoroutine(Shoot());
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

    void Update()
    {
        // ȸ�� ���� ���
        float angle = rotationSpeed * Time.deltaTime;

        // �θ� ������Ʈ ������ �ڽ� ������Ʈ ȸ��
        transform.RotateAround(parentObject.position, Vector3.forward, angle);

    }

    private IEnumerator Shoot()
    {
        GameObject manager = GameObject.Find("Managers");
        BaseCharacter character = manager.GetComponent<CharacterManager>().Player;

        // ���� 3: �� �� �������� �÷��̾�� �ϳ��� �߻�
        int numBullets = 5;
        float interval = 1.0f;

        for (int i = 0; i < numBullets; i++)
        {
            Vector3 playerPosition = character.CharacterManager.Player.transform.position;
            Vector3 playerDirection = (playerPosition - transform.position).normalized;
            ShootProjectile(transform.position, playerDirection);
            yield return new WaitForSeconds(interval);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}
