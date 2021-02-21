using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] obstaclePrefabs;

    public GameObject[] obstacleWarnings;
    private float startRate = 0f;
    public float repeatRate = 1.3f;
    public PlayerController pc;
    private AudioSource spawnManagerAudio;
    public AudioClip loading;

    int referencenumber = 10;

    int timedReferenceNumber = 15;


   /* public TextMeshProUGUI levelUp; */

    
    void Start()
    {

        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManagerAudio = GetComponent<AudioSource>();
       
        InvokeRepeating("spawnRandomEnemy", startRate, repeatRate);
    
    }

    // Update is called once per frame
    void Update()
    {
        if(pc.gameOver == false){
        RepeatRateChange();
        }

        if(pc.timedGameOver == false){
            NewTimedStage();
        }
    }

    void spawnRandomEnemy(){

        if(pc.gameOver == false && pc.pause == false){

        int enemyIndex = Random.Range(0, obstaclePrefabs.Length);
        float enemyY = -0.49f;
        float enemyZ = 20f;
        
        
        Vector3 enemyLocation = new Vector3(Random.Range(-10f, 10f), enemyY, enemyZ);

        Instantiate(obstaclePrefabs[enemyIndex], enemyLocation, obstaclePrefabs[enemyIndex].transform.rotation);

        }

        if(pc.timedGameOver == false && pc.pause == false){

            CancelInvoke("spawnRandomEnemy");
            
            for(int numobstacles = 0; numobstacles <= 5; numobstacles++){

                int enemyIndex = Random.Range(0, 3);
                float enemyY = -0.49f;
                float enemyX = Random.Range(-10f, 10f);
                float enemyZ = Random.Range(-1f, 4f);
                
            Vector3 enemyLocation = new Vector3(enemyX, enemyY, enemyZ);


            if(enemyLocation != pc.transform.position){

                Instantiate(obstaclePrefabs[enemyIndex], enemyLocation, obstaclePrefabs[enemyIndex].transform.rotation);}

            }
            
            }

            
        }

      public void RepeatRateChange(){
        int score = GameObject.Find("Player").GetComponent<PlayerController>().score;

        if(score >= referencenumber ){

            CancelInvoke("spawnRandomEnemy");
            repeatRate = repeatRate * 0.95f;

            InvokeRepeating("spawnRandomEnemy", 0, repeatRate);

            referencenumber+= 10;
        }
 
}

      public void NewTimedStage(){

          int score = GameObject.Find("Player").GetComponent<PlayerController>().score;
          if(score >= timedReferenceNumber){

              GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag ("Enemy");
 
             for(var i = 0 ; i < gameObjects.Length ; i ++) {
                Destroy(gameObjects[i]); }


            spawnRandomEnemy();

            pc.transform.position = new Vector3(0, 0.5f, -4);
            timedReferenceNumber+= 15;
            spawnManagerAudio.PlayOneShot(loading, 0.6f);



          }

          


      }




}
