using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ServerBtnManager : MonoBehaviour
{
    PHPSelectServer phpSelectServer;
    string id;
    string serverName;

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
        }
        else
        {
            Debug.Log("Null");
        }
    }
}
