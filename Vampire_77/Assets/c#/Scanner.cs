using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{

    public float scanRange;         // 범위
    public LayerMask targerLayer;   // 타겟 레이어
    public RaycastHit2D[] targets;  // 타겟은 다수이다. >> 배열
    public Transform nearestTarget; // 가장 가까운 목표의 위치



    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position , scanRange , Vector2.zero , 0 , targerLayer);
        // 어딘간에 쏴서 보여주는 게 아니므로 캐스트 방향과 길이는 0 이다.
        /*
        1.캐스팅 시작 위치
        2.원의 반지름
        3.캐스팅 방향
        4.캐스팅 길이
        5.대상 레이어
        */

        // 완성된 함수를 통해 지속적으로 가장 가까운 목표 변수를 업데이트
        nearestTarget = GetNearest();
    }


    Transform GetNearest()
    {
        Transform result = null;

        float diff = 100;
        
        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;

            // Distance(A, B) : 벡터 A와 B의 거리를 계산해주는 함수
            float curDiff = Vector3.Distance(myPos , targetPos);

            if (curDiff < diff)
            {
                // 반복문을 돌며 가져운 거리가 저장된 거리보다 작으면 교체
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }

}
