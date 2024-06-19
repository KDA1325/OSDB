using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Mission1 : MonoBehaviour
{
    public Text inputText, KeyCode;

    Animator anim; // ����
    PlayerController playerController;

    MissionController missionController;

    void Start()
    {
        anim = GetComponentInChildren<Animator>(); // ����, �ִϸ��̼��� �ڽ����� �ֱ� ������ GetComponentInChildren ���
        missionController = FindObjectOfType<MissionController>();
    }

    // �̼� ����
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerController = FindObjectOfType<PlayerController>(); // ��ũ��Ʈ�� ã�Ƽ� �־���, �̼��� ó������ ������ ĳ���ʹ� �߰��� ȣ��Ǿ� ����� ������ Start �Լ��� �ƴ� MissionStart �Լ� �ȿ��� ���

        // �ʱ�ȭ
        inputText.text = ""; // �ؽ�Ʈ ����
        KeyCode.text = "";

        // Ű�ڵ� ����
        for (int i = 0; i < 5; i++)
        {
            KeyCode.text += Random.Range(0, 10); // 0���� 9������ �� �� �ϳ� ���� ����, �ߺ� ���� X
        }
    }

    // X ��ư ������ ȣ��
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerController.MissionEnd();
    }

    // ���� ��ư ������ ȣ��
    public void ClickNumber()
    {
        if (inputText.text.Length <= 4) // 5���� �Է� ����
        {
            inputText.text += EventSystem.current.currentSelectedGameObject.name; // EventSystem.current.currentSelectedGameObject ��� ���� ��ư�� ������
                                                                                  // .name -> ������ ���� ������Ʈ�� �̸��� inputText ������ �־���
                                                                                  // �׳� ��ȣ�� ������ �ϳ� ���� ������ �տ� ���� ���ڰ� ������� ��� ���� ���ڳ� ���� ������ ���ڸ� �����ֱ� ���� +=�� �־���
        }

    }

    // ���� ��ư ������ ȣ��
    public void ClickDelete()
    {
        if (inputText.text != " ") // �۾��� ��� ���� ���� ���� �۵�
        {
            inputText.text = inputText.text.Substring(0, inputText.text.Length - 1); //Substring(��𼭺���, �� ���� ����� ������): ���ڿ� �ڸ��� ���
            // -> 0��° ���ں��� ������ ���ڸ� �� ������ ���ڸ� ������ ��
        }
    }

    // üũ ��ư ������ ȣ��
    public void ClickCheck()
    {
        if (inputText.text == KeyCode.text)
        {
            MissionSuccess();
        }
    }

    // �̼� �����ϸ� ȣ��
    public void MissionSuccess()
    {
        ClickCancle(); // �̼� â ������
        missionController.MissionSuccess(GetComponent<CircleCollider2D>());
        SceneManager.LoadScene("House");

    }
}
