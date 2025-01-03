using System.Collections;
using UnityEngine;

public class EnemySpawnState : MonoBehaviour, IState<EnemyController>
{
    public void Enter(EnemyController ec)
    {
        ec.animatorController.Spawn();
        ec.spawnVfx.gameObject.SetActive(true);
        StartCoroutine(WaitForAnimation(ec));
    }

    private IEnumerator WaitForAnimation(EnemyController ec)
    {
        yield return new WaitForSeconds(ec.spawnVfx.GetComponent<ParticleSystem>().main.duration);
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
        ec.spawnVfx.gameObject.SetActive(false);
        StopAllCoroutines();
    }
}