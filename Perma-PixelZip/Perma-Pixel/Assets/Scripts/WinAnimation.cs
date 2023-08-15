using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinAnimation : MonoBehaviour
{
    public TextMeshProUGUI endText;
    public string streeng;

    public GameObject winScreen;

    public GameObject PascalsTunez, AmbientTunez;

    public GameObject replayButton;

    // Start is called before the first frame update
    void Start()
    {
        streeng = endText.text;
        winScreen.SetActive(false);
        replayButton.SetActive(false);
        PascalsTunez.SetActive(false);
    }

    public void YouWin()
    {
        winScreen.SetActive(true);
        StartCoroutine(PlayText());
    }

    IEnumerator PlayText()
    {
        PascalsTunez.SetActive(false);
        AmbientTunez.SetActive(false);

        endText.SetText("");
        foreach (char c in streeng)
        {
            endText.SetText(endText.text += c);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(4);

        endText.SetText("");
        streeng = ("A really really really warm feeling…");

        foreach (char c in streeng)
        {
            endText.SetText(endText.text += c);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2);

        endText.SetText("");
        streeng = "Play Again?";

        foreach (char c in streeng)
        {
            endText.SetText(endText.text += c);
            yield return new WaitForSeconds(0.05f);
        }

        replayButton.SetActive(true);
    }

}
