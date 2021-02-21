using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillSpawnManager : MonoBehaviour
{
    // Start is called before the first frame update

     public GameObject[] pillPrefabs;

     public PlayerController pc;

    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("generatePrefab", 0, 1);
        InvokeRepeating("powerupSpawn", 0, 8);

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generatePrefab(){

        if(pc.gameOver == false && pc.pause == false){

        int index = Random.Range(0, pillPrefabs.Length);
        Vector3 pillLocation = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(0,5f));

        Instantiate(pillPrefabs[index], pillLocation, pillPrefabs[index].transform.rotation);

        }

        if(pc.timedGameOver == false && pc.pause == false){

        int index = Random.Range(0, pillPrefabs.Length -1);
        Vector3 pillLocation = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(0,5f));

        Instantiate(pillPrefabs[index], pillLocation, pillPrefabs[index].transform.rotation);

        }


    }

    void powerupSpawn(){

        if(pc.timedGameOver == false && pc.pause == false){

            int index = pillPrefabs.Length -1;
           Vector3 pillLocation = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(0,5f));

        Instantiate(pillPrefabs[index], pillLocation, pillPrefabs[index].transform.rotation);

            
        }

        



    }




  
}
