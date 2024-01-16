using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

//게임 내의 주요 정보들의 값을 저장하거나 조정하는 역활을 하는 클래스로 주요 정보들을 다른 스크립트에서도 가져가 사용할 수 있게 하는 저장소의 역활을 하기도 한다.
public class GameInstance : MonoBehaviour
{   
    // 전역변수로서 Gameinstance의 변수나 함수들을 외부에서 사용 가능하게 해주는 역활을 한다.
    public static GameInstance instance;
    // 게임 시작 시간을 정하는 실수형 변수이다.
    public float GameStartTime = 0f;
    // 게임의 점수 역활을 하는 정수형 변수이다.
    public int Score = 0;
    // 스테이지의 단계를 정해주는 값의 역활을 하는 정수형 변수이다.
    public int CurrentStageLevel = 1;
    // 플레이어의 무기 레벨의 값의 역활을 하는 정수형 변수이다 값이 높아지면서 다른 종류의 공격 방식이 된다.
    public int CurrentPlayerWeaponLevel = 0;
    // 플레이어의 체력값을 담당하는 정수형 함수이다.
    public int CurrentPlayerHP = 3;
    // 플레이어의 연료값을 담당하는 실수형 변수이다.
    public float CurrentPlayerFuel = 100f;

    private void Awake()
    {
        if (instance == null)  // 단 하나만 존재하게끔
        {
            instance = this;  // 객체 생성시 instance에 자기 자신을 넣어줌
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        GameStartTime = Time.time;
    }
}