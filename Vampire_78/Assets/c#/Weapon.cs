using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;

    player player;

    void Awake()
    {
        player = GetComponentInParent<player>();
    }


    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:
                timer += Time.deltaTime;
                if( timer > speed )
                {
                    timer = 0f;
                    Fire();
                }
                break;

        }
    
    // Test Code
    if (Input.GetButtonDown("Jump"))
        LevelUp(10 , 1);
    }


    
    public void LevelUp(float damage , int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
        {
            Batch();
        }
    
    }




    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;

            default:
                speed = 0.3f;
                break;

        }
    }

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            // 부모를 풀 메니저가 아닌 플레이어로 바꾸어야 한다.
            Transform bullet;
            if(index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            } 
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            
            bullet.localPosition = Vector3.zero; 
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count; 
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f , Space.World);

            bullet.GetComponent<Bullet>().Init(damage , -1 , Vector3.zero); // 관통은 숫자가 필요없다. -1로 무한을 정의하자. -1 is Infinity Per
        }
    }



    void Fire()
    {
        if (!player.scanner.nearestTarget)
        {
            return;
        }

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position; // 크기가 포함된 방향 :  목표 위치 - 나의 위치

        dir = dir.normalized; // 현재 벡터의 방향은 유지하고 크기를 1로 변환된 속성 >> 큰 연산을 줄여줌

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up , dir);
       
        bullet.GetComponent<Bullet>().Init(damage , count , dir); // 관통은 숫자가 필요없다. -1로 무한을 정의하자. -1 is Infinity Per
    
    }
}
               