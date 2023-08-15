using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoom : MonoBehaviour
{
    public bool opened = false;
    // Start is called before the first frame update
    public void Open()
    {
        if (!opened)
        {
            opened = true;
            FloorTexture[] texts = gameObject.GetComponentsInChildren<FloorTexture>();
            foreach (FloorTexture s in texts)
            {
                s.Open();
            }
            Collider2D hit = Physics2D.OverlapPoint((Vector2)transform.position + 3 * Vector2.right);
            if (hit != null)
            {
                Debug.Log(hit.gameObject.tag);
                if (hit.gameObject.tag == "EnemyRoom" && hit.gameObject != gameObject)
                {
                    hit.gameObject.GetComponent<MonsterRoom>().Open();
                }
                else if (hit.gameObject.tag == "EndRoom")
                {
                    Debug.Log(hit.gameObject.tag);
                    hit.gameObject.GetComponent<EndRoom>().Open();
                }
            }
            Collider2D hit2 = Physics2D.OverlapPoint((Vector2)transform.position + 3 * Vector2.left);
            if (hit2 != null)
            {
                Debug.Log(hit.gameObject.tag);
                if (hit2.gameObject.tag == "EnemyRoom" && hit2.gameObject != gameObject)
                {
                    hit2.gameObject.GetComponent<MonsterRoom>().Open();
                }
                else if (hit2.gameObject.tag == "EndRoom")
                {
                    Debug.Log("!");
                    hit2.gameObject.GetComponent<EndRoom>().Open();
                }
            }
            Collider2D hit3 = Physics2D.OverlapPoint((Vector2)transform.position + 3 * Vector2.up);
            if (hit3 != null)
            {
                Debug.Log(hit.gameObject.tag);
                if (hit3.gameObject.tag == "EnemyRoom" && hit3.gameObject != gameObject)
                {
                    hit3.gameObject.GetComponent<MonsterRoom>().Open();
                }
                else if (hit3.gameObject.tag == "EndRoom")
                {
                    Debug.Log("!");
                    hit3.gameObject.GetComponent<EndRoom>().Open();
                }
            }
            Collider2D hit4 = Physics2D.OverlapPoint((Vector2)transform.position + 3 * Vector2.down);
            if (hit4 != null)
            {
                Debug.Log(hit.gameObject.tag);
                if (hit4.gameObject.tag == "EnemyRoom" && hit4.gameObject != gameObject)
                {
                    hit4.gameObject.GetComponent<MonsterRoom>().Open();
                }
                else if (hit4.gameObject.tag == "EndRoom")
                {
                    Debug.Log("!");
                    hit4.gameObject.GetComponent<EndRoom>().Open();
                }
            }
        }
    }
}
