using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Misssion2 : MonoBehaviour
{
    public Color black;
    public Image[] images;

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
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white; // 모든 버튼이 하얀색으로 초기화 되었다가 랜덤 버튼 생성
        }

        // 랜덤
        for (int i = 0; i < 5; i++)
        {
            int rand = Random.Range(0, 10); //Range는 첫번째 수 포함, 마지막 수 미포함. -> 0부터 6까지 중 하나가 랜덤으로 rand에 들어감, 중복 고려 X => 최소 1개, 최대 4개 빨간색

            images[rand].color = black; // 랜덤한 버튼의 색 지정
        }
    }

    // X 버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerController.MissionEnd();
    }

    // 육각형 버튼 누르면 호출
    public void ClickButton()
    {
        Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>(); // 방금 클릭한 게임 오브젝트 정보 가져옴, GetComponent로 이미지 가져와서 img 라는 이름으로 저장

        // 하얀색
        if (img.color == Color.white)
        {
            // 색으로
            img.color = black;
        }
        // 색
        else
        {
            // 하얀색으로
            img.color = Color.white;
        }

        // 성공 여부 체크
        int count = 0;
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].color == Color.white)
            {
                count++;
            }
        }

        if (count == images.Length)
        {
            // 성공
            Invoke("MissionSuccess", 0.2f); // Invoke("함수 이름", 딜레이 시간); 함수 호출 기능
        }
    }

    // 미션 성공하면 호출
    public void MissionSuccess()
    {
        ClickCancle(); // 미션 창 내려감
        missionController.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
