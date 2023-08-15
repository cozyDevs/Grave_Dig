//Milo Wilson
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPoint : MonoBehaviour
{
    private HashSet<BaseEnemy> damagedEnemies = new HashSet<BaseEnemy>();
    
    Transform t;
    [SerializeField]
    GameObject targetEnemy;
    [SerializeField]
    List<GameObject> currentMaterial;
    [SerializeField]
    bool attacking;
    [SerializeField]
    int damage;
    [SerializeField] 
    Vector2 up, down, right, left;

    void Start()
    {
        //assigns variables
        t = this.transform;
        attacking = false;
    }

    public void MoveUp()
    {
        //moves the interact box above the player
        t.localPosition = up;
    }

    public void MoveDown()
    {
        //moves the interact box below the player
        t.localPosition = down;
    }

    public void MoveRight()
    {
        //moves the interact box to the right of the player
        t.localPosition = right;
    }

    public void MoveLeft()
    {
        //moves the interact box to the left of the player
        t.localPosition = left;
    }

    public void Attack()
    {
        //makes attacking active
        attacking = true;
    }

    public void StopAttacking()
    {
        //makes attacking inactive
        attacking = false;
        damagedEnemies.Clear();
    }

    public void setDamage(int x)
    {
        damage = x;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //will damage enemies if within 
        if ((other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("EnemyNoCollide")) && attacking)
        {
            //deal damage equal to player admage damage
            BaseEnemy _be;
            if (other.TryGetComponent<BaseEnemy>(out _be)) {
                if (!damagedEnemies.Contains(_be)) {
                    _be.Damage(damage);
                    damagedEnemies.Add(_be);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Mineable"))
        {
            currentMaterial.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mineable"))
        {
            currentMaterial.Remove(collision.gameObject);
        }
    }

    public void DamageBlock(float x)
    {
        
        if(currentMaterial.Count > 0 && currentMaterial[0] != null)
            currentMaterial[0].GetComponent<BlockBreaker>().Picked(x);
    }
}
