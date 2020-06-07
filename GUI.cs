using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour


{

    public Image backGround;
    public Button startButton;
    public Button controlsButton;
    public Button quitButton;
    public Button backHome;
    public Button continuePlaying;
    public Image chhayaChobi;
    public Image controlGuid;
    public Image win;
    public Image quitBox;
    








    public bool statuss;




    void Start()
    {


        startButton.onClick.AddListener(StartGame);
        
        //  controlsButton.onClick.AddListener(RestartGame);
        //  quitButton.onClick.AddListener(MainGame);





        startButton.gameObject.SetActive(true);
        backGround.gameObject.SetActive(true);
        controlsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        chhayaChobi.gameObject.SetActive(true);

        controlGuid.gameObject.SetActive(false);
        backHome.gameObject.SetActive(false);
        win.gameObject.SetActive(false);
        continuePlaying.gameObject.SetActive(false);
        quitBox.gameObject.SetActive(false);




    }


    void Update()
    {





        if (GameManager.gameState == GameManager.GameState.HOMEPAGE)
        {
            startButton.gameObject.SetActive(true);
            backGround.gameObject.SetActive(true);
            controlsButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            chhayaChobi.gameObject.SetActive(true);

            controlGuid.gameObject.SetActive(false);
            backHome.gameObject.SetActive(false);
            win.gameObject.SetActive(false);
            continuePlaying.gameObject.SetActive(false);


        }
        else if (GameManager.gameState == GameManager.GameState.PAUSE)
        {

            

            startButton.gameObject.SetActive(false);
            backGround.gameObject.SetActive(false);
            controlsButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            chhayaChobi.gameObject.SetActive(false);
            win.gameObject.SetActive(false);
            backHome.gameObject.SetActive(true);
            continuePlaying.gameObject.SetActive(true);
            controlGuid.gameObject.SetActive(true);



        }

        else if (GameManager.gameState == GameManager.GameState.CONT)
        {
            startButton.gameObject.SetActive(false);
            backGround.gameObject.SetActive(false);
            controlsButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            chhayaChobi.gameObject.SetActive(false);
            win.gameObject.SetActive(false);
            backHome.gameObject.SetActive(true);
            continuePlaying.gameObject.SetActive(false);
            controlGuid.gameObject.SetActive(true);



        }
        else if (GameManager.gameState == GameManager.GameState.PLAY)
        {
            startButton.gameObject.SetActive(false);
            backGround.gameObject.SetActive(false);
            controlsButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            chhayaChobi.gameObject.SetActive(false);
            win.gameObject.SetActive(false);
            backHome.gameObject.SetActive(false);
            continuePlaying.gameObject.SetActive(false);
            controlGuid.gameObject.SetActive(false);





        }
        else if (GameManager.gameState == GameManager.GameState.DEATH)
        {


            startButton.gameObject.SetActive(false);
            backGround.gameObject.SetActive(false);
            controlsButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            chhayaChobi.gameObject.SetActive(false);
            win.gameObject.SetActive(true);
            backHome.gameObject.SetActive(true);
            continuePlaying.gameObject.SetActive(false);



        }

    }


        public void StartGame()
        {


            GameManager.gameState = GameManager.GameState.PLAY;

        }

        public void RestartGame()
        {
            GameManager.gameState = GameManager.GameState.PLAY;


        }

        public void MainGame()
        {
            GameManager.gameState = GameManager.GameState.HOMEPAGE;


        }
    public void Controls()
    {
        GameManager.gameState = GameManager.GameState.CONT;


    }

    public void Quiton()
    {
        quitBox.gameObject.SetActive(true);
    }

    public void Quitoff()
    {
        quitBox.gameObject.SetActive(false);
    }


}
