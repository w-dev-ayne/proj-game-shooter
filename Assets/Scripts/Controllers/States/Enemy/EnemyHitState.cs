using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : MonoBehaviour, IState<EnemyController>
{
    public void Enter(EnemyController ec)
    {
        ec.rb.AddForce(ec.transform.forward * -100);
        ec.animatorController.Hit();
        StartCoroutine(Hit(ec));
    }

    private IEnumerator Hit(EnemyController ec)
    {
        yield return new WaitForSeconds(ec.animatorController.hitDurataion);
        if (ec.attackCondition)
        {
            ec.Attack();
        }
        else
        {
            ec.Move();
        }
    }

    public void Exit(EnemyController ec)
    {
        StopAllCoroutines();
    }
}
