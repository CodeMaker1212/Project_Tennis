using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using participants = ProjectEnums.Enums.ParticipantsOfGame;
using environments = ProjectEnums.Enums.Environments;



public class GameBehaviour : MonoBehaviour 
{
    private Player _player;
    private BallBehaviour _ball;
    private Text _whoThrowsText;
    private Text _playerScoresText;
    private Text _enemyScoresText;


    private GameObject _winTextObject;
    private GameObject _whoGetsPointTextObject;
    private GameObject _touchingNetTextObject;
    private GameObject _outTextObject;
    private GameObject _doubleTouchTextObject;

    public List<GameObject> environments= new List<GameObject>();
    public List<GameObject> ballsInScene= new List<GameObject>();

   
    private static int _roundsCount;
    public bool PlayerHitsFirst { get; private set; }
    public bool RoundHasBegan { get; set; } = false;
    public bool RoundIsOver { get; private set; }
    public static participants NextRoundBeginner { get; set; }
 

    private static string _lastTouched;
    public static string LastTouched
    {
        get { return _lastTouched; }
        set
        {
            if (value == "Player" || value == "Enemy")
                _lastTouched = value;
            else Debug.Log("Последним ударившим могут быть Player или Enemy");
        }
    }



    private void Awake()
    {
       Application.targetFrameRate = 60;

       if(MainManager.Instance !=null)
            SetEnvironment(MainManager.Instance.chosenCourt);

       RoundIsOver = false;
    }

    private void Start()
    {
        InitializeReferences();

        if (_roundsCount == 0) ShowWhoThrowsText(ChooseGameBeginner());
        else
        {
           
            ShowWhoThrowsText(NextRoundBeginner);
            
        }

        DisableOutText();
        DisableTouchingNetText();
        DisableDoubleTouchText();
        DisableWinText();

        Invoke("InitializeAndSubscribeToBall", 2f);
    }
    private void Update()
    {
      
        PrintPlayerScores();
        PrintEnemyScores();


        if(ScoreManager.PlayerScores == ScoreManager.MaxScores || ScoreManager.EnemyScores == ScoreManager.MaxScores)
            ShowWinText(DetermineWinner());
    }

    private void InitializeAndSubscribeToBall()
    {
        _ball = ballsInScene[0].GetComponent<BallBehaviour>();
        _ball.DoubleTouch += ShowDoubleTouchText;
    }
    private void InitializeReferences()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _whoThrowsText = GameObject.Find("Who_Throws_Text").GetComponent<Text>();
        _playerScoresText = GameObject.Find("Player_Scores_Text").GetComponent<Text>();
        _enemyScoresText = GameObject.Find("Enemy_Scores_Text").GetComponent<Text>();

