using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class PlayerMove : MonoBehaviour
{
    Transform player;
    Rigidbody2D rigid; //�����̵��� ���� ���� ���� 
    public float maxHp = 100;
    public float nowHp = 100;
    public bool aKey;
    public bool dKey;
    //public Image nowHpbar;
    public float maxSpeed;
    public bl_Joystick js; // ���̽�ƽ ������Ʈ�� ������ ������� �����ϱ�.
    public float speed; // ���̽�ƽ�� ���� ������ ������Ʈ�� �ӵ�.
    //public float originPos;
    private Animator animator;
    GameObject attack;
    //private MonsterController monster;

    private void Start()
    {
        animator = GetComponent<Animator>();
        attack = GameObject.Find("Attack");
        //monster = GetComponent<MonsterController>();
        //nowHpbar = GetComponent<Image>();
    }
    private void Awake()
    {

        rigid = GetComponent<Rigidbody2D>(); //���� �ʱ�ȭ 


    }

    // Update is called once per frame
    void Update()

    {
        //nowHpbar.fillAmount = nowHp / maxHp;


        // ��ƽ�� �����ִ� ������ �������ش�.
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0);

        // Vector�� ������ ���������� ũ�⸦ 1�� ���δ�. ���̰� ����ȭ ���� ������ 0���� ����.
        dir.Normalize();

        // ������Ʈ�� ��ġ�� dir �������� �̵���Ų��.
        transform.position += dir * speed * Time.deltaTime;

        if(js.Horizontal < 1)
        {
            transform.localScale = new Vector3(-3, 3, 1);
            animator.SetBool("IsWalk", true);
        }
        else if(js.Vertical < 1)
        {
            animator.SetBool("IsWalk", true);
        }
        else
            animator.SetBool("IsWalk", false);
        if (js.Horizontal > -1)
        {
            transform.localScale = new Vector3(3, 3, 1);
            animator.SetBool("IsWalk", true);
        }
        else if(js.Vertical > -1)
        {
            animator.SetBool("IsWalk", true);
        }
        else
            animator.SetBool("IsWalk", false);

        float moveY = 0f;

        float moveX = 0f;

        if (Input.GetKey(KeyCode.W))
        {

            moveY += maxSpeed;
            animator.SetBool("IsWalk", true);

        }
        else if (Input.GetKey(KeyCode.S))

        {

            moveY -= maxSpeed;
            animator.SetBool("IsWalk", true);

        }
        else
        {
            animator.SetBool("IsWalk", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            aKey = true;
            dKey = false;
            if(aKey == true && dKey == false)
            {
                transform.localScale = new Vector3(-3, 3, 1);
            }
            moveX -= maxSpeed;
            animator.SetBool("IsWalk", true);

        }

        else if (Input.GetKey(KeyCode.D))
        {
            dKey = true;
            aKey = false;
            if(dKey == true && aKey == false)
            {
                transform.localScale = new Vector3(3, 3, 1);
            }
            moveX += maxSpeed;
            animator.SetBool("IsWalk", true);
        }
        
        else
        {
            animator.SetBool("IsWalk", false);
        }


        transform.Translate(new Vector3(moveX, moveY, 0f) * 0.01f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Portal")
        {
            SceneManager.LoadScene("DungeonScene");
        }
        if (collision.tag == "TransparentWall")
        {
            transform.position = new Vector3(0, -2, -9);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Top")
        {
            transform.position = new Vector3(0, 0, -9);
        }
    }
}