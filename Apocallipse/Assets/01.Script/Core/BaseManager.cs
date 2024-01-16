using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임메니저를 자신을 통해 자기 자식클래스들에게 사용가능하게 만들어 주는 역활을 하는 클래스이다. 
public class BaseManager : MonoBehaviour
{   
    //프로텍티드형식으로 게임 메니저를 가져와 자신의 자식 클래스 스크립트에서 게임메니저에 접근이 가능하게 만들어줌
    protected GameManager _gameManager;

    public GameManager GameManager { get { return _gameManager; } }
    public virtual void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}