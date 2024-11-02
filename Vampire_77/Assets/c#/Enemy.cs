using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("속도 , 추적 , 생존 변수")]
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    bool isLive;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spirter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spirter = GetComponent<SpriteRenderer>();
    }

    
    void Start()
    {
        // isLive = true;
    }





    // 물리적인 연산이 아닌 이동은 fixupdate 를 사용한다.
    void FixedUpdate()
    {
        if(!isLive)     // 살아있지 않으면 아무것도 반환하지 않는다.
        {
            return;
        }


        // 방향
        Vector2 dirVec = target.position - rigid.position;
        
        // 속도
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        // 속도 적용
        rigid.MovePosition(rigid.position + nextVec); 
        rigid.velocity = Vector2.zero; // 물리적인 속도 제거
    }


    void LateUpdate()
    {
        if(!isLive)
        {
            return;
        }

        spirter.flipX = target.position.x < rigid.position.x;
    }



    // 활성화 될때 실행되는 함수
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;

    }


    // Spawner 에서 만든 변수를 가져오는 함수
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) 
        { 
            return; 
        }

        health -= collision.GetComponent<Bullet>().damage;

        if(health > 0 )
        {

        }
        else 
        {
            Dead();
        }


    }

    void Dead()
    {
        gameObject.SetActive(false);
    }


}
