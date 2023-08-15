using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float lifetime = 1;

    private float life;

    private bool alreadyHurt = false;
    
    [SerializeField] private float hurtStart = .2f;
    [SerializeField] private float hurtEnd = .4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        life += Time.deltaTime;
        if (life > lifetime) Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerMovement player;
        if (other.TryGetComponent<PlayerMovement>(out player)
                && !alreadyHurt
                && life >= hurtStart && life <= hurtEnd) {
            alreadyHurt = true;
            // TODO Player damage logic here
            PlayerHealth _ph;
            if (player.TryGetComponent<PlayerHealth>(out _ph)) {
                _ph.UpdateHealth(-4);
            }
        }
    }
}
