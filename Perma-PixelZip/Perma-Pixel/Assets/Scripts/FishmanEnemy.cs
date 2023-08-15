using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishmanEnemy : SkeletonEnemy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 5;
    public override bool CheckForAttack()
    {
        if (base.CheckForAttack()) return true;
        if (targetVisible && swipeTimer <= 0) {
            FireProjectile();
            return true;
        }
        return false;
    }

    protected virtual void FireProjectile() {
        swipeTimer = swipeCooldown;
        GameObject projectileGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        Vector2 dispToTarget = attackTarget.transform.position - transform.position;
        projectile.velocity = dispToTarget.normalized * projectileSpeed;
        // Debug.Log(dispToTarget);
    }
}
