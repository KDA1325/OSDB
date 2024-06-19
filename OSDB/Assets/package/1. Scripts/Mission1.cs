using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Mission1 : MonoBehaviour
{
    public Text inputText, KeyCode;

    Animator anim; // 선언
    PlayerController playerController;

    MissionController missionController;

    void Start()
    {
        anim = GetComponentInChildren<Animator>(); // 정의, 애니메이션이 자식으로 있기 때문에 GetComponentInChildren 사용
        missionController = FindObjectOfType<MissionController>();
    }

    // 미션 시작
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerController = FindObjectOfType<PlayerController>(); // 스크립트를 찾아서 넣어줌, 미션은 처음부터 있지만 캐릭터는 중간에 호출되어 생기기 때문에 Start 함수가 아닌 MissionStart 함수 안에서 사용

        // 초기화
        inputText.text = ""; // 텍스트 비우기
        KeyCode.text = "";

        // 키코드 랜덤
        for (int i = 0; i < 5; i++)
        {
            KeyCode.text += Random.Range(0, 10); // 0부터 9까지의 수 중 하나 랜덤 선택, 중복 배제 X
        }
    }

    // X 버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerController.MissionEnd();
    }

    // 숫자 버튼 누르면 호출
    public void ClickNumber()
    {
        if (inputText.text.Length <= 4) // 5글자 입력 제한
        {
            inputText.text += EventSystem.current.currentSelectedGameObject.name; // EventSystem.current.currentSelectedGameObject 방금 누른 버튼을 가져옴
                                                                                  // .name -> 가져온 게임 오브젝트의 이름을 inputText 안으로 넣어줌
                                                                                  // 그냥 등호로 넣으면 하나 누를 때마다 앞에 누른 숫자가 사라지고 방금 누른 숫자남 남기 때문에 숫자를 더해주기 위해 +=로 넣어줌
        }

    }

    // 삭제 버튼 누르면 호출
    public void ClickDelete()
    {
        if (inputText.text != " ") // 글씨가 비어 있지 않을 때만 작동
        {
            inputText.text = inputText.text.Substring(0, inputText.text.Length - 1); //Substring(어디서부터, 몇 개를 사용할 것인지): 문자열 자르는 기능
            // -> 0번째 글자부터 마지막 글자를 뺀 나머지 글자만 나오게 됨
        }
    }

    // 체크 버튼 누르면 호출
    public void ClickCheck()
    {
        if (inputText.text == KeyCode.text)
        {
            MissionSuccess();
        }
    }

    // 미션 성공하면 호출
    public void MissionSuccess()
    {
        ClickCancle(); // 미션 창 내려감
        missionController.MissionSuccess(GetComponent<CircleCollider2D>());
        SceneManager.LoadScene("House");

    }
}
