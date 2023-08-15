using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector2 velocity;
    [SerializeField] private float lifetime = 5;
    private float life = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        life += Time.deltaTime;
        if (life > lifetime) Destroy(this.gameObject);
        // Debug.Log(velocity);
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)velocity * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement _player;
        if (other.TryGetComponent<PlayerMovement>(out _player)) {
            PlayerHealth _ph;
            if (_player.TryGetComponent<PlayerHealth>(out _ph)) {
                _ph.UpdateHealth(-2);
            }
            Destroy(gameObject);
        } else if (other.gameObject.layer == 0) {
            Destroy(gameObject);
        }
    }
}
