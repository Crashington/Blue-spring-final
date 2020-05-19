using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditScript : MonoBehaviour
{
    private float creditEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.03f);
        if (transform.localPosition.y >=3050)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, 3050);
            creditEnd += Time.deltaTime;
        }

        if (creditEnd >= 10)
        {
            Debug.Log("QUIT APPLICATION");
            Application.Quit();
        }
    }
}
