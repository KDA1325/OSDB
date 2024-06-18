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
            Debug.LogWarning("���� �� �� �̻��� ���� �Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // ���� ����
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

    // ���� ���� �ҷ���
    public void GetServerInfo(string response)
    {
        string[] infos = response.Split(';');
        population = infos[0]; 
      
        // ���� ������ UI�� ����
        SetServerInfo();
    }

    // ĳ���� ����
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

    // ������ ������ ĳ���͸� ������
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

        // ���� ������ ������ ĳ���Ͱ� ���ٸ�
        if (response == "NO_CHARACTER")
        {
            // �˾� UI Ȱ��ȭ
            GameManager.instance.popUpUI.SetActive(true);
            return;
        }

        GameManager.instance.selectCharacterUI.SetActive(true);
        GameManager.instance.CreateCharacterBtn.SetActive(true);
    }

    // ������ ĳ������ ������ ������
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

    // ���� ������ UI�� ����
    public void SetServerInfo()
    {
        serverNameText.text = $"����: {serverName}";
        Debug.Log("���� �ؽ�Ʈ ����");

        populationText.text = $"�α� ��: {population}";
        Debug.Log("�α� �� �ؽ�Ʈ ����");
    }

    // ĳ���� ������ UI�� ����
    public void SetCharacterStat()
    {
        levelText.text = $"Lv. {_level}";
        nickNameText.text = nickName;
        jobText.text = _job;
        strText.text = $"STR: {_str}";
        intText.text = $"INT: {_int}";
        dexText.text = $"DEX: {_dex}";
        lukText.text = $"LUK: {_luk}";

        Debug.Log("���� ����");
    }

    // ������ ĳ���� ���� ���� 
    public void SelectCreateJob()
    {
        GameObject characterBtn = EventSystem.current.currentSelectedGameObject;
        if (characterBtn != null)
        {
            _job = characterBtn.GetComponentInChildren<Text>().text;

            Debug.Log(_job + "����");

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
