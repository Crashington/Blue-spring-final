using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject thisGameObject;
    // Start is called before the first frame update
    void Start()
    {
        thisGameObject = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.name + gameObject.GetComponent<Rigidbody2D>().isKinematic + gameObject.GetComponent<CircleCollider2D>().enabled);
        playerScript.player.GetComponent<playerScript>().dropPickUp(thisGameObject);
        playerScript.player.GetComponent<playerScript>().pickUp(thisGameObject);
        playerScript.player.GetComponent<playerScript>().kickPickUp(thisGameObject);


    }
}
