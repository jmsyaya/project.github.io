using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    // 아래 세 가지 클래스는 유니티 지원 클래스이며,
    // 실제 게임 오브젝트에 들어가는 컴포넌트이다.
    // 리지드 바디 
    Rigidbody2D rigid;
    // 스프라이트 (이미지 파일)
    SpriteRenderer sprite;
    // 애니메이터 (애니메이션 모음)
    Animator anim;

    // 벡터 (간단히 float값 매개변수를 세개를 들고 있는 유니지 지원 메서드)
    // 여기서는 캐릭터의 움직임을 위해 선언
    Vector3 inputVec;
    
    // 캐릭터 스피드를 결정할 변수
    float playerSpeed = 6; 



    // 유니티 기본 지원 메서드로 씬 시작 시 Awake 메서드가 가장 먼저 실행
    private void Awake()
    {
        // GetComponent는 현재 클래스에서 갖고 있는 rigid, sprite, anim 컴포넌트를
        // 찾고, 게임 오브젝트와 연결 시켜준다.
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // 유니티에서 지원하는 메서드로, 매 프레임마다 실행되는 메서드
    private void Update()
    {
        // 플레이어의 움직임을 위해 inputVec의 x, y 값에 수치를 대입해준다.
        // GetAxisRaw("Horizontal") = a, d 혹은 좌 우 화살표 버튼. 유니티 옵션에서 키를 바꿔줄 수 있다.
        // GetAxisRaw("Vertical") = w, s 혹은 위 아래 화살표 버튼. 마찬가지로 바꿀 수 있다.

        // 키 입력을 받으면, x값이 1, 0, -1 / y값이 1, 0, -1 등으로 변화한다.
        // 부드러운 이동을 원한다면 GetAxis("Horizontal / Vertical")을 사용할 수 있다.
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    // 물리 (Physics) 계산으로, 매 프레임마다 실행되는 메서드
    // 현재 Rigidbody2D를 이용해 움직임을 관리하기 때문에 FixedUpdate에서 활용하는 것이 더 적합하다.
    private void FixedUpdate()
    {
        // 대부분 게임 오브젝트는 Vector3인 x, y, z값이 있다.
        // position을 컨트롤하기 위해 transform이라는 컴포넌트에 먼저 접근해야한다.
        
        // Time.deltaTime = 현실 시간에 비례한 타이머 (float)
        // Time.fixedDeltaTime = 현실 시간에 비례한 물리 타이머 (float)

        // 단순하게 fixedUpdate 외에선 deltaTime 사용하면 된다.

        // 캐릭터의 포지션 += Update에서 입력 받은 -1~1값 * Time.fixedDeltaTime * playerSpeed;
        transform.position += inputVec * Time.fixedDeltaTime * playerSpeed;

        // 애니메이터에서 만든 애니메이션
        anim.SetFloat("Walk", inputVec.magnitude);

        // 이미지 파일을 왼쪽 오른쪽으로 뒤집는 기능
        if (inputVec.x != 0)
            sprite.flipX = (inputVec.x < 1) ? true : false;
    }



    // 유니티 지원 메서드.
    // 현재 클래스가 소속되어 있는 오브젝트가 특정 객체에 닿았을 경우 시행된다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 현재 클래스를 넣어 둔 클래스가 "Arrow"라는 객체와 닿은게 아닐 경우
        // 강제 return으로 아무 일도 안생긴다.
        if (!collision.gameObject.CompareTag("Arrow"))
            return;

        // 그 외의 경우엔 위 if문을 빠져 나오니 Debug.Log로 특정 문자열을 출력해준다.
        // 유니티는 메서드가 정상적으로 실행됐는지 확인하기 위해 Debug.Log라는 메서드를 지원함.
        Debug.Log("ㅈㅈ");
    }
}