using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject brain;
    public float speed;

    public float normSpeed = 3f;

    public float tempSpeed = 0;
    private float enemyBound = -5;
    public GameObject player;
    public PlayerController pc;
    

  



    public int referenceNumber;


    void Start()
    {
        
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();

        referenceNumber = 10;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(pc.gameOver == false && pc.pause == false){

        speed = normSpeed;
       
        transform.Translate(Vector3.back * speed * Time.deltaTime); 
        destoyEnemy();

        UpdateSpeed();

        }

        if(pc.gameOver == true || pc.pause == true){
            speed = tempSpeed;
        } 

        
    }


    void destoyEnemy(){

        if(transform.position.z < enemyBound){

            Destroy(gameObject);
        }

    }

   

    void UpdateSpeed(){


        int score = GameObject.Find("Player").GetComponent<PlayerController>().score;

        if(score >= referenceNumber){

            normSpeed *= 1.2f;

            referenceNumber += 10;
            


        }

    }

   
}

