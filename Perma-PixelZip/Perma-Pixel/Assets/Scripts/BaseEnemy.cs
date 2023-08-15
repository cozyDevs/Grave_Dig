using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{

    [SerializeField] private int maxHealth = 3;
    private int health;

    public GameObject attackTarget;
    [SerializeField] private LayerMask targetRaycastLayers;
    private Vector2 moveTarget;

    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private bool useVelocity = false;
    [SerializeField]
    private float acceleration = 1f;
    [SerializeField] protected float detectionRadius = 10;
    private Vector2 m_velocity;

    private Rigidbody2D _rb;
    private Animator _an;

    private bool idle = true;
    private float idleTime = 0f;
    [SerializeField] private float targetIdleTime = 4f;
    [SerializeField] private float maxWanderDist = 10;
    private bool wandering = true;

    protected bool targetVisible = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        TryGetComponent<Animator>(out _an);
        moveTarget = transform.position;
        health = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_an) {
            _an.SetBool("idle", idle);
        }
    }

    public void Damage(int amt) {
        health -= amt;
        Debug.Log(health);
        if (health<=0) {
            Kill();
        }
    }

    public void Kill() {
        Destroy(this.gameObject);
    }

    bool CheckNewMoveTarget() {
        if (!attackTarget) return false;
        Vector2 distToTarget = attackTarget.transform.position - transform.position;
        if (distToTarget.magnitude > detectionRadius) return false;
        RaycastHit2D rh = Physics2D.Raycast(transform.position, distToTarget, distToTarget.magnitude, targetRaycastLayers);
        if (!rh.transform) {
            moveTarget = attackTarget.transform.position;
            idle = false;
            wandering = false;
            return true;
        }
        return false;
    }

    void ChooseRandomTarget() {
        Vector2 dir = Random.insideUnitCircle;
        float dist = Random.Range(0,maxWanderDist);
        moveTarget = (Vector3)dir * dist + transform.position;
    }

    void FixedUpdate() {
        bool newTargetFound = CheckNewMoveTarget();
        targetVisible = newTargetFound;

        Vector2 dispToMoveTarget = moveTarget - (Vector2)transform.position;
        Vector2 dirToMove = dispToMoveTarget.normalized;
        
        if (!idle && !newTargetFound && dispToMoveTarget.magnitude < .2f) {
            idle = true;
            idleTime = 0f;
        }
        
            if (useVelocity) {
                Vector2 dv = acceleration * dirToMove * Time.fixedDeltaTime;
                _rb.velocity += dv;
                _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, moveSpeed);
            } else if (!idle) {
                _rb.velocity = dirToMove * moveSpeed;
            } else {
                _rb.velocity = Vector2.zero;
            }

            if (!idle && _an) {
                _an.SetFloat("dirX", dirToMove.x);
                _an.SetFloat("dirY", dirToMove.y);
            }

            // transform.position += (Vector3)_rb.velocity * Time.fixedDeltaTime;

            if (!idle && CheckForAttack()) {
                _an.SetTrigger("attack");
            }

            if (idle) {
                idleTime += Time.fixedDeltaTime;
                if (idleTime >= targetIdleTime) {
                    ChooseRandomTarget();
                    idle = false;
                    wandering = true;
                }
            }
    }

    public abstract bool CheckForAttack();

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!idle && wandering) {
            idle = true;
            idleTime = 0;
        }
    }
}
