using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBalloon : MonoBehaviour
{
    public enum BalloonType
    {
        movingBalloon, //�����̴� ��ǳ��
        fixedBalloon   //�����Ǿ� �ִ� ��ǳ��
    }

    public float yOffset = 1.5f; //ĳ������ �Ӹ��� ��ǥ ������
    public float targetPositionX ;
    public float targetPositionY;
    public float destroyTime = 1.7f;

    public Image image;
    public Transform farmerTransform;
    public Transform emotionIcon;
    float moveSpeed = .5f;
    Vector3 vel = Vector3.zero;

    public BalloonType balloonType;
    void Start()
    {
        transform.position = new Vector3(farmerTransform.position.x, farmerTransform.position.y + yOffset, farmerTransform.position.z);
        targetPositionX = GameManager.Instance.camera1.transform.position.x - 2.3f; //�̸�Ƽ���� �̵��� ��ǥ.(�ټǰ����� ������ ���ڶ�) //���߿� �ٽ� �����ؾ��ҵ�. 2022.04.13
        targetPositionY = GameManager.Instance.camera1.transform.position.y + 3.66f;//�̸�Ƽ���� �̵��� ��ǥ.(�ټǰ����� ������ ���ڶ�)

        if (balloonType == BalloonType.movingBalloon)
            StartCoroutine(MovingFadeoutAnimation());
        else if (balloonType == BalloonType.fixedBalloon)
            StartCoroutine(FixedBalloonFadeoutAnimation());



        Destroy(gameObject, 1.7f);

    }

    // Update is called once per frame
    void Update()
    {
        if(balloonType == BalloonType.movingBalloon)
        emotionIcon.transform.position = Vector3.SmoothDamp(emotionIcon.transform.position, new Vector3(targetPositionX, targetPositionY, transform.position.z), ref vel, moveSpeed);

    }

    IEnumerator MovingFadeoutAnimation()
    {
        yield return new WaitForSeconds(.7f);

        for (float time = 1; time > 0; time -= .01f)
        {

            image.color = new Color(1, 1, 1, time);
            yield return new WaitForSeconds(.01f);

        }
    }
    IEnumerator FixedBalloonFadeoutAnimation()
    {
        for (float time = 1; time > 0; time -= .01f)
        {
            image.color = new Color(1, 1, 1, time);
            emotionIcon.GetComponent<Image>().color = new Color(1, 1, 1, time);
            yield return new WaitForSeconds(.01f);

        }
    }

}
