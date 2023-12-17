using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelbarrowController : MonoBehaviour
{
    public float fWheelBarrowSpeed = 0.0f;  //수레 속도 변수
    public bool isWheelBarrowStartMoving = false;   //수레 출발 여부 변수

    float fSwipeLength = 0.0f;  //스와이프 길이 변수

    Vector2 vMouseStartPosition = Vector2.zero; //마우스 클릭 위치 벡터 변수
    Vector2 vMouseEndPosition = Vector2.zero;   //마우스 클릭을 뗀 위치 벡터 변수

    // Start is called before the first frame update
    void Start()
    {
        /*
            - 프레임레이트를 60으로 고정
            - 어떤 성능의 컴퓨터에서 동작해도 같은 속도로 움직이도록 하는 처리
        */
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            vMouseStartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            /*
			 * 마우스 버튼에서 손가락을 떼었을 때, 좌표, 도착점
			 * 도착 지점의 마우스 좌표 : Input.mousePosition
			 */
            vMouseEndPosition = Input.mousePosition;

            //스와이프 길이 = 도착점 - 출발점
            fSwipeLength = -(vMouseEndPosition.x - vMouseStartPosition.x);

            //스와이프 길이를 처음 속도로 변경
            fWheelBarrowSpeed = fSwipeLength / 2400.0f;

            //GetComponent<AudioSource>().Play();

            isWheelBarrowStartMoving = true; //수레가 출발하였으므로 true
        }

        transform.Translate(fWheelBarrowSpeed, 0, 0);

        fWheelBarrowSpeed *= 0.98f; //수레 속도 감속
    }
}