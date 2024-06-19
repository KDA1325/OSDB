using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Image quit;
    public Color blue; // ���� �ڵ�� ���� �ֱ� ���� ���� ����, Inspector���� �� ����
    public PlayerController playerController;
    //public IntroManager introManager;

    //GameObject mainView, playView; // ClickQuit ȭ�� ��ȯ�� ���� ����

    private void Start()
    {
    }

    // ���� ��ư�� ������ ȣ��
    public void ClickSetting()
    {
        gameObject.SetActive(true); // ���� ȭ�� ����
        playerController.isCantMove = true;
    }

    // �������� ���ư��� ��ư�� ������ ȣ��
    public void ClickBack()
    {
        gameObject.SetActive(false);
        playerController.isCantMove = false;
    }

    // ��ġ �̵��� ������ ȣ��
    public void ClickTouch()
    {
        quit.color = blue;
    }

    // ���� ������ ��ư�� ������ ȣ��
    public void ClickQuit()
    {
        //print("���� ���� ��ư ����");
        // ����Ƽ ������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // �ȵ���̵�
#else // ����Ƽ �����Ͱ� �ƴ϶��
Application.Quit();
#endif //if�� ������
    }
}
