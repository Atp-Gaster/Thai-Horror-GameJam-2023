using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameControl : MonoBehaviour
{
    public BattleManager battleManager;
    public Slider healthSlider_A; // Reference to the Slider UI element for Player A
    public Slider healthSlider_B; // Reference to the Slider UI element for Player B

    private float maxHealthA = 600f;
    private float maxHealthB = 600f;
    public float currentHealth_A; // Current health for Player A
    public float currentHealth_B; // Current health for Player B

    public GameObject PausePanal;
    public List<Image> SkillPicture;

    int[] PartID = { 0, 0, 0, 0 };   

    private float [] cooldownDuration = { 10f, 10f, 10f, 10f }; // Cooldown time in seconds
    private float [] cooldownTimer = { 0f , 0f , 0f , 0f };
    public Text [] cooldownText; // Reference to the Text component on the button
    public Button[] SKillPlayerA;

    public Animation Win;
    public Animation Lose;

    public void GotoNextScene()
    {
        GameObject.Find("Music Arrays").GetComponent<MusicPlayer>().StopMusic(1);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    bool CheckCD = false;
    public void CheckEnd()
    {
        float UpdateHPPlayer = battleManager.player.core.sumOfHp;
        float UpdateHPEnemy = battleManager.enemy.core.sumOfHp;

        if (UpdateHPPlayer <= 0)
        {
            Lose.Play();
            GameObject.Find("Music Arrays").GetComponent<MusicPlayer>().PlayVoice(2);
            Invoke("GotoNextScene", 3);

        }

        if (UpdateHPEnemy <= 0)
        {
            Win.Play();
            GameObject.Find("Music Arrays").GetComponent<MusicPlayer>().PlayVoice(1);
            Invoke("GotoNextScene", 3);

        }
        CheckCD = true;
    }

    private void Start()
    {
        PartID[0] = PlayerPrefs.GetInt("PlayerA_PartID_Head");
        PartID[1] = PlayerPrefs.GetInt("PlayerA_PartID_Body");
        PartID[2] = PlayerPrefs.GetInt("PlayerA_PartID_Lh");
        PartID[3] = PlayerPrefs.GetInt("PlayerA_PartID_Rh");

        Invoke("SetStatus", 1);
        GameObject.Find("Music Arrays").GetComponent<MusicPlayer>().PlayMusic(1);//battle scene
        GameObject.Find("Music Arrays").GetComponent<MusicPlayer>().PlayVoice(0);//battle scene
               
        //SKill Picture               
        switch (PartID[0])
        {
            case 0:
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Jack_SH");
                break;
            case 1:
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Wendigo_SH");
                break;
            case 2:
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Frank_SH");
                break;
            case 3:
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Dino_SH");
                break;
            case 4:
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Peetahkhon_SH");
                break;
            case 5:
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Oni_SH");
                break;
        }

        switch (PartID[1])
        {
            case 0:
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Jack_SB");
                break;
            case 1:
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Wendigo_SB");
                break;
            case 2:
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Frank_SB");
                break;
            case 3:
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Dino_SB");
                break;
            case 4:
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Peetahkhon_SB");
                break;
            case 5:
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Oni_SB");
                break;
        }

        switch (PartID[2])
        {
            case 0:
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Jack_SA");
                break;
            case 1:
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Wendigo_SA");
                break;
            case 2:
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Frank_SA");
                break;
            case 3:
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Dino_SA");
                break;
            case 4:
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Peetahkhon_SA");
                break;
            case 5:
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Oni_SA");
                break;
        }

        switch (PartID[3])
        {
            case 0:
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Jack_SA");
                break;
            case 1:
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Wendigo_SA");
                break;
            case 2:
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Frank_SA");
                break;
            case 3:
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Dino_SA");
                break;
            case 4:
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Peetahkhon_SA");
                break;
            case 5:
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Oni_SA");
                break;
        }

    }
    void SetStatus()
    {
        //Setup the Slider
        currentHealth_A = battleManager.player.core.sumOfMaxHp;
        currentHealth_B = battleManager.enemy.core.sumOfMaxHp;
        maxHealthA = battleManager.player.core.sumOfMaxHp;
        maxHealthB = battleManager.enemy.core.sumOfMaxHp;
        healthSlider_A.maxValue = battleManager.player.core.sumOfMaxHp;
        healthSlider_B.maxValue = battleManager.enemy.core.sumOfMaxHp;
        healthSlider_A.value = battleManager.player.core.sumOfMaxHp;
        healthSlider_B.value = battleManager.enemy.core.sumOfMaxHp;
        //Setup the CoolDown button
        cooldownDuration[0] = battleManager.player.getParts(0).attackCD.remainTime; //body

        //if(battleManager.player.core.AllAliveParts.Count <= 3) //play sound B

    }

    public void TakeDamage(float damage, int player)
    {
        if (player == 1)
        {
            currentHealth_A -= damage;
            currentHealth_A = Mathf.Clamp(currentHealth_A, 0, maxHealthA);

            // Start a coroutine to animate the slider value decrease for Player A
            StartCoroutine(AnimateHealthDecrease(healthSlider_A, currentHealth_A));
        }
        else if (player == 2)
        {
            currentHealth_B -= damage;
            currentHealth_B = Mathf.Clamp(currentHealth_B, 0, maxHealthB);

            // Start a coroutine to animate the slider value decrease for Player B
            StartCoroutine(AnimateHealthDecrease(healthSlider_B, currentHealth_B));
        }
    }

    // Coroutine to animate the health decrease for a specific slider
    private IEnumerator AnimateHealthDecrease(Slider slider, float targetHealth)
    {
        float startHealth = slider.value;
        float duration = 0.5f; // Adjust the duration of the animation

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            slider.value = Mathf.Lerp(startHealth, targetHealth, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        slider.value = targetHealth; // Ensure the final value is set correctly
    }

    void Update()
    {
        for(int i = 0; i < cooldownTimer.Length; i++)
        if (cooldownTimer[i] > 0)
        {
            cooldownTimer[i] -= Time.deltaTime;
            cooldownText[i].text = Mathf.Ceil(cooldownTimer[i]).ToString(); // Display the remaining time as an integer

        }
        else
        {
            cooldownTimer[i] = 0;
            cooldownText[i].text = "Ready"; // Skill is ready
            SKillPlayerA[i].interactable = true;
        }

        float UpdateHPPlayer = battleManager.player.core.sumOfHp;
        float UpdateHPEnemy = battleManager.enemy.core.sumOfHp;

        //Set HP
        if (currentHealth_A > UpdateHPPlayer)
        {
            float Damage = currentHealth_A - UpdateHPPlayer;
            Debug.Log(Damage);
            TakeDamage(Damage, 1);

        }

        if (currentHealth_B > UpdateHPEnemy)
        {
            float Damage = currentHealth_B - UpdateHPEnemy;
            Debug.Log(Damage);
            TakeDamage(Damage, 2);
        }        

        //SKill section;
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            OnButtonSkillClick(1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnButtonSkillClick(2);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            OnButtonSkillClick(0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnButtonSkillClick(3);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PausePanal.SetActive(true);            
        }

        if(!CheckCD) Invoke("CheckEnd", 1);
    }

    public void OnButtonSkillClick(int SkillID)
    {
        if (cooldownTimer[SkillID] <= 0)
        {
            // Perform the skill action here
            // Set cooldownTimer to the cooldown duration to start the cooldown
            cooldownTimer[SkillID] = cooldownDuration[SkillID];
            SKillPlayerA[SkillID].interactable = false;
            GameObject.Find("Music Arrays").GetComponent<MusicPlayer>().PlayMusic(0);
        }
    }
}
