using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public static playercontroller instance;
    public SpriteRenderer Player;
    public Rigidbody2D m_Rigidbody;
    public bool canset = true;
    public GameObject fruit;
    public enum GameState { gamestart, gameend, gameinprogess, playerfruit};
    public enum MobilePlayerInput { idle,left, right, up, down };
    public enum CanPlayerMove { idle,move, left, right, up, down };
    public GameState gamestate;
    public MobilePlayerInput mobileplayerinput;
    public CanPlayerMove canplayermove;
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        if (gamestate != GameState.gameend)
        {
            if ((Input.GetKey(KeyCode.LeftArrow) || mobileplayerinput == MobilePlayerInput.left) && (canplayermove != CanPlayerMove.right))
            {

                Player.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Player.flipX = true;
                transform.position -= new Vector3(2f * Time.deltaTime, 0f, 0f);
            }
            if ((Input.GetKey(KeyCode.RightArrow) || mobileplayerinput == MobilePlayerInput.right) && (canplayermove != CanPlayerMove.left))
            {

                Player.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Player.flipX = false;
                transform.position += new Vector3(2f * Time.deltaTime, 0f, 0f);
            }
            if ((Input.GetKey(KeyCode.UpArrow) || mobileplayerinput == MobilePlayerInput.up) && (canplayermove != CanPlayerMove.down)) 
            {
                Player.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
                Player.flipX = false;
                transform.position += new Vector3(0f, 2f * Time.deltaTime, 0f);
            }
            if ((Input.GetKey(KeyCode.DownArrow) | mobileplayerinput == MobilePlayerInput.down) && (canplayermove != CanPlayerMove.up))
            {
                Player.gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
                Player.flipX = false;
                transform.position -= new Vector3(0f, 2f * Time.deltaTime, 0f);
            }
        }
    }
    int triggercount = 0;
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "block" && gamestate != GameState.gameend)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            uicontroller.instance.updateprogress();
            if (triggercount % 3 == 0)
            {
                canset = true;
                StartCoroutine(Setposition(collision.gameObject));
            }
            triggercount++;
        }
        if(collision.gameObject.tag == "enemy" && gamestate != GameState.gameend)
        {
            uicontroller.instance.updatelife();
            if (uicontroller.instance.lifecount <= 0)
            {
                m_Rigidbody.gravityScale = 1;
                gamestate = GameState.gameend;
            }
        }
        if (collision.gameObject.tag == "fruit" && gamestate != GameState.gameend)
        {
            gamestate = GameState.playerfruit;
            collision.gameObject.SetActive(false);
            enemycontroller.instance.enemyspeed();
            StartCoroutine(enemycontroller.instance.resetenemy());
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "leftblock" && collision.gameObject.GetComponentInParent<SpriteRenderer>().enabled == true && gamestate != GameState.gameend)
        {
            canplayermove = CanPlayerMove.left;
        }
        else if (collision.gameObject.tag == "rightblock" && collision.gameObject.GetComponentInParent<SpriteRenderer>().enabled == true && gamestate != GameState.gameend)
        {
            canplayermove = CanPlayerMove.right;
        }
        else if (collision.gameObject.tag == "upblock" && collision.gameObject.GetComponentInParent<SpriteRenderer>().enabled == true && gamestate != GameState.gameend)
        {
            canplayermove = CanPlayerMove.up;
        }
        else if (collision.gameObject.tag == "downblock" && collision.gameObject.GetComponentInParent<SpriteRenderer>().enabled == true && gamestate != GameState.gameend)
        {
            canplayermove = CanPlayerMove.down;
        }
    }

    IEnumerator Setposition(GameObject obj)
    {
        yield return new WaitForSecondsRealtime(0.05f);
        if (canset)
        {
            StopCoroutine(Setposition(obj));
            gameObject.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y);
            canset = false;
        }
    }
}
