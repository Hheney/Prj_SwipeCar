using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //�� ������ ���� ���Ŵ�����Ʈ �߰�
using UnityEngine.UI;

/*
 *  �ϵ� ��� ���� ���� ��ũ��Ʈ
 */

public class GameHardDirector : MonoBehaviour
{
    //CarController carController; //������ ����
    WheelbarrowHardController wheelHardController; //������ ����


    GameObject gWheelbarrow = null; //���� GameObject ����
    GameObject gGoldenChest = null; //�������� GameObject ����
    GameObject gDistance = null;    //�Ÿ� GameObject ����
    GameObject gScore = null;       //���� GameObject ����

    //private �������� ���
    private const float fLeftGoldenChestXPos = 5.0f;        //��� ���� ���� ��ǥ��
    private const float fRightGoldenChestXpos = 10.0f;      //��� ������ ���� ��ǥ��
    private const float fWheelbarrowInitXpos = -7.0f;       //���� ������Ʈ �ʱ� X��ǥ��
    private const float fWheelbarrowInitYpos = -3.8f;       //���� ������Ʈ �ʱ� Y��ǥ��
    private const float fWallXpos = 10.0f;                  //ȭ�� ������ �ڵ����� �����°��� �����ϱ� ���� ���� X��ǥ��
    private const float fBoundarySpeed = 0.002f;             //������ ���߾����� �Ǵ��ϴ� ��� ��

    //private �������� ���� 
    private float fDistanceLength = 0.0f;   //���� �Ÿ� ����
    private int nScore = 0,                 //���� ����
                nCount = 10;                //���� Ƚ�� ����

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        //gCar = GameObject.Find("car");
        //gFlag = GameObject.Find("flag");
        //carController = gCar.GetComponent<CarController>();

        gWheelbarrow = GameObject.Find("Wheelbarrow");
        gGoldenChest = GameObject.Find("GoldenChest");
        gDistance = GameObject.Find("txtDistance");
        gScore = GameObject.Find("ScoreBoard");
        wheelHardController = gWheelbarrow.GetComponent<WheelbarrowHardController>();
    }

    // Update is called once per frame
    void Update()
    {
        fDistanceLength = gGoldenChest.transform.position.x - gWheelbarrow.transform.position.x;

        //gDistance.GetComponent<Text>().text = "��ǥ �������� : " + fDistanceLength.ToString("F2") + 'm';
        gDistance.GetComponent<Text>().text = $"��ǥ �������� : {fDistanceLength.ToString("F2")} {'m'}";

        //gScore.GetComponent<Text>().text = "���� : " + nScore.ToString("D") + " ��" + "���� ��ȸ : " + nCount.ToString("D") + " ȸ";
        gScore.GetComponent<Text>().text = $"���� {nScore.ToString("D")}, ���� ��ȸ {nCount.ToString("D")} ȸ"; //����, ���� ��ȸ ���

        CarStopped(); //������ ����� ����

        f_EndGame();
    }

    void CarPosInit()
    {
        //carController.transform.position = new Vector2(fWheelbarrowInitXpos, fWheelbarrowInitYpos); //�ʱ� ��ġ�� x, y��ǥ ������ �̵�
        wheelHardController.transform.position = new Vector2(fWheelbarrowInitXpos, fWheelbarrowInitYpos); //�ʱ� ��ġ�� x, y��ǥ ������ �̵�
    }

    void CarStopped()
    {
        /*
        //if(������ ������������ ����Ͽ���? && ������ �ӵ��� �������� ������?)                                            
        if (carController.isCarStartMoving == true && carController.fCarSpeed < fBoundarySpeed || 
            carController.transform.position.x > fWallXpos) //or �ڵ����� ��ġ�� ������ ���� �Ѿ ���
        {
            WithinRange(); //������ ����� ���� ���� �ִ°�?
            CarReset(); //���� �ʱ�ȭ
        }*/

        if (wheelHardController.isWheelBarrowStartMoving == true && wheelHardController.fWheelBarrowSpeed < fBoundarySpeed ||
            wheelHardController.transform.position.x > fWallXpos) //or �ڵ����� ��ġ�� ������ ���� �Ѿ ���
        {
            WithinRange(); //������ ����� ���� ���� �ִ°�?
            CarReset(); //���� �ʱ�ȭ
        }
    }

    void CarReset()
    {
        CarPosInit(); //��ġ �ʱ�ȭ
        wheelHardController.fWheelBarrowSpeed = 0; //�ӵ� �ʱ�ȭ, 0���� �ʱ�ȭ���� ���� ��� ��ġ �ʱ�ȭ �� ���������� ������ �߻�
        wheelHardController.isWheelBarrowStartMoving = false; //������ ���߾����Ƿ� false�� ����
        nCount--; //���� Ƚ�� ����
    }

    void WithinRange()
    {
        //������ x��ǥ���� ����� ���� ���� �����Ѵٸ�
        if (wheelHardController.transform.position.x >= fLeftGoldenChestXPos
            && wheelHardController.transform.position.x <= fRightGoldenChestXpos)
        {
            GetComponent<AudioSource>().Play();
            nScore += 10; //���� 10�� �߰�
        }
    }

    void f_EndGame()
    {
        //���� ��ȸ�� 0�� ��� ���������� ����
        if (nCount == 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}


