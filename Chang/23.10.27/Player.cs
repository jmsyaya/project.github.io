using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using DG.Tweening;


public class Player : MonoBehaviour
{
    // �Ʒ� �� ���� Ŭ������ ����Ƽ ���� Ŭ�����̸�,
    // ���� ���� ������Ʈ�� ���� ������Ʈ�̴�.
    // ������ �ٵ� 
    Rigidbody2D rigid;
    // ��������Ʈ (�̹��� ����)
    SpriteRenderer sprite;
    // �ִϸ����� (�ִϸ��̼� ����)
    Animator anim;

    // ���� (������ float�� �Ű������� ������ ��� �ִ� ������ ���� �޼���)
    // ���⼭�� ĳ������ �������� ���� ����
    public Vector3 inputVec;
    
    // ĳ���� ���ǵ带 ������ ����
    float playerSpeed = 6;

    // �������¸� �����ϴ� ����
    bool Jump = false;

    bool isSlide = false;


    // ����Ƽ �⺻ ���� �޼���� �� ���� �� Awake �޼��尡 ���� ���� ����
    private void Awake()
    {
        // GetComponent�� ���� Ŭ�������� ���� �ִ� rigid, sprite, anim ������Ʈ��
        // ã��, ���� ������Ʈ�� ���� �����ش�.
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // ����Ƽ���� �����ϴ� �޼����, �� �����Ӹ��� ����Ǵ� �޼���
    private void Update()
    {
        // �÷��̾��� �������� ���� inputVec�� x, y ���� ��ġ�� �������ش�.
        // GetAxisRaw("Horizontal") = a, d Ȥ�� �� �� ȭ��ǥ ��ư. ����Ƽ �ɼǿ��� Ű�� �ٲ��� �� �ִ�.
        // GetAxisRaw("Vertical") = w, s Ȥ�� �� �Ʒ� ȭ��ǥ ��ư. ���������� �ٲ� �� �ִ�.

        // Ű �Է��� ������, x���� 1, 0, -1 / y���� 1, 0, -1 ������ ��ȭ�Ѵ�.
        // �ε巯�� �̵��� ���Ѵٸ� GetAxis("Horizontal / Vertical")�� ����� �� �ִ�.
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
        SlideButton();
        anim.SetBool("Slide", this.isSlide);
    }

    public void SlideButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isSlide = true;
        else if (Input.GetKeyUp(KeyCode.Space))
            isSlide = false;
    }


    // ���� (Physics) �������, �� �����Ӹ��� ����Ǵ� �޼���
    // ���� Rigidbody2D�� �̿��� �������� �����ϱ� ������ FixedUpdate���� Ȱ���ϴ� ���� �� �����ϴ�.
    private void FixedUpdate()
    {
        // ��κ� ���� ������Ʈ�� Vector3�� x, y, z���� �ִ�.
        // position�� ��Ʈ���ϱ� ���� transform�̶�� ������Ʈ�� ���� �����ؾ��Ѵ�.
        
        // Time.deltaTime = ���� �ð��� ����� Ÿ�̸� (float)
        // Time.fixedDeltaTime = ���� �ð��� ����� ���� Ÿ�̸� (float)

        // �ܼ��ϰ� fixedUpdate �ܿ��� deltaTime ����ϸ� �ȴ�.

        // ĳ������ ������ += Update���� �Է� ���� -1~1�� * Time.fixedDeltaTime * playerSpeed;
        transform.position += inputVec * Time.fixedDeltaTime * playerSpeed;

        // �ִϸ����Ϳ��� ���� �ִϸ��̼�
        anim.SetFloat("Walk", inputVec.magnitude);

        // �̹��� ������ ���� ���������� ������ ���
        if (inputVec.x != 0)
            sprite.flipX = (inputVec.x < 1) ? true : false;
    }


    // ����Ƽ ���� �޼���.
    // ���� Ŭ������ �ҼӵǾ� �ִ� ������Ʈ�� Ư�� ��ü�� ����� ��� ����ȴ�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� Ŭ������ �־� �� Ŭ������ "Arrow"��� ��ü�� ������ �ƴ� ���
        // ���� return���� �ƹ� �ϵ� �Ȼ����.
        if (!collision.gameObject.CompareTag("Arrow"))
            return;

        // �� ���� ��쿣 �� if���� ���� ������ Debug.Log�� Ư�� ���ڿ��� ������ش�.
        // ����Ƽ�� �޼��尡 ���������� ����ƴ��� Ȯ���ϱ� ���� Debug.Log��� �޼��带 ������.
        Debug.Log("����");
    }
}