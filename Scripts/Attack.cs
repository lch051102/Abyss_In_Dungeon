using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Attack : MonoBehaviour
{
    private float curTime;
    public float coolTime = 0.3f; 
    public int atkDmg = 8;
    public float atkSpeed = 1;
    public bool attacked = false;
    //public Transform target;
    public float attackDelay;
    public UnityEngine.UI.Button button;
    GameObject monster;
    Animator animator;

    private void Start()
    {
        //SetAttackSpeed(1.5f);

        animator = GetComponent<Animator>();
        monster = GameObject.FindGameObjectWithTag("Enemy");
        button.enabled = false;
    }

    private void Update()
    {
        if (attacked == false)
        {
            coolTime = 0.3f;
            button.enabled = false;
        }
        if(coolTime == 0.3f)
        {
            coolTime -= Time.deltaTime;
        }
        if (coolTime <= 0)
        {
            button.enabled = true;
        }
    }

    public void OnClickButton()
    {
        animator.SetTrigger("IsStick");
        if (attacked == true)
        {
            
            monster.GetComponent<MonsterController>().monsterHp -= atkDmg;
            Debug.Log(monster.GetComponent<MonsterController>().monsterHp);
            attacked = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
            Debug.Log("In");
            button.enabled = true;
            if(button.enabled == true)
            {
                
                attacked = true;
            }
            
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            OnClickButton();
            monster.GetComponent<MonsterController>().monsterHp -= 8;
            Debug.Log(monster.GetComponent<MonsterController>().monsterHp);
        }
    }*/

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            monster.monsterHp -= 8;
        }        
    }*/

    void AttackTarget()
    {
        //monster.monsterHp -= 8;
    }

    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    void SetAttackSpeed(float speed)
    {
        //animator.SetFloat("attackSpeed", speed);
        atkSpeed = speed;
    }
}
