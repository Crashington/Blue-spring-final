using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class portraitScript : MonoBehaviour
{
    public GameObject player;
    public GameObject image;

    public float sceneTimer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 4f)
        {
            if (!playerScript.isInScene)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerScript.isInScene = true;
                    sceneTimer = 0;
                    image.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
            }

            if (playerScript.isInScene)
            {
                sceneTimer += Time.deltaTime;


                image.SetActive(true);
                if (sceneTimer >= 3)
                {
                    image.GetComponent<Image>().color = new Color(1, 1, 1, 0);


                    image.SetActive(false);
                    playerScript.isInScene = false;

                }




            }
        }
    }
}
