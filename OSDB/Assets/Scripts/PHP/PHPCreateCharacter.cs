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

    // 닉네임 중복 검사
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

                // 중복된 닉네임이라면
                if(response == "DUPLICATE")
                {
                    // 중복된 닉네임 팝업 UI 활성화 
                    GameManager.instance.DuplicateNamePopUpUI.SetActive(true);
                }
                else
                {
                    // 중복된 닉네임이 아니라면 캐릭터 생성 
                    StartCoroutine(Createcharacter(nickName, _userID, _serverName, _job));
                }
            }
        }
    }

    // 닉네임 중복 검사 후 캐릭터 생성
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

                // 캐릭터 생성 성공 시 성공 팝업 UI 활성화
                if (response == "SUCCESS")
                {
                    GameManager.instance.CreateSuccessPopUpUI.SetActive(true);
                }
                else // 캐릭터 생성 실패 시 실패 팝업 UI 활성화
                {
                    GameManager.instance.CreateFailPopUpUI.SetActive(true);
                }
            }
        }
    }
}
