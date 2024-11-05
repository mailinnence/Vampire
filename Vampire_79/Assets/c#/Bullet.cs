using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;


    Rigidbody2D rigid;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage , int per , Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1) // 관통이 -1 (무한) 보다 큰 것에 대해서는 속도 적용
        {
            rigid.velocity = dir * 15f;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1 )
        {
            return;
        }

        per--; // 한번 맞으면 관통력이 줄어들어야 한다.


        // 관통력이 다 떨어지면 오브젝트 풀링을 하기 위해 비활성화 및 속도 0으로 지정
        if(per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }


    }



}
