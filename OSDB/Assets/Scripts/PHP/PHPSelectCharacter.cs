using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PHPSelectCharacter : MonoBehaviour
{
    public string ServerURL = "http://localhost/SelectCharacter.php";

    public void SelectCharacter(string nickName)
    {
        StartCoroutine(Selectcharacter(nickName));
    }

    IEnumerator Selectcharacter(string nickName)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickName", nickName);

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

                ServerManager.instance.DisplayCharacterStat(www.downloadHandler.text);
            }
        }
    }
}
