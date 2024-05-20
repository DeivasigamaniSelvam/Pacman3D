using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Transform parent,block;
    public float worldSizex = 18, worldSizey = 8, count=0, innercount=0;
    public bool unblock = false, canblock = true;
    void Start()
    {
        innercount = Random.Range(10, 20);
        for (float x = -7.2f; x <= worldSizex; x += .48f)
        {
            for (float y = -4.7f; y <= worldSizey; y += .42f)
            {
                Transform newBlock = Instantiate(block, new Vector2( x, y), Quaternion.identity, parent) as Transform;
                newBlock.gameObject.name = "Block" + count;
                count++;
                if((count >=0 && count <=22) || (count >= 638 && count <= 659))
                {
                    canblock = false;
                }
                else
                {
                    canblock = true;
                }
                
                for (int i = 0; i <= Random.Range(100, 600); i++)
                {
                    if (count % Random.Range(1,3) == 0 || unblock == true)
                    {
                        if (canblock)
                        {
                            newBlock.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        }
                    }
                    else
                    {
                        newBlock.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
                if (count%Random.Range(10,25) == 0)
                {
                    unblock = false;
                }
              }

            }
        parent.transform.position = new Vector2(0.2f, 0f);
    }
}