        _outTextObject = GameObject.Find("Out_Text");
        _touchingNetTextObject = GameObject.Find("Touching_Net_Text");
        _whoGetsPointTextObject = GameObject.Find("Who_Gets_Point_Text");
        _doubleTouchTextObject = GameObject.Find("Double_Touch_Text");
        _winTextObject = GameObject.Find("Win_Text");
    }

    private void RestartLevel()
    {
        _roundsCount++;
        SceneManager.LoadScene("Game");
       
    }
    public void ExitGame() => SceneManager.LoadScene("New_Game_Settings");
    private void BackToMainMenu()
    {
        _roundsCount = 0;
        ScoreManager.ResetScores();
        SceneManager.LoadScene("Main_Menu");
    }
    private participants ChooseGameBeginner()
    {
        int playerNumber = 1;
        int enemyNumber = 3;
        int rdmNumber = Random.Range(playerNumber, enemyNumber);
       
        NextRoundBeginner = (rdmNumber == playerNumber) ? participants.Player :participants.Enemy;

        return NextRoundBeginner;

    }
    private void SetEnvironment(environments chosenEnvironment) => environments[(int)chosenEnvironment].gameObject.SetActive(true);
    private void ShowWhoThrowsText(participants nameOfParticipant)
    {
        if (nameOfParticipant == participants.Player)
        {
            _whoThrowsText.text = "PLAYER HITS FIRST";
            _whoThrowsText.color = new Color(0.6f, 1f, 1f);
        }
        else if(nameOfParticipant == participants.Enemy)
        {
            _whoThrowsText.text = "ENEMY HITS FIRST";
            _whoThrowsText.color = new Color(1f, 0.63f, 0.63f);
        }
        Invoke("DisableWhoThrowsText", 2f);
    }
    public void ShowOutText(string objectName)
    {
        RoundHasBegan = false;
        RoundIsOver = true;

        _outTextObject.gameObject.SetActive(true);

        Text outText = _outTextObject.GetComponent<Text>();

        switch (objectName)
        {
            case "Player":
                outText.color = new Color(0.6f, 1f, 1f);
                break;
            case "Enemy": outText.color = new Color(1f, 0.63f, 0.63f);
                break;
        }

        Invoke("DisableOutText", 2f);
        Invoke("RestartLevel", 2f);
    }
    private void DisableOutText() =>_outTextObject.gameObject.SetActive(false);
    private void DisableWhoThrowsText() => _whoThrowsText.gameObject.SetActive(false);
    private void PrintPlayerScores() => _playerScoresText.text = "" + ScoreManager.PlayerScores;
    private void PrintEnemyScores() => _enemyScoresText.text = ScoreManager.EnemyScores + "";
    public void ShowTouchingNetText(string objectName)
    {
        RoundHasBegan = false;
        RoundIsOver= true;

        _touchingNetTextObject.gameObject.SetActive(true);

        Text touchingNetText = _touchingNetTextObject.GetComponent<Text>();

        switch (objectName)
        {
            case "Player":
                touchingNetText.color = new Color(0.6f, 1f, 1f);
                break;
            case "Enemy":
                touchingNetText.color = new Color(1f, 0.63f, 0.63f);
                break;
        }

        Invoke("DisableTouchingNetText", 2f);
        Invoke("RestartLevel", 2f);

    }
    private void DisableTouchingNetText() => _touchingNetTextObject.gameObject.SetActive(false);
    public void ShowWhoGetsPointText(string objectName)
    {
        RoundIsOver = true;

        if(ScoreManager.PlayerScores < ScoreManager.MaxScores && ScoreManager.EnemyScores < ScoreManager.MaxScores)
        {

            _whoGetsPointTextObject.gameObject.SetActive(true);

            Text whoGetsPointText = _whoGetsPointTextObject.GetComponent<Text>();

            switch (objectName)
            {
                case "Player":
                    whoGetsPointText.text = "PLAYER GETS A POINT!";
                    whoGetsPointText.color = new Color(0, 0.0775275f, 0.5943396f);
                    break;

                case "Enemy":
                    whoGetsPointText.text = "ENEMY GETS A POINT!";
                    whoGetsPointText.color = new Color(0.5283019f, 0, 0.01586957f);
                    break;
            }

        } 

        Invoke("DisableWhoGetsPointText", 2f);
        Invoke("RestartLevel", 2f);
    }
    private void DisableWhoGetsPointText() => _whoGetsPointTextObject.gameObject.SetActive(false);

    private void ShowDoubleTouchText()
    {
        if (RoundHasBegan == true && RoundIsOver == false)
        {
            RoundHasBegan = false;
            RoundIsOver = true;

            ScoreManager.DeleteScoreFrom(LastTouched);

            _doubleTouchTextObject.gameObject.SetActive(true);

            Text doubleTouchText = _doubleTouchTextObject.GetComponent<Text>();

            switch (LastTouched)
            {
                case "Player":
                    NextRoundBeginner = participants.Enemy;
                    doubleTouchText.color = new Color(0.6f, 1f, 1f);
                    break;

                case "Enemy":
                    NextRoundBeginner = participants.Player;
                    doubleTouchText.color = new Color(1f, 0.63f, 0.63f);
                    break;
            }

            Invoke("DisableDoubleTouchText", 2f);
            Invoke("RestartLevel", 2f);
        }
    }
    private void DisableDoubleTouchText() => _doubleTouchTextObject.gameObject.SetActive(false);

    private string DetermineWinner()
    {
        string winner = null;

        if (ScoreManager.PlayerScores == ScoreManager.MaxScores)
            winner = "Player";
        else if (ScoreManager.EnemyScores == ScoreManager.MaxScores)
            winner = "Enemy";

        return winner;
    }
   
    private void ShowWinText(string whoWin)
    {
        RoundHasBegan = false;
        RoundIsOver = true;

        _winTextObject.gameObject.SetActive(true);

        Text winText = _winTextObject.GetComponent<Text>();

        switch (whoWin)
        {
            case "Player":
                winText.text = "PlAYER WON!";
                winText.color = new Color(0.6f, 1f, 1f);
                break;

            case "Enemy":
                winText.text = "ENEMY WON!";
                winText.color = new Color(1f, 0.63f, 0.63f);
                break;
        }
      
        Invoke("BackToMainMenu", 2f);
    }
    private void DisableWinText() => _winTextObject.gameObject.SetActive(false);
    
}
