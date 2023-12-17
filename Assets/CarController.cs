/*
	- SwipeCar ����
	- [�˰���]
		- ������ ���۵Ǹ� ȭ�� ���� �Ʒ��� �ڵ����� ǥ�õǰ�, ȭ���� ���������ϸ� �ڵ����� �޸��� �����ϴ� ���� �����ϸ鼭 ����
		- ���������� ���̸� �����ؼ� �ڵ��� ����Ÿ��� �ٲ� �� ����
		- ȭ�� ������ �Ʒ����� ����� ǥ�õǰ� ȭ�� �߾ӿ��� �ڵ����� ��� ������ �Ÿ��� ǥ��
	- [������]
		- [1�ܰ�] : ȭ�鿡 ���� ������Ʈ�� ��� ����, �ڵ���, ���, ����, �Ÿ�ǥ�� UI
		- [2�ܰ�] : ������Ʈ�� ������ �� �ִ� ��Ʈ�ѷ� ��ũ��Ʈ �ۼ� : �ڵ��� ��Ʈ�ѷ�
		- [3�ܰ�] : ������Ʈ�� �ڵ����� ������ �� �ֵ��� ���ʷ����� ��ũ��Ʈ �ۼ�, �ڵ����� ��� ������ �Ÿ��� UI�� ǥ���ؾ� �ϹǷ� ���� ��ũ��Ʈ�� �ۼ�
		- [4�ܰ�] : UI�� ������ �� �ֵ��� ���� ��ũ��Ʈ �ۼ�, �ڵ����� ��� ������ �Ÿ��� UI�� ǥ���ؾ� �ϹǷ� ���� ��ũ��Ʈ�� �ۼ�
	- ������Ʈ
	- ������Ʈ ��Ī : Ch04_SwipeCar_20231106
	- �� ��Ī : GameScene
	- Ŭ���� ��Ī : CarController
 */

/*
    �÷���(Collections)�� C#���� �����ϴ� �ڷᱸ�� Ŭ����
    ���ʸ� �÷����� using ���ù����� System.Collections.Generic�� ������ �־�� �ϴµ�
    ����Ƽ���� �� C# ��ũ��Ʈ�� �����, �ڵ������� �����
    ù���� using�� �ٸ� lib�� �ڵ带 import�ϴ� ����̸� public class �� �ڹ��� ��ü ������, ���������� void Start()�� �ڹ��� �޼ҵ带 �ǹ�
    System.Collections, System.Collections.Generic�� �����͸� �����ϴ� �ڷᱸ������ ����
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float fCarSpeed = 0.0f;          //���� �ӵ� ����
    public bool isCarStartMoving = false;   //���� ��� ���� ����

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
        if(Input.GetMouseButtonDown(0))
        {
            vMouseStartPosition = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            /*
			 * ���콺 ��ư���� �հ����� ������ ��, ��ǥ, ������
			 * ���� ������ ���콺 ��ǥ : Input.mousePosition
			 */
            vMouseEndPosition = Input.mousePosition;

            //�������� ���� = ������ - �����
            fSwipeLength = -(vMouseEndPosition.x - vMouseStartPosition.x);

            //�������� ���̸� ó�� �ӵ��� ����
            fCarSpeed = fSwipeLength / 2400.0f;

            GetComponent<AudioSource>().Play();

            isCarStartMoving = true; //������ ����Ͽ����Ƿ� true
        }

        transform.Translate(fCarSpeed, 0, 0);

        fCarSpeed *= 0.98f;
    }
}
