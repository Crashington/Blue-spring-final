using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructionScript : MonoBehaviour
{
    public GameObject player;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (parent != null)
        {
            gameObject.transform.position = parent.transform.position + new Vector3(5, 5, 0);
        }

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (Vector2.Distance(player.transform.position, transform.position) - 10) * 1/4 *-1);
        if (Vector2.Distance(player.transform.position, transform.position) > 10)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }
        
}
