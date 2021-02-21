using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillMovement : MonoBehaviour
{

    // Start is called before the first frame update

    public float pillspeed = 5f;

    public float tempSpeed = 0;

    public float normSpeed = 5f;

    public PlayerController pc; 
    

    void Start()
    {

        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(pillTime());
        
    }

    // Update is called once per frame
    void Update()
    {

        if(pc.gameOver == false && pc.pause == false){ 

            pillspeed = normSpeed;

            destroyPill();


        }

        transform.Translate(Vector3.back * pillspeed *Time.deltaTime);

        if(pc.gameOver == true || pc.pause == true){
            pillspeed = tempSpeed;
        }


        
    }

    void destroyPill(){

        if(transform.position.z < -5){

            Destroy(gameObject);
        }


    }

    IEnumerator pillTime(int seconds = 4){

        Debug.Log("Came into pillTime");

        if(pc.timedGameOver == false){

          while(seconds > 0){

            if(pc.pause == false){


            yield return new WaitForSeconds(1);
            seconds--; }

            else{
                yield return null;
            }

            }

            Destroy(gameObject);

            
        }

        
        }


    
}
