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
    Collider2D coll;
    Animator anim;
    SpriteRenderer spirter;

    WaitForFixedUpdate wait;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spirter = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();
        
    }

    
    void Start()
    {
        // isLive = true;
    }





    // 물리적인 연산이 아닌 이동은 fixupdate 를 사용한다.
    void FixedUpdate()
    {
        
        if(!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))     // 살아있지 않으면 아무것도 반환하지 않는다.
        {
            //  anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")
            // 첫 번째는 어느 레이어인지를 구별함
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
       
        // 사망했다가 다시 부를때 다시 활성화 되어야 한다.
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spirter.sortingOrder = 2;
        anim.SetBool("Dead" , false);

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
        // 사망 로직이 연달아 실행되는 것을 방지하기 위해서 조건추가
        if (!collision.CompareTag("Bullet") || !isLive) 
        { 
            return; 
        }

        health -= collision.GetComponent<Bullet>().damage;
        

        // 두 가지 방법이 있다.
        StartCoroutine(KnockBack());
        // StartCoroutine("KnockBack");


        if(health > 0 )
        {
            // 애니메이션 - 공격을 맞았을때 
            anim.SetTrigger("Hit");
        }
        else 
        {
            // 사망시 비활성화
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spirter.sortingOrder = 1;
            anim.SetBool("Dead" , true);
            // Dead();

            // 킬 수 와 경험치 
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }


    }


    // 코루틴 Coroutine : 생명 주기와 비동기처럼 실행되는 함수
    IEnumerator  KnockBack()
    {
        // yield return null;                   // 1프레임 쉬기
        // yield return new waitForSceonds(2f); // 2초간 쉬기 단 반복해서 쓸 경우 최척화에 좋지 않으므로 변수로 설정하는 것이 좋다
        yield return wait;                   // 하나의 물리 프레임을 딜레이

        // 한 프레임 뒤에 넉백 시킴
        Vector3 playerPos = GameManager.instance.player.transform.position;
        
        // 플레이어 방향에서 뒷 방향으로 방향 설정
        // 플레이어 기준의 반대 방향 : 현재 위치 - 플레이어 위치
        Vector3 dirVec = transform.position - playerPos;
        
        // 힘 가하기
        // 크기까지 같이 들어갔으므로 정규화 (normalized)
        // 순간적인 힘이므로 ForceMode2D () >> rigidbody 가 2d 임으로 ForceMode2D
        rigid.AddForce(dirVec.normalized * 3 , ForceMode2D.Impulse); 

    }


    void Dead()
    {
        gameObject.SetActive(false);
    }






}
