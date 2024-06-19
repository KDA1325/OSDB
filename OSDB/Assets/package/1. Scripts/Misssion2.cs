using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Misssion2 : MonoBehaviour
{
    public Color black;
    public Image[] images;

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
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white; // ��� ��ư�� �Ͼ������ �ʱ�ȭ �Ǿ��ٰ� ���� ��ư ����
        }

        // ����
        for (int i = 0; i < 5; i++)
        {
            int rand = Random.Range(0, 10); //Range�� ù��° �� ����, ������ �� ������. -> 0���� 6���� �� �ϳ��� �������� rand�� ��, �ߺ� ��� X => �ּ� 1��, �ִ� 4�� ������

            images[rand].color = black; // ������ ��ư�� �� ����
        }
    }

    // X ��ư ������ ȣ��
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerController.MissionEnd();
    }

    // ������ ��ư ������ ȣ��
    public void ClickButton()
    {
        Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>(); // ��� Ŭ���� ���� ������Ʈ ���� ������, GetComponent�� �̹��� �����ͼ� img ��� �̸����� ����

        // �Ͼ��
        if (img.color == Color.white)
        {
            // ������
            img.color = black;
        }
        // ��
        else
        {
            // �Ͼ������
            img.color = Color.white;
        }

        // ���� ���� üũ
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
            // ����
            Invoke("MissionSuccess", 0.2f); // Invoke("�Լ� �̸�", ������ �ð�); �Լ� ȣ�� ���
        }
    }

    // �̼� �����ϸ� ȣ��
    public void MissionSuccess()
    {
        ClickCancle(); // �̼� â ������
        missionController.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
