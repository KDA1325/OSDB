using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PHPSelectServer : MonoBehaviour
{
    public string ServerURL = "http://localhost/SelectServer.php";

    public void SelectServer(string id, string serverName)
    {
        StartCoroutine(Selectserver(id, serverName));    
    }

    IEnumerator Selectserver(string id, string serverName)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("server", serverName);

        using (UnityWebRequest www = UnityWebRequest.Post(ServerURL, form))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error:"+www.error);
            }
            else
            {
                
                Debug.Log(www.downloadHandler.text);

                ServerManager.instance.DisplayCharacterSelection(www.downloadHandler.text);
            }
        }
    }
}
