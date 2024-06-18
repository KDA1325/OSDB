using UnityEngine;
using UnityEngine.UI;

public class CharacterSlotManager : MonoBehaviour
{
    private Image characterImage;
    private SpriteRenderer spriteRenderer;
    public Sprite[] characterSprites;
    private Sprite loadSprite;
    private Text nickNameText;
    //private string selectedGunName;

    // Start is called before the first frame update
    void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        characterImage = transform.Find("CharacterImage").GetComponentInChildren<Image>();
        nickNameText = transform.Find("NickName").GetComponentInChildren<Text>();
    }

    public void SetCharacter(string nickName, string jobName)
    {
        switch (jobName)
        {
            case "아크":
                loadSprite = characterSprites[0];
                //spriteRenderer.sprite = loadSprite;
                //Debug.Log("game play " + selectedGunName);
                break;
            case "에반":
                loadSprite = characterSprites[1];
                //spriteRenderer.sprite = loadSprite;
                //Debug.Log("game play " + selectedGunName);
                break;
            case "카이저":
                loadSprite = characterSprites[2];
                //spriteRenderer.sprite = loadSprite;
                //Debug.Log("game play " + selectedGunName);
                break;
            case "아델":
                loadSprite = characterSprites[3];
                //spriteRenderer.sprite = loadSprite;
                //Debug.Log("game play " + selectedGunName);
                break;
            case "제로":
                loadSprite = characterSprites[4];
                //spriteRenderer.sprite = loadSprite;
                //Debug.Log("game play " + selectedGunName);
                break;
        }

        if (loadSprite != null) 
        { 
            characterImage.sprite = loadSprite;
        }
        nickNameText.text = nickName;
    }

        //public void changeCharacterSprite(string jobName)
        //{
        //    //GameManager gameManager = GameManager.Instance;

        //    //selectedGunName = gameManager.selectedGunName;
        //    //selectedSprite = null;

        //    //switch (selectedGunName)
        //    //{
        //    //    case "M4A1":
        //    //        selectedSprite = gunSprites[0];
        //    //        spriteRenderer.sprite = selectedSprite;
        //    //        Debug.Log("game play " + selectedGunName);
        //    //        break;
        //    //    case "WA2000":
        //    //        selectedSprite = gunSprites[1];
        //    //        spriteRenderer.sprite = selectedSprite;
        //    //        Debug.Log("game play " + selectedGunName);
        //    //        break;
        //    //    case "G41":
        //    //        selectedSprite = gunSprites[2];
        //    //        spriteRenderer.sprite = selectedSprite;
        //    //        Debug.Log("game play " + selectedGunName);
        //    //        break;
        //    //}
        //    switch (jobName)
        //    {
        //        case "아크":
        //            loadSprite = characterSprites[0];
        //            spriteRenderer.sprite = loadSprite;
        //            //Debug.Log("game play " + selectedGunName);
        //            break;
        //        case "에반":
        //            loadSprite = characterSprites[1];
        //            spriteRenderer.sprite = loadSprite;
        //            //Debug.Log("game play " + selectedGunName);
        //            break;
        //        case "카이저":
        //            loadSprite = characterSprites[2];
        //            spriteRenderer.sprite = loadSprite;
        //            //Debug.Log("game play " + selectedGunName);
        //            break;
        //        case "아델":
        //            loadSprite = characterSprites[3];
        //            spriteRenderer.sprite = loadSprite;
        //            //Debug.Log("game play " + selectedGunName);
        //            break;
        //        case "제로":
        //            loadSprite = characterSprites[4];
        //            spriteRenderer.sprite = loadSprite;
        //            //Debug.Log("game play " + selectedGunName);
        //            break;
        //    }
        //}
    }
