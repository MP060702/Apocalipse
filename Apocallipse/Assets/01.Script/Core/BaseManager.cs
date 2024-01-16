using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���Ӹ޴����� �ڽ��� ���� �ڱ� �ڽ�Ŭ�����鿡�� ��밡���ϰ� ����� �ִ� ��Ȱ�� �ϴ� Ŭ�����̴�. 
public class BaseManager : MonoBehaviour
{   
    //������Ƽ���������� ���� �޴����� ������ �ڽ��� �ڽ� Ŭ���� ��ũ��Ʈ���� ���Ӹ޴����� ������ �����ϰ� �������
    protected GameManager _gameManager;

    public GameManager GameManager { get { return _gameManager; } }
    public virtual void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}