using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontroller : MonoBehaviour
{
    public bool rightmove = false;
    public float speed = 6f;
    public static enemycontroller instance;
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        if(!rightmove && playercontroller.instance.gamestate != playercontroller.GameState.gameend && playercontroller.instance.gamestate != playercontroller.GameState.playerfruit)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
        else if(playercontroller.instance.gamestate != playercontroller.GameState.gameend && playercontroller.instance.gamestate != playercontroller.GameState.playerfruit)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "leftblock" && collision.gameObject.GetComponentInParent<SpriteRenderer>().enabled == true && playercontroller.instance.gamestate != playercontroller.GameState.gameend)
        {
            rightmove = false;
        }
        else if (collision.gameObject.tag == "rightblock" && collision.gameObject.GetComponentInParent<SpriteRenderer>().enabled == true && playercontroller.instance.gamestate != playercontroller.GameState.gameend)
        {
            rightmove = true;
        }
    }

    public void enemyspeed()
    {
        if (playercontroller.instance.gamestate == playercontroller.GameState.playerfruit)
        {
            speed /= 10;
        }
        else
        {
            speed = 6;
        }
    }

    public IEnumerator resetenemy()
    {
        yield return new WaitForSecondsRealtime(10.0f);
        playercontroller.instance.gamestate = playercontroller.GameState.gameinprogess;
        enemyspeed();
        playercontroller.instance.fruit.transform.position = new Vector2(playercontroller.instance.fruit.transform.position.x, playercontroller.instance.fruit.transform.position.y);
    }
}
