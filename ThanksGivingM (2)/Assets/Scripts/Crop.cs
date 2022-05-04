using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public int cropLevel;   //�۹� ����
    public Sprite deadCrop; //��Ȯ �� �۹� �̹���
    public float hp;        //�۹� ü��
    public int har;         //��Ȯ��


    private Animator animator;

    public bool harvested;

    void Start()
    {
        animator = GetComponent<Animator>();

        harvested = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0 && !harvested) 
        {
            animator.SetTrigger("Dead");
            harvested = true;
            GameManager.Instance.total_yield += har;
        }
        if (transform.position.x < GameManager.Instance.camera1.transform.position.x - 60)
        {
            Destroy(gameObject);
        }
    }
}