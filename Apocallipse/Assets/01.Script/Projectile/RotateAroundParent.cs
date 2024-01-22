using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundParent : MonoBehaviour
{
    public Transform parentObject; // 부모 오브젝트를 가리키는 변수
    public float rotationSpeed = 5f; // 회전 속도

    void Start()
    {
        parentObject = transform.parent;
    }
    void Update()
    {
        // 회전 각도 계산
        float angle = rotationSpeed * Time.deltaTime;

        // 부모 오브젝트 주위로 자식 오브젝트 회전
        transform.RotateAround(parentObject.position, Vector3.forward, angle);
    }
}
