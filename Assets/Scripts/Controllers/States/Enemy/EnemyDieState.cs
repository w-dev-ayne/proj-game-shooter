using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : MonoBehaviour, IState<EnemyController>
{
    public void Enter(EnemyController ec)
    {
        ec.animatorController.Die();
        ec.GetComponent<Collider>().enabled = false;
        ec.hpBar.transform.parent.gameObject.SetActive(false);
        StartCoroutine(WaitAnimation(ec));
    }

    private IEnumerator WaitAnimation(EnemyController ec)
    {
        yield return new WaitForSeconds(ec.animatorController.dieDurattion);

        Exit(ec);
    }

    public void Exit(EnemyController ec)
    {
        StopAllCoroutines();
        Destroy(ec.gameObject);
    }
}
