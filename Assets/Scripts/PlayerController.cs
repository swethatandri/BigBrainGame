using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI scoretext;

    public TextMeshProUGUI timetext;
    public TextMeshProUGUI titletext;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI instructionsText;

    public TextMeshProUGUI plusScore;



    public bool hasPowerUp = false;

    public GameObject powerUp;
    public AudioClip pillSound;

    public AudioClip enemyDestroy;

    public AudioClip powerUpSound;

    public AudioClip losePointSound;

    public AudioClip timedMode;

    public AudioClip infiniteMode;

    public AudioClip boom;

    private AudioSource playerAudio;

    private AudioSource cameraAudio;

    public float directionSpeed = 5f;
    public float speed = 0.001f;

    public int timeLeft = 60;
    private float upperBound = 10f;

    private float forwardBound = 5f;

    public bool gameOver;

    public bool timedGameOver;

    public bool pause;

    public Button restartButton;

    public Button playButton;

    public Button timedButton;

    public Button instructionsButton;

    public Button homeButton;

    public Button pauseButton;

    public Button unpauseButton;





    public int score;
    // Start is called before the first frame update
    void Start()   {

        cameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()

    {

        if((gameOver == false || timedGameOver == false) && pause == false){
        
        checkBounds();

        if(Input.GetKey(KeyCode.UpArrow)){

         transform.Translate(Vector3.forward * Time.deltaTime * directionSpeed);
         
        }


        if(Input.GetKey(KeyCode.LeftArrow)){

            transform.Translate(Vector3.left * Time.deltaTime * directionSpeed);
        }

        if(Input.GetKey(KeyCode.RightArrow)){

            transform.Translate(Vector3.right * Time.deltaTime * directionSpeed);
        }

        if(Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(Vector3.back * Time.deltaTime * (directionSpeed));
        }

        }

        



    
    }

    void checkBounds(){


        if(transform.position.x > upperBound){

            transform.position = new Vector3(upperBound- 1, transform.position.y, transform.position.z);
        }

        else if(transform.position.x < -upperBound){
            transform.position = new Vector3(-upperBound + 1, transform.position.y, transform.position.z);
        }

        if(transform.position.z > forwardBound){

            transform.position = new Vector3(transform.position.x, transform.position.y, forwardBound-1);
        }

        if(transform.position.z < -forwardBound){

            transform.position = new Vector3(transform.position.x, transform.position.y, -forwardBound + 1);
        }


    }

    private void OnTriggerEnter(Collider other){

       if(other.CompareTag("Green Pill")){

           UpdateScore(1);
           
           StartCoroutine(PlusScore(1));


           playerAudio.PlayOneShot(pillSound, 4.0f);
            Destroy(other.gameObject);
           
       }

       if(other.CompareTag("Red Pill")){

           UpdateScore(2);
           
           StartCoroutine(PlusScore(2));
           playerAudio.PlayOneShot(pillSound, 4.0f);
            Destroy(other.gameObject);
           
       }

       if(other.CompareTag("Magenta Pill")){

           UpdateScore(3);
           
           StartCoroutine(PlusScore(3));
           playerAudio.PlayOneShot(pillSound, 4.0f);
            Destroy(other.gameObject);
           
       }

       if(other.CompareTag("Teal Pill")){

           

           UpdateScore(4);
          
           StartCoroutine(PlusScore(4));
           playerAudio.PlayOneShot(pillSound, 4.0f);
            Destroy(other.gameObject);
           
       }

       if(other.CompareTag("Black Pill")){

           UpdateScore(-1);
           
           StartCoroutine(PlusScore(-1));
           
           playerAudio.PlayOneShot(losePointSound, 4.0f);
            Destroy(other.gameObject);
           

           
       }

       if(other.CompareTag("Time Icon")){


           playerAudio.PlayOneShot(powerUpSound, 4.0f);

           if(gameOver == false){

               if(hasPowerUp == false){
                   
               

               hasPowerUp = true;
               powerUp.SetActive(true);
               powerUp.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
               timetext.gameObject.SetActive(true);
               timetext.text = "Time Remaining: ";
               StartCoroutine(TenSecondsPowerUp());
                Destroy(other.gameObject);

               }

               else{
                   Destroy(other.gameObject);
               }
           }

           if(timedGameOver == false){
            
           timeLeft += 5;
          
           timetext.text = "Time left: "  + timeLeft;
           
           StopAllCoroutines();
           StartCoroutine(StartCountdown(timeLeft));
            Destroy(other.gameObject); }

    
           
       }

       if(other.CompareTag("Enemy")){


            if(hasPowerUp == true){


                Destroy(other.gameObject);
                playerAudio.PlayOneShot(enemyDestroy);

            }

            if(hasPowerUp == false){

            gameOver = true;
            timedGameOver = true;
            directionSpeed = 0;

            playerAudio.PlayOneShot(boom, 4.0f);

            
            gameOverText.gameObject.SetActive(true);
            StopAllCoroutines();
            timetext.text = "Time Left: " + 0;
            gameOverText.text += " Your Brainpower is " + score + "!";
            restartButton.gameObject.SetActive(true);} 

            /* Destroy(gameObject); */
        }
       
       
    }

    

    private void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoretext.text = "Brainpower: " + score;
    }

    public void RestartGame(){
        gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(){

        cameraAudio.Stop();

        pause = false;

        cameraAudio.loop = true;
        cameraAudio.clip = infiniteMode;
        cameraAudio.Play();

        gameOver = false;
        titletext.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        timedButton.gameObject.SetActive(false);
        timetext.gameObject.SetActive(false);
        scoretext.gameObject.SetActive(true);
        instructionsText.gameObject.SetActive(false);
        instructionsButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        unpauseButton.gameObject.SetActive(true);
        score = 0;
        scoretext.text = "Brainpower: " + score;

        playerAudio = GetComponent<AudioSource>();
        
    }

    public void StartTimedGame(){
        timedGameOver = false;
        pause = false;
        titletext.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        timedButton.gameObject.SetActive(false);
        instructionsButton.gameObject.SetActive(false);
        timetext.gameObject.SetActive(true);
        scoretext.gameObject.SetActive(true);
        instructionsText.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        unpauseButton.gameObject.SetActive(true);


        directionSpeed = 7f;

        cameraAudio.loop = true;
        cameraAudio.clip = timedMode;
        cameraAudio.Play();

        score = 0;
        timeLeft = 60;
        scoretext.text = "Brainpower: " + score;
        timetext.text = "Time Left: " + timeLeft;

        playerAudio = GetComponent<AudioSource>();
        StartCoroutine(StartCountdown(timeLeft));

    }

    public void HowToPlay(){

        instructionsText.gameObject.SetActive(true);
        homeButton.gameObject.SetActive(true);
        titletext.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        timedButton.gameObject.SetActive(false);
        instructionsButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
    
        
    }

    public void HomeButton(){

        titletext.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        timedButton.gameObject.SetActive(true);
        instructionsButton.gameObject.SetActive(true);
        gameObject.SetActive(true);
        instructionsText.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);


    }

    public void PauseButton(){

        pause = true;
        
    }
    

    public void UnpauseButton(){
        pause = false;
        
    }


    public IEnumerator StartCountdown(int countdownValue){

        while(countdownValue >= 0){

            timeLeft = countdownValue;

            if(pause == false){

            timetext.text = "Time Left: " + countdownValue;
            yield return new WaitForSeconds(1);
            countdownValue = countdownValue -1 ;

            }

            if(pause == true){

                yield return null;
            }

            
            
            
        }

        timedGameOver = true;
        gameOverText.gameObject.SetActive(true);
        gameOverText.text += " Your Brainpower is " + score + "!";
        restartButton.gameObject.SetActive(true);
    }

    public IEnumerator PlusScore(int score, float seconds = 0.5f){

        while(seconds >= 0){

            plusScore.gameObject.SetActive(true);
            plusScore.text = "+" + score;
            if(pause == false){
            yield return new WaitForSeconds(seconds);
            seconds--;}
        }

        plusScore.gameObject.SetActive(false);


    }

    public IEnumerator TenSecondsPowerUp(int duration = 10){


        while(duration >= 0){

            bool isPaused = pause;

            if(isPaused == true){
                yield return null;
            }
            if(isPaused == false){
            yield return new WaitForSeconds(1);
            timetext.text = "Time Remaining: " + duration;
            duration = duration - 1 ;
            }

        }

       hasPowerUp = false;
       timetext.gameObject.SetActive(false);
       powerUp.gameObject.SetActive(false);

        
    } 


}
