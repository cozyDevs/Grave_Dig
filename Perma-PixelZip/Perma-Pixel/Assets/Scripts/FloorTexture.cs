using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FloorTexture : MonoBehaviour
{
    public Sprite right;
    public Sprite left;
    public Sprite down;
    public Sprite up;
    public Sprite upright;
    public Sprite downright;
    public Sprite upleft;
    public Sprite downleft;
    public Sprite uprightc;
    public Sprite upleftc;
    public Sprite downrightc;
    public Sprite downleftc;
    // Start is called before the first frame update
    void Start()
    {
        int counter = 0;
        Collider2D hit = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.right);
        if(hit != null && hit.gameObject.tag == "Wall")
        {
            counter += 1;
        }
        Collider2D hit2 = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.left);
        if (hit2 != null && hit2.gameObject.tag == "Wall")
        {
            counter += 2;
        }
        Collider2D hit3 = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.up);
        if (hit3 != null && hit3.gameObject.tag == "Wall")
        {
            counter += 10;
        }
        Collider2D hit4 = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.down);
        if (hit4 != null && hit4.gameObject.tag == "Wall")
        {
            counter += 20;
        }
        Collider2D hit5 = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.up + Vector2.right);
        if (hit5 != null && hit5.gameObject.tag == "Wall" && counter == 0)
        {
            counter += 3;
        }
        Collider2D hit6 = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.up + Vector2.left);
        if (hit6 != null && hit6.gameObject.tag == "Wall" && counter == 0)
        {
            counter += 4;
        }
        Collider2D hit7 = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.down + Vector2.right);
        if (hit7 != null && hit7.gameObject.tag == "Wall" && counter == 0)
        {
            counter += 5;
        }
        Collider2D hit8 = Physics2D.OverlapPoint((Vector2)transform.position + Vector2.down + Vector2.left);
        if (hit8 != null && hit8.gameObject.tag == "Wall" && counter == 0)
        {
            counter += 6;
        }
        switch (counter)
        {
            case 0:
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = right;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = left;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = uprightc;
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = upleftc;
                break;
            case 5:
                GetComponent<SpriteRenderer>().sprite = downrightc;
                break;
            case 6:
                GetComponent<SpriteRenderer>().sprite = downleftc;
                break;
            case 10:
                GetComponent<SpriteRenderer>().sprite = up;
                break;
            case 11:
                GetComponent<SpriteRenderer>().sprite = upright;
                break;
            case 12:
                GetComponent<SpriteRenderer>().sprite = upleft;
                break;
            case 20:
                GetComponent<SpriteRenderer>().sprite = down;
                break;
            case 21:
                GetComponent<SpriteRenderer>().sprite = downright;
                break;
            case 22:
                GetComponent<SpriteRenderer>().sprite = downleft;
                break;
        }
    }

    public void Open()
    {
        GetComponent<Light2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = new Color(.8f,.8f,.8f,1);
    }
    
}
