using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreaker : MonoBehaviour
{


    public Sprite[] sprites;
    public float hitPoints;
    private bool broken = false;

    //Function that removes the sprite if the game Object is hit
    public void Picked(float dam)
	{
        GetComponent<SpriteRenderer>().flipX = true;


        hitPoints -= dam;
        if(hitPoints <= 0 && !broken)
		{
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<ParticleSystem>().Play();
            Collider2D hit = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.right);
            if (hit != null && hit.gameObject.tag == "EnemyRoom")
            {
                hit.gameObject.GetComponent<MonsterRoom>().Open();
            }
            else if (hit != null && hit.gameObject.tag == "EndRoom")
            {
                hit.gameObject.GetComponent<EndRoom>().Open();
            }
            Collider2D hit2 = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.left);
            if (hit2 != null && hit2.gameObject.tag == "EnemyRoom")
            {
                hit2.gameObject.GetComponent<MonsterRoom>().Open();
            }
            else if (hit2 != null && hit2.gameObject.tag == "EndRoom")
            {
                hit2.gameObject.GetComponent<EndRoom>().Open();
            }
            Collider2D hit3 = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.up);
            if (hit3 != null && hit3.gameObject.tag == "EnemyRoom")
            {
                hit3.gameObject.GetComponent<MonsterRoom>().Open();
            }
            else if (hit3 != null && hit3.gameObject.tag == "EndRoom")
            {
                hit3.gameObject.GetComponent<EndRoom>().Open();
            }
            Collider2D hit4 = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.down);
            if (hit4 != null && hit4.gameObject.tag == "EnemyRoom")
            {
                hit4.gameObject.GetComponent<MonsterRoom>().Open();
            }
            else if (hit4 != null && hit4.gameObject.tag == "EndRoom")
            {
                hit4.gameObject.GetComponent<EndRoom>().Open();
            }
            transform.GetChild(0).GetComponent<FloorTexture>().Open();
            broken = true;
        }
        
	}
}
