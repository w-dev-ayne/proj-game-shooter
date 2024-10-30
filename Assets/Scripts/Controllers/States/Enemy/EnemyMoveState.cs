using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController ec;
    private Vector3 targetDirection;
    private bool isMoving = false;
    
    public void Enter(EnemyController ec)
    {
        this.ec = ec;
        ec.animatorController.StartMove();
        isMoving = true;
        //StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        WaitForEndOfFrame oneFrame = new WaitForEndOfFrame();
        
        while (!ec.attackCondition)
        {
            ec.transform.LookAt(ec.cc.transform.position);
            // 목표 위치로 이동
            ec.transform.position = Vector3.MoveTowards(transform.position, ec.cc.transform.position, Time.deltaTime);
            yield return oneFrame;
        }
        ec.Attack();
    }

    void FixedUpdate()
    {
        if (isMoving && !ec.attackCondition)
        {
            ec.transform.LookAt(ec.cc.transform.position);
            // 목표 위치로 이동
            ec.transform.position = Vector3.MoveTowards(transform.position, ec.cc.transform.position, Time.deltaTime);
        }
        else if (isMoving && ec.attackCondition)
        {
            ec.Attack();
        }
    }

    public void Exit(EnemyController ec)
    {
        StopAllCoroutines();
        isMoving = false;
        ec.animatorController.FinishMove();
    }
}
