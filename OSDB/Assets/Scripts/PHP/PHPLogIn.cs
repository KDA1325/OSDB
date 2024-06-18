using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PHPLogIn : MonoBehaviour
{
    public string loginURL = "http://localhost/Login.php";
    public bool isLoggedIn = false;


    public void LogIn(string id, string pass)
    {
        StartCoroutine(Login(id, pass));    
    }

    IEnumerator Login(string id, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("pass", pass);

        using (UnityWebRequest www = UnityWebRequest.Post(loginURL, form))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error:"+www.error);
                isLoggedIn = false;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == id)
                    isLoggedIn = true;
                else
                    isLoggedIn = false;
            }

            GameManager.instance.LogInResults(isLoggedIn);
        }

    }
}
