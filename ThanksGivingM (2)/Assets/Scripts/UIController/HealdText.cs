using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealdText : MonoBehaviour
{
    Text text;
    public Transform farmerTransform;
    public float yOffset = 1.5f; //캐릭터의 머리위 좌표 오프셋
    float moveSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        transform.position = new Vector3(farmerTransform.position.x, farmerTransform.position.y + yOffset, farmerTransform.position.z);
        StartCoroutine(FadeoutAnimation());
        Destroy(gameObject, 1.7f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
    }
    IEnumerator FadeoutAnimation()
    {
        for (float time = 1; time > 0; time -= .01f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, time);
            yield return new WaitForSeconds(.01f);

        }
    }
}
