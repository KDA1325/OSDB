using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Networking;

public class PHPCreateCharacter : MonoBehaviour
{
    public string CheckServerURL = "http://localhost/CheckDuplicate.php";
    public string CreateCharacterServerURL = "http://localhost/CreateCharacter.php";

    public string _nickName;
    public string _userID;
    public string _serverName;
    public string _job;

    public void CreateCharacter(string nickName, string userID, string serverName, string job)
    {
        _nickName = nickName; 
        _userID = userID;
        _serverName = serverName;
        _job = job;
       
        StartCoroutine(CheckDuplicate(_nickName));
    }

    // �г��� �ߺ� �˻�
    IEnumerator CheckDuplicate(string nickName)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickName", nickName);

        using (UnityWebRequest www = UnityWebRequest.Post(CheckServerURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error:" + www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                // �ߺ��� �г����̶��
                if(response == "DUPLICATE")
                {
                    // �ߺ��� �г��� �˾� UI Ȱ��ȭ 
                    GameManager.instance.DuplicateNamePopUpUI.SetActive(true);
                }
                else
                {
                    // �ߺ��� �г����� �ƴ϶�� ĳ���� ���� 
                    StartCoroutine(Createcharacter(nickName, _userID, _serverName, _job));
                }
            }
        }
    }

    // �г��� �ߺ� �˻� �� ĳ���� ����
    IEnumerator Createcharacter(string nickName, string userID, string serverName, string job)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickName", nickName);
        form.AddField("userID", userID);
        form.AddField("serverName", serverName);
        form.AddField("jobName", job);

        using (UnityWebRequest www = UnityWebRequest.Post(CreateCharacterServerURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error:" + www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                // ĳ���� ���� ���� �� ���� �˾� UI Ȱ��ȭ
                if (response == "SUCCESS")
                {
                    GameManager.instance.CreateSuccessPopUpUI.SetActive(true);
                }
                else // ĳ���� ���� ���� �� ���� �˾� UI Ȱ��ȭ
                {
                    GameManager.instance.CreateFailPopUpUI.SetActive(true);
                }
            }
        }
    }
}
