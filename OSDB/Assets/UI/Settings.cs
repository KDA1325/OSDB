using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Image quit;
    public Color blue; // 색상 코드로 색을 넣기 위해 변수 생성, Inspector에서 색 지정
    public PlayerController playerController;
    //public IntroManager introManager;

    //GameObject mainView, playView; // ClickQuit 화면 전환을 위해 선언

    private void Start()
    {
    }

    // 설정 버튼을 누르면 호출
    public void ClickSetting()
    {
        gameObject.SetActive(true); // 설정 화면 띄우기
        playerController.isCantMove = true;
    }

    // 게임으로 돌아가기 버튼을 누르면 호출
    public void ClickBack()
    {
        gameObject.SetActive(false);
        playerController.isCantMove = false;
    }

    // 터치 이동을 누르면 호출
    public void ClickTouch()
    {
        quit.color = blue;
    }

    // 게임 나가기 버튼을 누르면 호출
    public void ClickQuit()
    {
        //print("게임 종료 버튼 누름");
        // 유니티 에디터
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // 안드로이드
#else // 유니티 에디터가 아니라면
Application.Quit();
#endif //if문 끝내기
    }
}
