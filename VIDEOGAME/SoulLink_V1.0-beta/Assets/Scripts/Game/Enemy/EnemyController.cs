/*
Code used to determine whether enemies are in room and make 
them act accordingly.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
    {
        Idle,
        Follow,
        Die,
        Attack
    };

public class EnemyController : MonoBehaviour
{
    GameObject player;
    // public EnemyState currState = EnemyState.Follow;
    // public float range;
    // public float speed;
    // public float coolDown;
    // public float attackRange;
    // private bool coolDownAttack = false;
    public bool notInRoom;
    public Behaviour enemyType;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (notInRoom)
        {
            enemyType.enabled = false;
            return;
        }
        enemyType.enabled = true;

        // switch (currState)
        // {
        //     case(EnemyState.Idle):
        //     break;

        //     case(EnemyState.Follow):
        //         Follow();
        //     break;

        //     case(EnemyState.Die):
                
        //     break;

        //     case(EnemyState.Attack):
        //         Attack();
        //     break;
        // }

        // if (IsPlayerInRange(range) && currState != EnemyState.Die)
        // {
        //     currState = EnemyState.Follow;
        // } else if (IsPlayerInRange(range) == false && currState != EnemyState.Die)
        // {
        //     currState = EnemyState.Idle;
        // }
        // if(Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        // {
        //     currState = EnemyState.Attack;
        // }
    }

    // private bool IsPlayerInRange(float range)
    // {
    //     return Vector3.Distance(transform.position, player.transform.position) <= range;
    // }

    // void Follow()
    // {
    //     transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
    // }

    // void Attack()
    // {
    //     if (!coolDownAttack)
    //     {
    //         GameController.DamagePlayer(1);
    //         StartCoroutine(CoolDown());
    //     }
    // }

    // private IEnumerator CoolDown()
    // {
    //     coolDownAttack = true;
    //     yield return new WaitForSeconds(coolDown);
    //     coolDownAttack = false;
    // }


    // public void Death(){
    //     Destroy(gameObject);
    // }
}
