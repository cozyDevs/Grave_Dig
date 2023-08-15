using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : BaseEnemy
{
    [SerializeField] private GameObject biteAttack;
    [SerializeField] private float biteCooldown = 2.5f;
    private float biteTimer = 0;
    [SerializeField] private float attackRange = 1;

    protected override void Update()
    {
        base.Update();
        if (biteTimer > 0) {
            biteTimer = Mathf.MoveTowards(biteTimer, 0, Time.deltaTime);
        }
    }

    public override bool CheckForAttack()
    {
        Vector2 dispToTarget = attackTarget.transform.position - transform.position;
        if (biteTimer <= 0 && dispToTarget.magnitude < attackRange) {
            PerformAttack();
            return true;
        }
        return false;
    }

    private void PerformAttack()
    {
        Vector2 dispToTarget = attackTarget.transform.position - transform.position;
        Vector2 attackPos = transform.position;
        Instantiate(biteAttack, attackPos, Quaternion.identity);
        biteTimer = biteCooldown;
        // Do bite attack damage logic here
        PlayerHealth _ph;
        if (attackTarget.TryGetComponent<PlayerHealth>(out _ph)) {
            _ph.UpdateHealth(-2);
        }
    }
}
