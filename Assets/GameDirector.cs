using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; //씬 변경을 위해 씬매니지먼트 추가
using UnityEngine.UI;

/*
    C#에서는 #define 전처리기 지시문을 사용하여 일반적으로 C와 C++에서 사용되는 방식으로 상수를 정의할 수 없다.
    정수가 아닌 상수를 정의하기 위해 정적 클래스를 선언하였다.
    Rigidbody를 사용하지 않고 깃발에 닿았는지 감지하기 위해 좌표값 상수를 추가함
*/
static class ConstValue
{
    public const float fLeftFlagXPos = 5.0f;    //깃발 왼쪽 범위 좌표값
    public const float fRightFlagXpos = 10.0f;  //깃발 오른쪽 범위 좌표값
    public const float fBoundarySpeed = 0.002f; //차량이 멈추었는지 판단하는 경계 값

    public const float fCarInitXpos = -7.0f; //Car 오브젝트 초기 X좌표값
    public const float fCarInitYpos = -3.7f; //Car 오브젝트 초기 Y좌표값
    public const float fWallXpos = 10.0f;    //화면 밖으로 자동차가 나가는것을 방지하기 위한 벽의 X좌표값
}

public class GameDirector : MonoBehaviour
{

    CarController carController; //대행자 선언

    GameObject gCar = null;         //자동차 GameObject 변수
    GameObject gFlag = null;        //깃발 GameObject 변수
    GameObject gDistance = null;    //거리 GameObject 변수
    GameObject gScore = null;       //점수 GameObject 변수 

    float fDistanceLength = 0.0f;   //남은 거리 변수
    int nScore = 0,                 //점수 변수 
        nCount = 10;                //남은 횟수 변수        

    // Start is called before the first frame update
    void Start()
    {
        gCar = GameObject.Find("car");
        gFlag = GameObject.Find("flag");
        gDistance = GameObject.Find("txtDistance");
        gScore = GameObject.Find("ScoreBoard");
        carController = gCar.GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        fDistanceLength = gFlag.transform.position.x - gCar.transform.position.x;
       
        //gDistance.GetComponent<Text>().text = "목표 지점까지 : " + fDistanceLength.ToString("F2") + 'm';
        gDistance.GetComponent<Text>().text = $"목표 지점까지 : {fDistanceLength.ToString("F2")} {'m'}";

        //gScore.GetComponent<Text>().text = "점수 : " + nScore.ToString("D") + " 점" + "남은 기회 : " + nCount.ToString("D") + " 회";
        gScore.GetComponent<Text>().text = 
            $"점수 {nScore.ToString("D")}, 남은 기회 {nCount.ToString("D")} 회"; //점수, 남은 기회 출력

        CarStopped(); //차량이 출발후 멈춤

        EndGame();
    }
    
    void CarPosInit()
    {
        carController.transform.position = new Vector2(ConstValue.fCarInitXpos, ConstValue.fCarInitYpos); //초기 위치인 x, y좌표 값으로 이동
    }

    void CarStopped()
    {
        //if(차량이 시작지점에서 출발하였나? && 차량의 속도가 변수보다 낮은가?)                                            
        if (carController.isCarStartMoving == true && carController.fCarSpeed < ConstValue.fBoundarySpeed || 
            carController.transform.position.x > ConstValue.fWallXpos) //or 자동차의 위치가 오른쪽 벽을 넘어갈 경우
        {
            WithinRange(); //차량이 깃발의 범위 내에 있는가?
            CarReset(); //차량 초기화
        }
    }

    void CarReset()
    {
        CarPosInit(); //위치 초기화
        carController.fCarSpeed = 0; //속도 초기화, 0으로 초기화하지 않을 경우 위치 초기화 후 지속적으로 움직임 발생
        carController.isCarStartMoving = false; //차량이 멈추었으므로 false로 변경
        nCount--; //게임 횟수 차감
    }

    void WithinRange()
    {
        //차량의 x좌표값이 상수값 범위 내에 존재한다면
        if(carController.transform.position.x >= ConstValue.fLeftFlagXPos 
            && carController.transform.position.x <= ConstValue.fRightFlagXpos)
        {
            nScore += 10; //점수 10점 추가
        }
    }

    void EndGame()
    {
        //남은 기회가 0일 경우 엔딩씬으로 변경
        if (nCount == 0) 
        {
            SceneManager.LoadScene("EndScene"); 
        }
    }

    public void GameReStart()
    {
        //이 메소드가 호출될 경우 게임씬으로 변경
        SceneManager.LoadScene("GameScenes");
    }

}


