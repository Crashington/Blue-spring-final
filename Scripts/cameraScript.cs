using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float cameraHeight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()


    {
        if (playerScript.gamePaused)
        {
            transform.position -= new Vector3(0, 0.2f,0);
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color += new Color(1, 1, 1, 0.005f);
            if (transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a > 1)
            {
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            }
            if (transform.position.y <= 0.8756284f + cameraHeight)
            {
                playerScript.gamePaused = false;
            }
        }


        if (!playerScript.gamePaused)
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.005f);
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraHeight, -20);
            if (transform.position.x < -138)
            {
                transform.position = new Vector3(-138, player.transform.position.y + cameraHeight, -20);
            }

            if (transform.position.x > 2810)
            {
                transform.position = new Vector3(2810, player.transform.position.y + cameraHeight, -20);
            }
        }
    }
}
