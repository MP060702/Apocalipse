using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

//���� ���� �ֿ� �������� ���� �����ϰų� �����ϴ� ��Ȱ�� �ϴ� Ŭ������ �ֿ� �������� �ٸ� ��ũ��Ʈ������ ������ ����� �� �ְ� �ϴ� ������� ��Ȱ�� �ϱ⵵ �Ѵ�.
public class GameInstance : MonoBehaviour
{   
    // ���������μ� Gameinstance�� ������ �Լ����� �ܺο��� ��� �����ϰ� ���ִ� ��Ȱ�� �Ѵ�.
    public static GameInstance instance;
    // ���� ���� �ð��� ���ϴ� �Ǽ��� �����̴�.
    public float GameStartTime = 0f;
    // ������ ���� ��Ȱ�� �ϴ� ������ �����̴�.
    public int Score = 0;
    // ���������� �ܰ踦 �����ִ� ���� ��Ȱ�� �ϴ� ������ �����̴�.
    public int CurrentStageLevel = 1;
    // �÷��̾��� ���� ������ ���� ��Ȱ�� �ϴ� ������ �����̴� ���� �������鼭 �ٸ� ������ ���� ����� �ȴ�.
    public int CurrentPlayerWeaponLevel = 0;
    // �÷��̾��� ü�°��� ����ϴ� ������ �Լ��̴�.
    public int CurrentPlayerHP = 3;
    // �÷��̾��� ���ᰪ�� ����ϴ� �Ǽ��� �����̴�.
    public float CurrentPlayerFuel = 100f;

    private void Awake()
    {
        if (instance == null)  // �� �ϳ��� �����ϰԲ�
        {
            instance = this;  // ��ü ������ instance�� �ڱ� �ڽ��� �־���
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        GameStartTime = Time.time;
    }
}