using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using participants = ProjectEnums.Enums.ParticipantsOfGame;
using environments = ProjectEnums.Enums.Environments;



public class GameBehaviour : MonoBehaviour 
{
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
    public participants GameBeginner { get; set; }
    public participants GameWinner { get; private set; }
    public bool RoundHasBegan { get; set; } = false;
    public bool RoundIsOver { get; private set; }
    public static participants NextRoundBeginner { get; set; }
    public static participants LastTouched { get; set; }
   
    private Color _playerColor = new Color(0.6f, 1f, 1f);
    private Color _enemyColor = new Color(1f, 0.63f, 0.63f);


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

        DisableOutText();
        DisableTouchingNetText();
        DisableDoubleTouchText();
        DisableWinText();

        Invoke("InitializeAndSubscribeToBall", 2f);


        if (TutorialBehaviour.isIncluded == true)
            GameBeginner = participants.Player;
        else
        {
            if (_roundsCount == 0) ShowWhoThrowsText(ChooseGameBeginner());
            else ShowWhoThrowsText(NextRoundBeginner);
        }     
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
    public void ExitGame()
    {
        _roundsCount = 0;
        ScoreManager.ResetScores();
        SceneManager.LoadScene("New_Game_Settings");
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
    private void ShowWhoThrowsText(participants name)
    {
        if(RoundIsOver == false)
        {
            if (name == participants.Player)
            {
                _whoThrowsText.text = "PLAYER HITS FIRST";
                _whoThrowsText.color = _playerColor;
            }
            else if (name== participants.Enemy)
            {
                _whoThrowsText.text = "ENEMY HITS FIRST";
                _whoThrowsText.color = _enemyColor;
            }
            Invoke("DisableWhoThrowsText", 2f);
        }

        
    }
    public void ShowOutText(participants name)
    {
        RoundHasBegan = false;
        RoundIsOver = true;

        _outTextObject.gameObject.SetActive(true);

        Text outText = _outTextObject.GetComponent<Text>();

        switch (name)
        {
            case participants.Player:
                outText.color = _playerColor;
                break;
            case participants.Enemy:
                outText.color =_enemyColor;
                break;
        }

        Invoke("DisableOutText", 2f);
        Invoke("RestartLevel", 2f);
    }
    private void DisableOutText() =>_outTextObject.gameObject.SetActive(false);
    private void DisableWhoThrowsText() => _whoThrowsText.gameObject.SetActive(false);
    private void PrintPlayerScores() => _playerScoresText.text = "" + ScoreManager.PlayerScores;
    private void PrintEnemyScores() => _enemyScoresText.text = ScoreManager.EnemyScores + "";
    public void ShowTouchingNetText(participants name)
    {
        if(RoundIsOver == false)
        {
            RoundHasBegan = false;
            RoundIsOver = true;

            _touchingNetTextObject.gameObject.SetActive(true);

            Text touchingNetText = _touchingNetTextObject.GetComponent<Text>();

            switch (name)
            {
                case participants.Player:
                    touchingNetText.color = _playerColor;
                    break;
                case participants.Enemy:
                    touchingNetText.color = _enemyColor;
                    break;
            }

            Invoke("DisableTouchingNetText", 2f);
            Invoke("RestartLevel", 2f);
        }
    }
    private void DisableTouchingNetText() => _touchingNetTextObject.gameObject.SetActive(false);
    public void ShowWhoGetsPointText(participants name)
    {
        if(ScoreManager.PlayerScores < ScoreManager.MaxScores && ScoreManager.EnemyScores < ScoreManager.MaxScores && RoundIsOver == false)
        {
            RoundIsOver = true;

            _whoGetsPointTextObject.gameObject.SetActive(true);

            Text whoGetsPointText = _whoGetsPointTextObject.GetComponent<Text>();

            switch (name)
            {
                case participants.Player:
                    whoGetsPointText.text = "PLAYER GETS A POINT!";
                    whoGetsPointText.color = _playerColor;
                    break;

                case participants.Enemy:
                    whoGetsPointText.text = "ENEMY GETS A POINT!";
                    whoGetsPointText.color = _enemyColor;
                    break;
            }

            Invoke("DisableWhoGetsPointText", 2f);
            Invoke("RestartLevel", 2f);
        } 
      
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
                case participants.Player:
                    NextRoundBeginner = participants.Enemy;
                    doubleTouchText.color = _playerColor;
                    break;

                case participants.Enemy:
                    NextRoundBeginner = participants.Player;
                    doubleTouchText.color = _enemyColor;
                    break;
            }

            Invoke("DisableDoubleTouchText", 2f);
            Invoke("RestartLevel", 2f);
        }
    }
    private void DisableDoubleTouchText() => _doubleTouchTextObject.gameObject.SetActive(false);

    private participants DetermineWinner()
    {
          if (ScoreManager.PlayerScores == ScoreManager.MaxScores)
              GameWinner = participants.Player;

          else if (ScoreManager.EnemyScores == ScoreManager.MaxScores)
              GameWinner = participants.Enemy;

          return GameWinner;
    }
   
    private void ShowWinText(participants whoWin)
    {
        RoundHasBegan = false;
        RoundIsOver = true;

        _winTextObject.gameObject.SetActive(true);

        Text winText = _winTextObject.GetComponent<Text>();

        switch (whoWin)
        {
            case participants.Player:
                winText.text = "PlAYER WON!";
                winText.color = _playerColor;
                break;

            case participants.Enemy:
                winText.text = "ENEMY WON!";
                winText.color = _enemyColor;
                break;
        }
      
        Invoke("ExitGame", 2f);
    }
    private void DisableWinText() => _winTextObject.gameObject.SetActive(false);
    
}
