using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MonsterRoom : MonoBehaviour
{
    public bool opened = false;
    public GameObject[] monsterPrefabs;
    private GameObject[] monsterSpawns;
    private GameObject[] monsters;
    public Material nonLit;
    private int amount;
    // Start is called before the first frame update
    void Start()
    {
        amount = Random.Range(2, 6);
        monsterSpawns = new GameObject[amount];
        monsters = new GameObject[amount];
        for (int i = 0; i < amount; i++) {
            monsterSpawns[i] = monsterPrefabs[(int)Random.Range(0,monsterPrefabs.Length)];
        }
        Debug.Log(monsterSpawns);
    }

    public void Open()
    {
        if(!opened)
        {
            opened = true;
            FloorTexture[] texts = gameObject.GetComponentsInChildren<FloorTexture>();
            foreach(FloorTexture s in texts)
            {
                s.Open();
            }
            Collider2D hit = Physics2D.OverlapPoint((Vector2)transform.position + 3 * Vector2.right);
            if (hit != null)
            {
                if(hit.gameObject.tag == "EnemyRoom" && hit.gameObject != gameObject)
                {
                    hit.gameObject.GetComponent<MonsterRoom>().Open();
                }
                else if (hit.gameObject.tag == "EndRoom")
                {
                    hit.gameObject.GetComponent<EndRoom>().Open();
                }
            }
            Collider2D hit2 = Physics2D.OverlapPoint((Vector2)transform.position + 3 * Vector2.left);
            if (hit2 != null)
            {
                if (hit2.gameObject.tag == "EnemyRoom" && hit2.gameObject != gameObject)
                {
                    hit2.gameObject.GetComponent<MonsterRoom>().Open();
                }
                else if (hit2.gameObject.tag == "EndRoom")
                {
                    hit2.gameObject.GetComponent<EndRoom>().Open();
                }
            }
            Collider2D hit3 = Physics2D.OverlapPoint((Vector2)transform.position + 3 * Vector2.up);
            if (hit3 != null)
            {
                if (hit3.gameObject.tag == "EnemyRoom" && hit3.gameObject != gameObject)
                {
                    hit3.gameObject.GetComponent<MonsterRoom>().Open();
                }
                else if (hit3.gameObject.tag == "EndRoom")
                {
                    hit3.gameObject.GetComponent<EndRoom>().Open();
                }
            }
            Collider2D hit4 = Physics2D.OverlapPoint((Vector2)transform.position + 3 * Vector2.down);
            if (hit4 != null)
            {
                if (hit4.gameObject.tag == "EnemyRoom" && hit4.gameObject != gameObject)
                {
                    hit4.gameObject.GetComponent<MonsterRoom>().Open();
                }
                else if (hit4.gameObject.tag == "EndRoom")
                {
                    hit4.gameObject.GetComponent<EndRoom>().Open();
                }
            }
            SpawnMonsters();
        }

    }

    public void Reset()
    {
        if (!opened) return;
        for (int i = 0; i < amount; i++) {
            if (monsters[i]) Destroy(monsters[i]);
        }
        SpawnMonsters();
    }

    public void SpawnMonsters()
    {
        for(int i = 0; i < amount; i++)
        {
            monsters[i] = Instantiate(monsterSpawns[i], new Vector2(transform.position.x + Random.Range(-1.3f, 1.3f), transform.position.y + Random.Range(-1.3f, 1.3f)), transform.rotation, transform);
            monsters[i].GetComponent<BaseEnemy>().attackTarget = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
