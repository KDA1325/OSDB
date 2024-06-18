using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PHPGetServerInfo : MonoBehaviour
{
    public string ServerURL = "http://localhost/GetServerInfo.php";

    public void GetServerInfo(string serverName)
    {
        StartCoroutine(GetserverInfo(serverName));
    }

    IEnumerator GetserverInfo(string serverName)
    {
        WWWForm form = new WWWForm();
        form.AddField("server", serverName);

        using (UnityWebRequest www = UnityWebRequest.Post(ServerURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error:" + www.error);
            }
            else
            {

                Debug.Log(www.downloadHandler.text);

                ServerManager.instance.GetServerInfo(www.downloadHandler.text);
            }
        }
    }
}
