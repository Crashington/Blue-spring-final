using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteScript : MonoBehaviour
{
    public GameObject player;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject credits;
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
                }
            }

            if (playerScript.isInScene)
            {
                sceneTimer += Time.deltaTime;


                image1.SetActive(true);
                if (sceneTimer >= 2)
                {

                    image2.SetActive(true);
                }
                if (sceneTimer >= 4)
                {
                    image3.SetActive(true);
                }
                if (sceneTimer >= 6)
                {
                    image4.SetActive(true);
                }
                if (sceneTimer >= 8)
                {
                    image5.SetActive(true);
                }

                if (sceneTimer >= 16)
                {
                    credits.SetActive(true);
                    
                }



            }
        }
    }
}
