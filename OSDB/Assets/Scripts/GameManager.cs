using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.Jobs;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    PHPLogIn phpLogIn;
    PHPCreateCharacter phpCreateCharacter;

    public bool isLoggIn = false;
    string userID;
    string _job;

    public Text idText;
    public Text checkText;
    public Text nickNameText;
    public InputField passInputField; //패스워드가 설정된 텍스트는 텍스트UI 가 아니라 상위의 인풋 필드에 저장이 됨

    public GameObject signInUI;
    public GameObject serverUI;
    public GameObject selectCharacterUI;
    public GameObject characterStatUI;
    public GameObject createCharacterUI;

    public GameObject popUpUI;
    public GameObject CheckPopUpUI;
    public GameObject CreatePopUpUI;
    public GameObject DuplicateNamePopUpUI;
    public GameObject CreateSuccessPopUpUI;
    public GameObject CreateFailPopUpUI;
     
    public GameObject[] characterSlots;

    public GameObject CreateCharacterBtn;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            phpLogIn = GetComponent<PHPLogIn>();
            phpCreateCharacter = GetComponent<PHPCreateCharacter>();

            signInUI.SetActive(true);

            serverUI.SetActive(false);
            selectCharacterUI.SetActive(false);
            characterStatUI.SetActive(false);
            createCharacterUI.SetActive(false);
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

            // 로그인 성공하면 UI 전환
            signInUI.SetActive(false);
            serverUI.SetActive(true);
        }
        else
        {
            Debug.Log("로그인 실패! 아이디와 비밀번호를 확인 하세요!");
        }
    }

    // 캐릭터 생성 시, 선택한 직업 확인
    public void CheckCreateJob(string job)
    {
        _job = job;
        checkText.text = $"{_job} 을/를 생성하시겠습니까?";
    }

    // 서버를 선택하면 UI 전환
    public void SelectedServer()
    {
        serverUI.SetActive(false);
        selectCharacterUI.SetActive(true);
        CreateCharacterBtn.SetActive(true);
    }

    // 로그아웃 버튼 클릭
    public void ClickedSignOutBtn()
    {
        serverUI.SetActive(false);
        signInUI.SetActive(true);
    }

    // 서버 선택으로 돌아가기 버튼 클릭
    public void ClickedBackSelectServerBtn()
    {
        selectCharacterUI.SetActive(false);
        characterStatUI.SetActive(false);
        serverUI.SetActive(true);
    } 
    
    // 캐릭터 선택으로 돌아가기 버튼 클릭
    public void ClickedBackSelectCharacterBtn()
    {
        createCharacterUI.SetActive(false);
        selectCharacterUI.SetActive(true);
        CreateCharacterBtn.SetActive(true);
    }

    // 팝업 닫기 버튼 클릭
    public void ClickedCancleBtn()
    {
        popUpUI.SetActive(false);
        CheckPopUpUI.SetActive(false);
        CreatePopUpUI.SetActive(false);
        DuplicateNamePopUpUI.SetActive(false);
        CreateSuccessPopUpUI.SetActive(false);
        CreateFailPopUpUI.SetActive(false);
    }

    // 캐릭터 생성 버튼 클릭
    public void ClickedCreateCharacterBtn()
    {
        selectCharacterUI.SetActive(false);
        createCharacterUI.SetActive(true);
    }
    
    // 직업 확인 팝업 생성 버튼 클릭
    public void ClickedCreateBtn()
    {
        CheckPopUpUI.SetActive(false);
        CreatePopUpUI.SetActive(true);
    }

    // 캐릭터 생성 팝업 확인 버튼 클릭
    public void ClickedCheckBtn()
    {
        CreateSuccessPopUpUI.SetActive(false);
        CreateFailPopUpUI.SetActive(false);
        createCharacterUI.SetActive(false);
        serverUI.SetActive(true);
    }

    // 닉네임 입력 후 캐릭터 생성 버튼 클릭
    public void ClickedCreateNickNameBtn()
    {
        string newNickName = nickNameText.text;

        if(newNickName != null)
        {
            string newJobName = _job;
            string serverName = ServerManager.instance.serverName;

            // php서버로 넘겨서 닉네임 중복 검사 후 DB에 데이터 추가
            phpCreateCharacter.CreateCharacter(newNickName, userID, serverName, newJobName);
        }
    }

    public void ClickedGameStartBtn()
    {
        SceneManager.LoadScene("Main");
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
}
