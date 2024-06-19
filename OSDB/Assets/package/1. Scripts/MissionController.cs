using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    //public Slider guage;
    public int clear;
    public CircleCollider2D[] colls;
    //public GameObject text_anim, mainView;

    int missionCount;

    // �̼� �ʱ�ȭ
    public void MissionReset()
    {
        clear = 0;
        missionCount = 0;

        for (int i = 0; i < colls.Length; i++)
        {
            colls[i].enabled = true;
        }

        //text_anim.SetActive(false);
    }

    // �̼� �����ϸ� ȣ��
    public void MissionSuccess(CircleCollider2D coll)
    {
        missionCount++;
        clear = missionCount;
        //guage.value = missionCount / 7f;

        // ������ �̼��� �ٽ� �÷��� X
        coll.enabled = false;

        // ���� ���� üũ
        if (clear == 2)// ��� �̼� ����
        {
            //text_anim.SetActive(true);
            SceneManager.LoadScene("Clear");
            //Invoke("Change", 1f);
        }
    }

    // ȭ�� ��ȯ
    public void Change()
    {
        //mainView.SetActive(true);
        gameObject.SetActive(false);

        // ĳ���� ����
        //FindObjectOfType<PlayerController>().DestroyPlayer();
    }
}
