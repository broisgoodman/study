using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FloatingJoystick joystick;

    public float default_moveSpeed = 5f;
    public float moveStageSpeed = 1f;
    public float rapidPlaySpeed = 1f;
    public float moveSpeed;


    public Vector2 moveVec;
    private Animator player_Animator;
    public Buff buff;
    

    private void Start()
    {
        moveSpeed = default_moveSpeed;
        player_Animator = GetComponent<Animator>();
    }

    void Update()
    {

        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;
     
        moveVec = new Vector2(moveX, moveY);



        #region �̵� ����

        VerticalLimit();
        HorizontalLimit();
        ZLayerCulc(); //Z�� �����ֱ�

        #endregion


        transform.Translate(moveVec * Time.deltaTime * moveSpeed);

        if(moveX != 0 || moveY != 0) //�����̰� ������
        {
            player_Animator.SetBool("isWalk", true);

            if(moveX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);


        }


        else
            player_Animator.SetBool("isWalk", false);


        if (buff.isRapidPlayOn)
        {
            rapidPlaySpeed = buff.rapidPlayValue;
           SetSpeed();
        }
        else
            rapidPlaySpeed = 1f;
           SetSpeed();



    }
    public void SetSpeed()
    {
        moveSpeed = default_moveSpeed * rapidPlaySpeed * moveStageSpeed;
    }


    private void VerticalLimit() //���� �̵��� �Ѱ����� ����ִ� �޼ҵ�
    {
        if (transform.position.y < 0.1f)   
        {
            transform.position = new Vector2(transform.position.x, 0.1f);
        }
        else if (transform.position.y > 4.8f)
        {
            transform.position = new Vector2(transform.position.x, 4.8f);
        }
    }

    private void HorizontalLimit() //���� �̵��� �Ѱ����� ����ִ� �޼ҵ�
    {
        if(GameManager.Instance.camera1.transform.position.x - 6.7f > transform.position.x) //ī�޶��� �߽� ��ǥ�� �������� ����� ��.
        {
            transform.position = new Vector2(GameManager.Instance.camera1.transform.position.x - 6.7f, transform.position.y);
        }
        else if(GameManager.Instance.camera1.transform.position.x + 7 < transform.position.x)
        {
            transform.position = new Vector2(GameManager.Instance.camera1.transform.position.x + 7, transform.position.y);
        }
    }

    private void ZLayerCulc() //Y�� �̵��� ���� Z���� �ٲ��ִ� �޼ҵ�
    {

        if (transform.position.y < 0.4f) // ������ �ʹ� ����ϴ�. �۹��� �ص��� ���� �� z���� ��ȭ�ϰ� ����.
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.4f);
        }
        else if (0.4f < transform.position.y && transform.position.y < 1.3f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.4f);
        }
        else if (1.3f < transform.position.y && transform.position.y < 2.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1.4f); //3�� ��
        }
        else if (2.5f < transform.position.y && transform.position.y < 3.6f)//����
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 2.4f);
        }
        else if (3.6f < transform.position.y && transform.position.y < 5.3f)//����
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 3.4f);
        }
        else if (5.3f < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 4.4f);
        }
    }

}
