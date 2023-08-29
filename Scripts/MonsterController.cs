using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MonsterController : MonoBehaviour
{
    Transform player;
    GameObject attack;
    Rigidbody rb;
    SpriteRenderer spriteRenderer;
    public GameObject slime;
    public int monsterHp = 20;
    public float speed;
    public float distance;
    public LayerMask groundLayer;
    public string enemyName;
    public bool attacked = false;
    public int maxHp;
    public int nowHp = 20;
    public int atkDmg;
    public int atkSpeed;
    public int m_Count = 10;

    
    private void Start()
    {

        player = GameObject.Find("Player").transform;
        attack = GameObject.Find("Attack");
        //attack = GetComponent<Attack>();
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) > distance)
        {
            transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
            DirectionEnemy();

            //Physics2D.Raycast(transform.position, new Vector2(1, 1), 2f, groundLayer);
        }
        else if (Mathf.Abs(transform.position.y - player.position.y) > distance)
        {
            transform.Translate(new Vector2(0, -1) * Time.deltaTime * speed);
            DirectionEnemy2();
        }

        if (m_Count <= 0)
        {
            SceneManager.LoadScene("MainScene");
        }

        if (monsterHp <= 0) // 죽는 애니메이션 나오고 distory 적 사망
        {
            slime.SetActive(false);
            //Destroy(gameObject);
            m_Count -= 1;
            //Destroy(hpBar.gameObject);
        }
    }
    void DirectionEnemy()
    {
        if (transform.position.x - player.position.x < 0)
        {
            transform.localScale = new Vector3(-5, 5, 1);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.localScale = new Vector3(-5, 5, 1);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    void DirectionEnemy2()
    {
        if (transform.position.y - player.position.y < 0)
        {
            transform.localScale = new Vector3(-5, -5, 1);
            transform.eulerAngles = new Vector3(180, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Attack")
        {
            Debug.Log("Zone");
            OnDamaged(collision.transform.position);
        }
    }

    /*private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, int _atkSpeed)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
    }*/

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attack.GetComponent<Attack>().attacked == true)
        {
            if (collision.gameObject.name == "Attack")
            {
                monsterHp -= attack.GetComponent<Attack>().atkDmg;
                Debug.Log(monsterHp);
                attack.GetComponent<Attack>().attacked = false;
                if (monsterHp <= 0) // 죽는 애니메이션 나오고 distory 적 사망
                {
                    Destroy(gameObject);
                    //Destroy(hpBar.gameObject);
                }
            }
        }
    }*/

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            if (attack.GetComponent<Attack>().attacked == true)
            {
                monsterHp -= attack.GetComponent<Attack>().atkDmg;
                Debug.Log(monsterHp);
                //attack.GetComponent<Attack>().attacked = false;
                if (monsterHp <= 0) // 죽는 애니메이션 나오고 distory 적 사망
                {
                    Destroy(gameObject);
                    //Destroy(hpBar.gameObject);
                }
            }
            //attack.GetComponent<Attack>().OnClickButton();
            //monsterHp -= 8;
            //Debug.Log(monsterHp);
            //attack.attacked = false;
            /*if (monsterHp <= 0) // 죽는 애니메이션 나오고 distory 적 사망
            {
                Destroy(gameObject);
                m_Count -= 1;
                //Destroy(hpBar.gameObject);
            }
            /*if (attack.attacked)
            {
                nowHp -= attack.atkDmg;
                Debug.Log(nowHp);
                attack.attacked = false;
                if (nowHp <= 0) // 적 사망
                {
                    Destroy(gameObject);
                    //Destroy(hpBar.gameObject);
                }
            }
        }
    }*/

    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 6;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirc, 1) * 7, (ForceMode)ForceMode2D.Impulse);
    }
}
