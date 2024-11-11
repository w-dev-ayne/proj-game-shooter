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
        StartCoroutine(UpdateDestination());
        ec.agent.isStopped = false;
    }

    private IEnumerator UpdateDestination()
    {
        WaitForSeconds wait = new WaitForSeconds(1.0f);

        while (true)
        {
            ec.agent.SetDestination(ec.cc.transform.position);
            yield return wait;
        }
    }
    
    void FixedUpdate()
    {
        if (isMoving && !ec.attackCondition)
        {
            ec.transform.LookAt(ec.cc.transform.position);
            // 목표 위치로 이동
            
            //ec.transform.position = Vector3.MoveTowards(transform.position, ec.cc.transform.position, Time.deltaTime);
        }
        else if (isMoving && ec.attackCondition)
        {
            ec.Attack();
        }
    }

    public void Exit(EnemyController ec)
    {
        ec.agent.isStopped = true;
        StopAllCoroutines();
        isMoving = false;
        ec.animatorController.FinishMove();
    }
}
