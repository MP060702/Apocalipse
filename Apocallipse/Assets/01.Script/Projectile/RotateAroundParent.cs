using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundParent : MonoBehaviour
{
    public Transform parentObject; // �θ� ������Ʈ�� ����Ű�� ����
    public float rotationSpeed = 5f; // ȸ�� �ӵ�

    void Start()
    {
        parentObject = transform.parent;
    }
    void Update()
    {
        // ȸ�� ���� ���
        float angle = rotationSpeed * Time.deltaTime;

        // �θ� ������Ʈ ������ �ڽ� ������Ʈ ȸ��
        transform.RotateAround(parentObject.position, Vector3.forward, angle);
    }
}
