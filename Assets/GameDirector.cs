using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; //�� ������ ���� ���Ŵ�����Ʈ �߰�
using UnityEngine.UI;

/*
    C#������ #define ��ó���� ���ù��� ����Ͽ� �Ϲ������� C�� C++���� ���Ǵ� ������� ����� ������ �� ����.
    ������ �ƴ� ����� �����ϱ� ���� ���� Ŭ������ �����Ͽ���.
    Rigidbody�� ������� �ʰ� ��߿� ��Ҵ��� �����ϱ� ���� ��ǥ�� ����� �߰���
*/
static class ConstValue
{
    public const float fLeftFlagXPos = 5.0f;    //��� ���� ���� ��ǥ��
    public const float fRightFlagXpos = 10.0f;  //��� ������ ���� ��ǥ��
    public const float fBoundarySpeed = 0.002f; //������ ���߾����� �Ǵ��ϴ� ��� ��

    public const float fCarInitXpos = -7.0f; //Car ������Ʈ �ʱ� X��ǥ��
    public const float fCarInitYpos = -3.7f; //Car ������Ʈ �ʱ� Y��ǥ��
    public const float fWallXpos = 10.0f;    //ȭ�� ������ �ڵ����� �����°��� �����ϱ� ���� ���� X��ǥ��
}

public class GameDirector : MonoBehaviour
{

    CarController carController; //������ ����

    GameObject gCar = null;         //�ڵ��� GameObject ����
    GameObject gFlag = null;        //��� GameObject ����
    GameObject gDistance = null;    //�Ÿ� GameObject ����
    GameObject gScore = null;       //���� GameObject ���� 

    float fDistanceLength = 0.0f;   //���� �Ÿ� ����
    int nScore = 0,                 //���� ���� 
        nCount = 10;                //���� Ƚ�� ����        

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
       
        //gDistance.GetComponent<Text>().text = "��ǥ �������� : " + fDistanceLength.ToString("F2") + 'm';
        gDistance.GetComponent<Text>().text = $"��ǥ �������� : {fDistanceLength.ToString("F2")} {'m'}";

        //gScore.GetComponent<Text>().text = "���� : " + nScore.ToString("D") + " ��" + "���� ��ȸ : " + nCount.ToString("D") + " ȸ";
        gScore.GetComponent<Text>().text = 
            $"���� {nScore.ToString("D")}, ���� ��ȸ {nCount.ToString("D")} ȸ"; //����, ���� ��ȸ ���

        CarStopped(); //������ ����� ����

        EndGame();
    }
    
    void CarPosInit()
    {
        carController.transform.position = new Vector2(ConstValue.fCarInitXpos, ConstValue.fCarInitYpos); //�ʱ� ��ġ�� x, y��ǥ ������ �̵�
    }

    void CarStopped()
    {
        //if(������ ������������ ����Ͽ���? && ������ �ӵ��� �������� ������?)                                            
        if (carController.isCarStartMoving == true && carController.fCarSpeed < ConstValue.fBoundarySpeed || 
            carController.transform.position.x > ConstValue.fWallXpos) //or �ڵ����� ��ġ�� ������ ���� �Ѿ ���
        {
            WithinRange(); //������ ����� ���� ���� �ִ°�?
            CarReset(); //���� �ʱ�ȭ
        }
    }

    void CarReset()
    {
        CarPosInit(); //��ġ �ʱ�ȭ
        carController.fCarSpeed = 0; //�ӵ� �ʱ�ȭ, 0���� �ʱ�ȭ���� ���� ��� ��ġ �ʱ�ȭ �� ���������� ������ �߻�
        carController.isCarStartMoving = false; //������ ���߾����Ƿ� false�� ����
        nCount--; //���� Ƚ�� ����
    }

    void WithinRange()
    {
        //������ x��ǥ���� ����� ���� ���� �����Ѵٸ�
        if(carController.transform.position.x >= ConstValue.fLeftFlagXPos 
            && carController.transform.position.x <= ConstValue.fRightFlagXpos)
        {
            nScore += 10; //���� 10�� �߰�
        }
    }

    void EndGame()
    {
        //���� ��ȸ�� 0�� ��� ���������� ����
        if (nCount == 0) 
        {
            SceneManager.LoadScene("EndScene"); 
        }
    }

    public void GameReStart()
    {
        //�� �޼ҵ尡 ȣ��� ��� ���Ӿ����� ����
        SceneManager.LoadScene("GameScenes");
    }

}


