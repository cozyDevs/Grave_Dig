using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseScreen;

    bool isOpen;

    public AudioSource PascalTrack;

    // Start is called before the first frame update
    void Start()
    {
        PauseScreen.SetActive(false);
        isOpen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPauseMenu();
        }
    }
    public void OpenPauseMenu()
    {
        if (isOpen == false)
        {
            PauseScreen.SetActive(true);
            isOpen = true;
            PascalTrack.volume = 0.0f;
        }

        else if (isOpen == true)
        {
            Animator PauseAnim = PauseScreen.GetComponent<Animator>();
            PauseAnim.Play("PauseScreenFadeOut");
            isOpen = false;
            PascalTrack.volume = 1.0f;
            StartCoroutine(PauseWrapper());
        }
    }

    IEnumerator PauseWrapper()
    {
        yield return new WaitForSeconds(.12f);
        PauseScreen.SetActive(false);
    }
}
