using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expText : MonoBehaviour
{

    Text text;
    public Transform expTransform;
    float moveSpeed = 10f;

    void Start()
    {
        expTransform = GameObject.Find("expHolder").GetComponent<Transform>();
        text = GetComponent<Text>();
        transform.position = expTransform.position;
        StartCoroutine(FadeoutAnimation());
        Destroy(gameObject, 1.7f);

    }

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
