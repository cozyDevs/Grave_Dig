using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonEnemy : BaseEnemy
{
    [SerializeField] private GameObject swipeAttack;
    [SerializeField] protected float swipeCooldown = 2.5f;
    [SerializeField] private float swipeSpawnDist = 1.5f;
    protected float swipeTimer = 0;
    [SerializeField] private float attackRange = 1;

    protected override void Update()
    {
        base.Update();
        if (swipeTimer > 0) {
            swipeTimer = Mathf.MoveTowards(swipeTimer, 0, Time.deltaTime);
        }
    }

    public override bool CheckForAttack()
    {
        Vector2 dispToTarget = attackTarget.transform.position - transform.position;
        if (swipeTimer <= 0 && dispToTarget.magnitude < attackRange) {
            PerformAttack();
            return true;
        }
        return false;
    }

    private void PerformAttack()
    {
        Vector2 dispToTarget = attackTarget.transform.position - transform.position;
        Vector2 attackPos = (Vector2)transform.position + dispToTarget.normalized * swipeSpawnDist;
        Instantiate(swipeAttack, attackPos, Quaternion.Euler(0,0,Mathf.Atan2(dispToTarget.y, dispToTarget.x)*180/Mathf.PI-90), transform);
        swipeTimer = swipeCooldown;
    }
}
