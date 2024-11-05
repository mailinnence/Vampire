using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    [Header("이동 관련 변수")]
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;

    private Rigidbody2D rigid;
    private SpriteRenderer spirter;
    private Animator anim;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spirter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    // void Update()
    // {
    //     // 마찰을 살려 부드럽게 움직임
    //     // inputVec.x = Input.GetAxis("Horizontal");
    //     // inputVec.y = Input.GetAxis("Vertical");

    //     inputVec.x = Input.GetAxisRaw("Horizontal");
    //     inputVec.y = Input.GetAxisRaw("Vertical"); 
    // }


    void FixedUpdate()
    {
        // 이동에는 3가지 방법이 있다.
        // 1.힘을 준다
        // rigid.AddForce(inputVec);

        // 2.속도 제어
        // rigid.velocity = inputVec;

        // 3.위치 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

    }


    void OnMove(InputValue value)
    {
        // normalized 사용되기 때문에 쓰지 않아도 된다.
        inputVec = value.Get<Vector2>();
    }


    void LateUpdate()
    {
        // Speed 에 inputVec.magnitude 이 값을 넣는다는 함수
        anim.SetFloat("Speed" , inputVec.magnitude);

        if(inputVec.x != 0 )
        {
            spirter.flipX = inputVec.x < 0;
        }
    }





/*
`LateUpdate()`는 Unity에서 MonoBehaviour 클래스의 메서드 중 하나로, 프레임마다 호출됩니다. 
`Update()` 메서드와 비슷하게 매 프레임마다 실행되지만, 호출 시점이 다릅니다. 
`LateUpdate()`는 **모든 Update 함수들이 호출된 후에** 실행되기 때문에, 
다른 게임 오브젝트의 Update에서 발생한 변경 사항을 추적하고 처리하는 데 유용합니다.

주요 사용 사례는 다음과 같습니다:

- **카메라 따라가기**: 캐릭터의 움직임이 Update에서 계산되면, LateUpdate에서 카메라를 캐릭터 위치에 맞추는 방식으로 사용될 수 있습니다.
- **객체 상태 동기화**: 여러 객체 간의 상호작용이 Update에서 이루어졌다면, LateUpdate에서 그 결과를 처리하는 것이 적절할 수 있습니다.

유니티에서는 이처럼 **Update 순서가 중요한 작업**에 `LateUpdate()`를 활용합니다.
*/



}
