using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartSequence : MonoBehaviour
{
    public GameObject button;
    public Sprite deathSprite;
    public TextMeshProUGUI buttonText;

    public TextMeshProUGUI textObject;
    string text;

    public DeathCounter deathScript;

    public GameObject PascalMasterJamz;

    public PlayerMovement player;


    // Start is called before the first frame update
    void Start()
    {
        player.LosePlayerControl();
        button.SetActive(false);
        text = textObject.text;
        textObject.SetText("");
        deathScript.container.SetActive(false);
        StartCoroutine(PlayText());
        PascalMasterJamz.SetActive(false);
    }

    IEnumerator PlayText()
    {
        foreach (char c in text)
        {
            textObject.SetText(textObject.text += c);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1);
        button.SetActive(true);
    }

    public void StartGameButton()
    {
        button.GetComponent<Image>().sprite = deathSprite;
        buttonText.SetText("Good Luck!");
        deathScript.FirstTimeAround();
        StartCoroutine(TurnOffCanvas());
    }

    IEnumerator TurnOffCanvas()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

}
