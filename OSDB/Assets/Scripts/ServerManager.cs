using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
    public static ServerManager instance;

    CharacterSlotManager characterSlotManager;
    PHPSelectServer phpSelectServer;
    PHPSelectCharacter phpSelectCharacter;
    PHPGetServerInfo phpGetServerInfo;

    string id;
    public string serverName;
    public string nickName;
    public string population;
    public string _level;
    public string _job;
    public string _hp;
    public string _mp;
    public string _str;
    public string _int;
    public string _dex;
    public string _luk;

    public Text serverNameText;
    public Text populationText;
    public Text levelText;
    public Text nickNameText;
    public Text jobText;
    //public Text hpText;
    //public Text mpText;
    public Text strText;
    public Text intText;
    public Text dexText;
    public Text lukText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            phpSelectServer = GetComponent<PHPSelectServer>();
            phpSelectCharacter = GetComponent<PHPSelectCharacter>();
            phpGetServerInfo = GetComponent<PHPGetServerInfo>();
            characterSlotManager = GetComponent<CharacterSlotManager>();
        }
        else
        {
            Debug.LogWarning("씬에 두 개 이상의 서버 매니저가 존재합니다!");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // 서버 선택
    public void SelectServer()
    {
        GameObject serverBtn = EventSystem.current.currentSelectedGameObject;
        if (serverBtn != null)
        {
            id = GameManager.instance.idText.text;
            serverName = serverBtn.GetComponentInChildren<Text>().text;
            Debug.Log(id);
            Debug.Log(serverName);
            phpSelectServer.SelectServer(id, serverName);
            phpGetServerInfo.GetServerInfo(serverName);
            GameManager.instance.SelectedServer();
        }
        else
        {
            Debug.Log("Null");
        }
    }

    // 서버 정보 불러옴
    public void GetServerInfo(string response)
    {
        string[] infos = response.Split(';');
        population = infos[0]; 
      
        // 서버 정보를 UI에 갱신
        SetServerInfo();
    }

    // 캐릭터 선택
    public void SelectCharacter()
    {
        GameObject characterBtn = EventSystem.current.currentSelectedGameObject;
        if (characterBtn != null)
        {
            GameManager.instance.characterStatUI.SetActive(false);
            
            nickName = characterBtn.GetComponentInChildren<Text>().text;
            
            Debug.Log(nickName);

            phpSelectCharacter.SelectCharacter(nickName);
        }
        else
        {
            Debug.Log("Null");
        }
    }

    // 유저가 보유한 캐릭터만 보여줌
    public void DisplayCharacterSelection(string response)
    {

        string[] characters = response.Split(';');

        for (int i = 0; i < GameManager.instance.characterSlots.Length; i++)
        {
            if (i < characters.Length - 1)
            {
                string[] info = characters[i].Split(',');
                nickName = info[0];
                _job = info[1];

                characterSlotManager = GameManager.instance.characterSlots[i].GetComponent<CharacterSlotManager>();

                characterSlotManager.SetCharacter(nickName, _job);

                GameManager.instance.characterSlots[i].SetActive(true);
            }
            else
            {
                GameManager.instance.characterSlots[i].SetActive(false);
            }
        }

        // 현재 서버에 보유한 캐릭터가 없다면
        if (response == "NO_CHARACTER")
        {
            // 팝업 UI 활성화
            GameManager.instance.popUpUI.SetActive(true);
            return;
        }

        GameManager.instance.selectCharacterUI.SetActive(true);
        GameManager.instance.CreateCharacterBtn.SetActive(true);
    }

    // 선택한 캐릭터의 스탯을 보여줌
    public void DisplayCharacterStat(string response)
    {
        string[] stats = response.Split(';');
        for (int i = 0; i < GameManager.instance.characterSlots.Length; i++)
        {
            if (i < stats.Length - 1)
            {
                string[] stat = stats[i].Split(',');
                _level = stat[0];
                _job = stat[1];
                _hp = stat[2];
                _mp = stat[3];
                _str = stat[4];
                _int = stat[5];
                _dex = stat[6];
                _luk = stat[7];

                GameManager.instance.characterStatUI.SetActive(true);
            }
            else
            {
                GameManager.instance.characterStatUI.SetActive(false);
            }
        }

        SetCharacterStat();
        GameManager.instance.characterStatUI.SetActive(true);
        GameManager.instance.CreateCharacterBtn.SetActive(false);
    }

    // 서버 정보를 UI에 갱신
    public void SetServerInfo()
    {
        serverNameText.text = $"서버: {serverName}";
        Debug.Log("서버 텍스트 갱신");

        populationText.text = $"인구 수: {population}";
        Debug.Log("인구 수 텍스트 갱신");
    }

    // 캐릭터 스탯을 UI에 갱신
    public void SetCharacterStat()
    {
        levelText.text = $"Lv. {_level}";
        nickNameText.text = nickName;
        jobText.text = _job;
        strText.text = $"STR: {_str}";
        intText.text = $"INT: {_int}";
        dexText.text = $"DEX: {_dex}";
        lukText.text = $"LUK: {_luk}";

        Debug.Log("스탯 갱신");
    }

    // 생성할 캐릭터 직업 선택 
    public void SelectCreateJob()
    {
        GameObject characterBtn = EventSystem.current.currentSelectedGameObject;
        if (characterBtn != null)
        {
            _job = characterBtn.GetComponentInChildren<Text>().text;

            Debug.Log(_job + "선택");

            GameManager.instance.CheckPopUpUI.SetActive(true);
            GameManager.instance.CheckCreateJob(_job);
        }
        else
        {
            Debug.Log("Null");
        }
    }

    public static ServerManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(ServerManager)) as ServerManager;

                if (instance == null)
                    Debug.Log("no singleton obj");
            }

            return instance;
        }
    }
}
