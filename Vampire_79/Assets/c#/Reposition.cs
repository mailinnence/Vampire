using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{

    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }






    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        


        // 방향도 미리 계산
        Vector3 playerDir = GameManager.instance.player.inputVec;

        // 플레이어와 오브젝트의 x, y 차이를 계산
        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;


        // 태그별 동작을 함수로 분리하여 가독성과 유지보수성 향상
        switch (transform.tag)
        {
            case "Ground":
                RepositionGround(diffX, diffY, dirX, dirY);
                break;

            case "Enemy":
                RepositionEnemy(playerDir);  // 현재 빈 함수지만 추후 추가 가능
                break;
        }
    }

    void RepositionGround(float diffX, float diffY, float dirX, float dirY)
    {
        // 중복되는 값이 없으므로 Translate를 최소한으로 호출
        if (diffX > diffY)
        {
            transform.Translate(Vector3.right * dirX * 60);
        }
        else if (diffX < diffY)
        {
            transform.Translate(Vector3.up * dirY * 60);
        }
        else 
        {
            transform.Translate(dirX * 60, dirY * 60, 0);
        }
    }

    void RepositionEnemy(Vector3 playerDir)
    {
        if(coll.enabled) 
        {
            transform.Translate(playerDir * 30 + new Vector3(Random.Range(-3f, 3f) , Random.Range(-3f , 3f) , 0f));
        } 
    }
}



/*
// 강의 코드
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;



        switch (transform.tag) 
        {
            case "Ground":
                if(diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 60);
                }
                else if(diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 60);
                }
                else {
                    transform.Translate(dirX * 60, dirY * 60, 0);
                }
                break;
            case "Enemy":
                break;
        }

    }
}


*/