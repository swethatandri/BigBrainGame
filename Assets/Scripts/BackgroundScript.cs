using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float backgroundSpeed = 5f;
    public float repeatWidth;
    public Vector3 startPos;

    public PlayerController pc;

    void Start()
    {
        startPos = transform.position; // Establish the default starting position 
        repeatWidth = -10; // Set repeat width to half of the background

        pc = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {

        if(pc.gameOver == false && pc.pause == false){

        transform.Translate(Vector3.back * backgroundSpeed * Time.deltaTime);

        if(transform.position.z <= -20){


            transform.position = startPos;

        }

        }
       
    }

    
}
