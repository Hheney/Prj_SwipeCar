using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  �ϵ� ��� ���� ��Ʈ�ѷ� ��ũ��Ʈ
 */

public class WheelbarrowHardController : MonoBehaviour
{
    public float fWheelBarrowSpeed = 0.0f;  //���� �ӵ� ����
    public bool isWheelBarrowStartMoving = false;   //���� ��� ���� ����

    float fSwipeLength = 0.0f;  //�������� ���� ����

    Vector2 vMouseStartPosition = Vector2.zero; //���콺 Ŭ�� ��ġ ���� ����
    Vector2 vMouseEndPosition = Vector2.zero;   //���콺 Ŭ���� �� ��ġ ���� ����

    // Start is called before the first frame update
    void Start()
    {
        /*
            - �����ӷ���Ʈ�� 60���� ����
            - � ������ ��ǻ�Ϳ��� �����ص� ���� �ӵ��� �����̵��� �ϴ� ó��
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
			 * ���콺 ��ư���� �հ����� ������ ��, ��ǥ, ������
			 * ���� ������ ���콺 ��ǥ : Input.mousePosition
			 */
            vMouseEndPosition = Input.mousePosition;

            //�������� ���� = ������ - �����
            fSwipeLength = -(vMouseEndPosition.x - vMouseStartPosition.x);

            //�������� ���̸� ó�� �ӵ��� ����
            fWheelBarrowSpeed = fSwipeLength / 400.0f;

            //GetComponent<AudioSource>().Play();

            isWheelBarrowStartMoving = true; //������ ����Ͽ����Ƿ� true
        }

        transform.Translate(fWheelBarrowSpeed, 0, 0);

        fWheelBarrowSpeed *= 0.98f; //���� �ӵ� ����
    }
}