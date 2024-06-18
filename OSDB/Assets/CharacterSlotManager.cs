using UnityEngine;
using UnityEngine.UI;

public class CharacterSlotManager : MonoBehaviour
{
    private Image characterImage;
    public Sprite[] characterSprites;
    private Sprite loadSprite;
    public Text nickNameText;

    void Awake()
    {
        characterImage = transform.Find("CharacterImage").GetComponentInChildren<Image>();
        nickNameText = transform.Find("NickName").GetComponentInChildren<Text>();
    }

    public void SetCharacter(string nickName, string jobName)
    {
        switch (jobName)
        {
            case "��ũ":
                loadSprite = characterSprites[0];
                break;
            case "����":
                loadSprite = characterSprites[1];
                break;
            case "ī����":
                loadSprite = characterSprites[2];
                break;
            case "�Ƶ�":
                loadSprite = characterSprites[3];
                break;
            case "����":
                loadSprite = characterSprites[4];
                break;
        }

        if (loadSprite != null) 
        { 
            characterImage.sprite = loadSprite;
        }
        nickNameText.text = nickName;
    }
}
