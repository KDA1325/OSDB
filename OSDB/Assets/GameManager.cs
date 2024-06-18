using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public bool isGameover = false;
    //public Text scoreText;
    //public GameObject gameoverUI;

    //DBController dBController;
    PHPLogIn phpLogIn;
    //ServerManager serverManager;

    public bool isLoggIn = false;
    string userID;
    //public Button serverBtn;
    public Text idText;
    public InputField passInputField; //패그워드가 설정된 텍스트는 텍스트UI 가 아니라 상위의 인풋 필드에 저장이 됨
    //ArrayList gunList = new ArrayList();

    private int score = 0;

    public GameObject signInUI;
    public GameObject serverUI;
    public GameObject selectCharacterUI;
    public GameObject characterStatUI;
    
    public GameObject[] characterSlots;

    public GameObject signOutBtn;
    //public Dictionary<string, Sprite> characterSprites;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //dBController = GetComponent<DBController>();
            phpLogIn = GetComponent<PHPLogIn>();
            //serverManager = GetComponent<ServerManager>();
            //characterSlotManager = GetComponent<CharacterSlotManager>();
        }
        else
        {
            Debug.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void LogIn()
    {
        phpLogIn.LogIn(idText.text, passInputField.text);
    }

    public void LogInResults(bool result)
    { 

        if (phpLogIn.isLoggedIn && result)
        {
            Debug.Log("로그인이 완료 되었습니다!");
            isLoggIn = true;
            userID = idText.text;
            signInUI.SetActive(false);
            serverUI.SetActive(true);
            // gunList = dBController.SelectGuns(idText.text);
            // siginUI.SetActive(false);
            // selectUI.SetActive(true);
        }
        else
        {
            Debug.Log("로그인 실패! 아이디와 비밀번호를 확인 하세요!");
        }
    }
    public void SelectedServer()
    {
        serverUI.SetActive(false);
        selectCharacterUI.SetActive(true);
    }

    public void ClickedSignOutBtn()
    {
        serverUI.SetActive(false);
        signInUI.SetActive(true);
    }
    public static GameManager Instance
    {
        get 
        { 
            if(!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (instance == null)
                    Debug.Log("no singleton obj");
            }

            return instance;
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        //if(isGameover && Input.GetMouseButtonDown(0))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
    }

    //public void AddScore(int newScore)
    //{
    //    if(!isGameover)
    //    {
    //        score += newScore;
    //        scoreText.text = "Score :" + score;
    //    }

    //}

    //public void OnPlayerDead()
    //{
    //    isGameover = true;
    //    gameoverUI.SetActive(true);
    //}

    //public void StartGame(string id)
    //{
    //    SceneManager.LoadScene("SampleScene");
    //}

}
