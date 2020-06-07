using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public enum GameState { HOMEPAGE, PLAY, PAUSE, DEATH, CONT};

    public static GameState gameState;
    
    public static bool isDead = false;

    public GameObject player;
   // public GameObject life;
    bool playerCheck;


    


    

    void Start()
    {
        gameState = GameState.HOMEPAGE;
        
        
       

        
        //  life.gameObject.SetActive(true);

        



    }

    // Update is called once per frame
    void Update()
    {


        if (gameState == GameState.PAUSE && Input.GetKey("enter"))
        {
            gameState = GameState.PLAY;
        }



        if (gameState == GameState.PAUSE)
        {
            
        }

        else if (gameState == GameState.PLAY)
        {
            
            

        }
        else if (gameState == GameState.DEATH)
        {

           
            

        }
        else if (gameState == GameState.HOMEPAGE)
        {
         
          {
             
           }
        }
        

    }


   public void resetGame()
    {

        

        //   GameObject pl = Instantiate(player);
        //   pl.transform.position = new Vector3(0, 2.25f, 0);
        Enemyone.currentHealth = Enemyone.maxHealth;
       Enemyone.enemyRigidbody2d.gravityScale = 4;
        Enemyone.enemyCollider2d.enabled = true;
        Enemyone.enemy.localPosition = new Vector3(9, -6, 0);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}