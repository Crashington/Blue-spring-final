using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, gameObject.GetComponent<Image>().color.a + 0.00001f + (gameObject.GetComponent<Image>().color.a /2));


    }
}
