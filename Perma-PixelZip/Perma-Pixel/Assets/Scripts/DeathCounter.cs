using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathCounter : MonoBehaviour
{
    public Animator backgroundAnim; //this is the red background
    public Animator bloodPourAnim; // this is the blood pouring animation

    public GameObject container; //this is the container object for all the objects
    public GameObject bloodPour; //this is the blood pouring animation's game object

    public AudioSource[] gongSounds; //there are 5 gong sounds, they are neater in an array
    public int deathCount; //this is for the gong sounds

    public TextMeshProUGUI livesLeft; //this is the text that is only the number of lives left
    public TextMeshProUGUI grammar;
    public int numberofLives; //this is a tracker for that

    public GameObject[] hearts;
    public Sprite emptyHeart;

    public Animator youDiedAnim;
    public GameObject replayButton;
    public GameObject youDiedObj;
    public PlayerMovement player;

    public GameObject PascalMasterJamz;

    public PlayerHealth healthScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        replayButton.SetActive(false);
        youDiedObj.SetActive(false);
        deathCount = 0; //we havent died yet
        numberofLives = 4; //you have 5 lives
        livesLeft.SetText((numberofLives).ToString()); //set the text to match the number of lives left
    }

    public void CallOnDeath(GameObject g) //this method can be called when the player dies
    {
        StartCoroutine(DeathWrapper(false));
        hearts[(numberofLives)].GetComponent<Image>().sprite = emptyHeart; //delete a heart from the HUD
        deathCount++; // update the death count to reflect all of this next time
        numberofLives--; //you now has less lives
    }

    public void FirstTimeAround()
    {
        StartCoroutine(DeathWrapper(true));
    }

    IEnumerator DeathWrapper(bool firstRun)
    {
        PascalMasterJamz.SetActive(false);

        if (deathCount < 4) //to make sure we arent fully dead
        {
            if (firstRun == false) //dont do this on first run
            {
                yield return new WaitForSeconds(2); //wait for dramatic impact
            }

            if (numberofLives+1 == 1) { grammar.SetText("life"); } //fix the grammar on the "lives left part"

            if (firstRun) { yield return new WaitForSeconds(.8f); }
            
            livesLeft.SetText((numberofLives+1).ToString()); //update the canvas life count. the plus one is to account for the fact that the variable is used for array

            container.SetActive(true); //set the whole canvas game object to true

            bloodPourAnim.Play("Blood Pour"); //play the blood pouring animation
            backgroundAnim.Play("BackgroundFadeIn"); //fade in the background at the same time
            gongSounds[deathCount].Play(); //play whatever gong sound goes along with this

            yield return new WaitForSeconds(gongSounds[deathCount].clip.length-4);// wait for gong sounds to play
            
            //reset player here
            yield return new WaitForSeconds(5);

            PascalMasterJamz.SetActive(true);
            backgroundAnim.Play("BackgroundFadeOut");
            player.ResetPlayer(deathCount != 0);
            foreach (GameObject monsterRoom in GameObject.FindGameObjectsWithTag("EnemyRoom"))
            {
                monsterRoom.GetComponent<MonsterRoom>().Reset();
                healthScript.trackHealth = true;
            }
            yield return new WaitForSeconds(.5f);
            player.GainPlayerControl();
            container.SetActive(false);
        }
        else //youre dead fr now
        {
            livesLeft.SetText("No"); //no for "no lives left"
            grammar.SetText("lives"); 

            container.SetActive(true); //set the whole canvas game object to true
            bloodPourAnim.Play("Blood Pour"); //play the blood pouring animation
            backgroundAnim.Play("BackgroundFadeIn"); //fade in the background at the same time
            gongSounds[4].Play(); //play whatever gong sound goes along with this
            yield return new WaitForSeconds(2);
            youDiedObj.SetActive(true);
            youDiedAnim.Play("YouDiedText");
            yield return new WaitForSeconds(2.5f);
            replayButton.SetActive(true);
            Animator replayAnim = replayButton.GetComponent<Animator>();
            replayAnim.Play("ReplayButtonFadeIn");
        }
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene(0);
    }

}
