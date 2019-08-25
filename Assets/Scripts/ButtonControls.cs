using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonControls : MonoBehaviour {

    [SerializeField]
    Sprite SoundOffImg;
    Sprite SoundOnImg;

    GameObject BottomButtons;

    Image SoundButtonImage;

    GameObject TempMenus;
    Animator TempMenuAnimator;

    void Start () {
        //Sound button toggle related code
        //Replace when you have time
        TempMenus = GameObject.Find("TempMenus");
        TempMenuAnimator = TempMenus.GetComponent<Animator>();
        BottomButtons = GameObject.FindGameObjectWithTag("BottomButtons");
        SoundButtonImage = BottomButtons.transform.Find("Sound").GetComponent<Image>();
        SoundOnImg = SoundButtonImage.sprite;
	}
	
	void Update () {
		
	}
    
    public void SoundButton() {
        SoundButtonImage.sprite = (SoundButtonImage.sprite != SoundOffImg) ? SoundOffImg : SoundOnImg;        
    }

    public void ProButton() {

    }

    public void MarketButton() {
        TempMenus.transform.Find("MarketMenu").GetComponent<Image>().enabled = true;
        TempMenuAnimator.SetBool("TempMenuClicked", true);
    }

    public void LeaderboardButton() {
        TempMenus.transform.Find("LeaderboardsMenu").GetComponent<Image>().enabled = true;
        TempMenuAnimator.SetBool("TempMenuClicked", true);
    }

    public void AchievementsButton() {
        TempMenus.transform.Find("AchievementsMenu").GetComponent<Image>().enabled = true;
        TempMenuAnimator.SetBool("TempMenuClicked", true);

    }

    public void TutorialButton() {
        TempMenus.transform.Find("TutorialMenu").GetComponent<Image>().enabled = true;
        TempMenuAnimator.SetBool("TempMenuClicked", true);
    }

    public void ReturnButton() {
        TempMenuAnimator.SetBool("TempMenuClicked", false);
        StartCoroutine(WaitAndDisable());
    }

    IEnumerator WaitAndDisable() {
        yield return new WaitForSeconds(0.5f);
        var tmp = TempMenus.GetComponentsInChildren<Image>();
        foreach (var item in tmp) {
            item.enabled = false;
        }
    }

}
