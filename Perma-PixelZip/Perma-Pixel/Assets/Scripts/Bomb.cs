using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float fuseTimer;
    private bool exploding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (exploding)
        {
            Destroy(this.gameObject);
        }
        fuseTimer -= Time.deltaTime; 
        if (fuseTimer <= 0)
        {
            exploding = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (exploding && collision.gameObject.CompareTag("Mineable"))
        {
            collision.GetComponent<BlockBreaker>().Picked(20);
        }
    }

}
