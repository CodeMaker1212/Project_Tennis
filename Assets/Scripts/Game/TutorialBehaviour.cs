using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialBehaviour : MonoBehaviour
{
    public enum SwipeExample { Direct = 1, Left, Right, UpTouch }

    private GameObject _hand;
    private Animator _handAnimator;
    private GameBehaviour _gameBehaviour;
    private GameObject _background;
    [SerializeField] private GameObject[] _exampleTexts;

    public static bool isIncluded { get; set; } = false;
    public static bool isCompleted { get; set; } = false;


    private void ShowSwipeExample(SwipeExample swipeType)
    {
        _handAnimator.SetFloat("swipe_index", (int)swipeType);
        _exampleTexts[(int)swipeType -1].gameObject.SetActive(true);

        if(swipeType != SwipeExample.Direct)
        _exampleTexts[(int)swipeType -2].gameObject.SetActive(false);
    }
    
    private IEnumerator ShowTutorial(SwipeExample swipeType, float waitingTime)
    {    
        for (int example = 1; example < 5; example++)
        {
            if (example == 4)
                waitingTime *= 2f;
               
            ShowSwipeExample((SwipeExample)example);
            yield return new WaitForSeconds(waitingTime);    
        }

       
        CompleteTutorial();
    }    
   
    private void CompleteTutorial()
    {
        isIncluded = false;
        isCompleted = true;

        _hand.gameObject.SetActive(false);
        _background.gameObject.SetActive(false);

        _handAnimator.gameObject.SetActive(false);
        _exampleTexts[3].gameObject.SetActive(false);
    }
    
    private void Awake()
    {
        _hand = GameObject.Find("Hand");
        _handAnimator = _hand.GetComponent<Animator>();
        _gameBehaviour = GameObject.Find("Game Behaviour").GetComponent<GameBehaviour>();
        _background = GameObject.Find("Tutorial_Background");

        _hand.gameObject.SetActive(false);
        _background.gameObject.SetActive(false);


        if(MainManager.Instance.tutorialEnabled == true) 
        {
            isIncluded = true;
            isCompleted = false;
        }
        
    }
    private void Start()
    {
        if(isIncluded == true && isCompleted == false)
        {
            _hand.gameObject.SetActive(true);
            _background.gameObject.SetActive(true);
            StartCoroutine(ShowTutorial(SwipeExample.Direct, 3f));
        }
        
    }

    private void OnDisable()
    {
        MainManager.Instance.tutorialEnabled = false;
        isIncluded = false;
        isCompleted = true;
    }
}
