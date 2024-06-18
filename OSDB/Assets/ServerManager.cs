using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
    public static ServerManager instance;

    CharacterSlotManager characterSlotManager;
    PHPSelectServer phpSelectServer;
    PHPSelectCharacter phpSelectCharacter;

    string id;
    string serverName;
    public string nickName;
    public string job;
    public string population;
    public string _level;
    public string _hp;
    public string _mp;
    public string _str;
    public string _int;
    public string _dex;
    public string _luk;

    public Text serverNameText;
    public Text populationText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            phpSelectServer = GetComponent<PHPSelectServer>();
            phpSelectCharacter = GetComponent<PHPSelectCharacter>();
            characterSlotManager = GetComponent<CharacterSlotManager>();
        }
        else
        {
            Debug.LogWarning("씬에 두 개 이상의 서버 매니저가 존재합니다!");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
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

            GameManager.instance.SelectedServerSelectedServer();
        }
        else
        {
            Debug.Log("Null");
        }
    }

    public void DisplayCharacterSelection(string response)
    {
        string[] characters = response.Split(';');
        for (int i = 0; i < GameManager.instance.characterSlots.Length; i++)
        {
            if (i < characters.Length - 1)
            {
                string[] info = characters[i].Split(',');
                nickName = info[0];
                job = info[1];
                population = info[2];

                characterSlotManager = GameManager.instance.characterSlots[i].GetComponent<CharacterSlotManager>();

                characterSlotManager.SetCharacter(nickName, job);

                GameManager.instance.characterSlots[i].SetActive(true);
            }
            else
            {
                GameManager.instance.characterSlots[i].SetActive(false);
            }
        }

        DisplayServerInfo();
        GameManager.instance.selectCharacterUI.SetActive(true);
    }
    public void DisplayCharacterStat(string response)
    {
        string[] characters = response.Split(';');
        for (int i = 0; i < GameManager.instance.characterSlots.Length; i++)
        {
            if (i < characters.Length - 1)
            {
                string[] stat = characters[i].Split(',');
                _level = stat[0];
                _hp = stat[1];
                _mp = stat[2];
                _str = stat[3];
                _int = stat[4];
                _dex = stat[5];
                _luk = stat[6];

                //characterSlotManager = GameManager.instance.characterSlots[i].GetComponent<CharacterSlotManager>();

                //characterSlotManager.SetCharacter(nickName, job);

                //GameManager.instance.characterSlots[i].SetActive(true);
            }
            else
            {
                GameManager.instance.characterSlots[i].SetActive(false);
            }
        }

        DisplayServerInfo();
        GameManager.instance.selectCharacterUI.SetActive(true);
    }


    public void DisplayServerInfo()
    {
        serverNameText.text = $"서버: {serverName}";
        Debug.Log("서버 텍스트 갱신");

        populationText.text = $"인구 수: {population}";
        Debug.Log("인구 수 텍스트 갱신");
    }

    public void SelectCharacter()
    {
        nickName = characterSlotManager.nickNameText.text;

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
