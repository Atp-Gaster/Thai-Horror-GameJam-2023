using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SelectingMenu : MonoBehaviour
{
    //Head = 0, Body = 1, L hand = 2, R hand = 3;
    public Text[] InformationText;
    public Image[] ExamplePictures;
    public Text PlayerText;
    public List<GameObject> MonsterPart = new List<GameObject>();
    public List<Image> SkillPicture;

    //Character selecting value
    [SerializeField] int[] PartID = { 0, 0, 0, 0 };
    public VideoPlayer[] DiceCG;
    public GameObject[] DiceObject;
    public void GotoNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SavePartIDPlayerA()
    {       
        PlayerPrefs.SetInt("PlayerA_PartID_Head", PartID[0]);
        PlayerPrefs.SetInt("PlayerA_PartID_Body", PartID[1]);
        PlayerPrefs.SetInt("PlayerA_PartID_Lh", PartID[2]);
        PlayerPrefs.SetInt("PlayerA_PartID_Rh", PartID[3]);
        print(PlayerPrefs.GetInt("PlayerA_PartID_Head"));
        print(PlayerPrefs.GetInt("PlayerA_PartID_Body"));
        print(PlayerPrefs.GetInt("PlayerA_PartID_Lh"));
        print(PlayerPrefs.GetInt("PlayerA_PartID_Rh"));
        Invoke("GotoNextScene", 1);
    }

    #region Button_Setup

    public void AddIndexHead()
        {
            if (PartID[0] >= 5)
            {
                PartID[0] = 0;
            }
            else PartID[0] += 1;
        }

        public void ReduceIndexHead()
        {
            if (PartID[0] <= 0)
            {
                PartID[0] = 0;
            }
            else PartID[0] -= 1;
        }

        public void AddIndexBody()
        {
            if (PartID[1] >= 5)
            {
                PartID[1] = 0;
            }
            else PartID[1] += 1;
        }

        public void ReduceIndexBody()
        {
            if (PartID[1] <= 0)
            {
                PartID[1] = 0;
            }
            else PartID[1] -= 1;
        }

        public void AddIndexLHand()
        {
            if (PartID[2] >= 5)
            {
                PartID[2] = 0;
            }
            else PartID[2] += 1;
        }

        public void ReduceIndexLHand()
        {
            if (PartID[2] <= 0)
            {
                PartID[2] = 0;
            }
            else PartID[2] -= 1;
        }

        public void AddIndexRHand()
        {
            if (PartID[3] >= 5)
            {
                PartID[3] = 0;
            }
            else PartID[3] += 1;
        }

        public void ReduceIndexRHand()
        {
            if (PartID[3] <= 0)
            {
                PartID[3] = 0;
            }
            else PartID[3] -= 1;
        }


    #endregion

    public void RandomEachpart(int PartId)
    {
        int Randompart = Random.Range(0, 5);
        print(Randompart);
        PartID[PartId] = Randompart;
        
        if(PartId == 0)
        {
            switch (Randompart)
            { 
                case 0:
                    DiceObject[0].SetActive(true);                    
                    StartCoroutine(CloseCutseen(0));
                    break;
                case 1:
                    DiceObject[1].SetActive(true);
                    StartCoroutine(CloseCutseen(1));
                    break;
                case 2:
                    DiceObject[2].SetActive(true);
                    StartCoroutine(CloseCutseen(2));
                    break;
                case 3:
                    DiceObject[3].SetActive(true);
                    StartCoroutine(CloseCutseen(3));
                    break;
                case 4:
                    DiceObject[4].SetActive(true);
                    StartCoroutine(CloseCutseen(4));
                    break;
                case 5:
                    DiceObject[5].SetActive(true);
                    StartCoroutine(CloseCutseen(5));
                    break;
            }
        }
        if (PartId == 1)
        {
            switch (Randompart)
            {
                case 0:
                    DiceObject[6].SetActive(true);
                    StartCoroutine(CloseCutseen(6));
                    break;
                case 1:
                    DiceObject[7].SetActive(true);
                    StartCoroutine(CloseCutseen(7));
                    break;
                case 2:
                    DiceObject[8].SetActive(true);
                    StartCoroutine(CloseCutseen(8));
                    break;
                case 3:
                    DiceObject[9].SetActive(true);
                    StartCoroutine(CloseCutseen(9));
                    break;
                case 4:
                    DiceObject[10].SetActive(true);
                    StartCoroutine(CloseCutseen(10));
                    break;
                case 5:
                    DiceObject[11].SetActive(true);
                    StartCoroutine(CloseCutseen(11));
                    break;
            }
        }

        if (PartId == 2)
        {
            switch (Randompart)
            {
                case 0:
                    DiceObject[12].SetActive(true);
                    StartCoroutine(CloseCutseen(12));
                    break;
                case 1:
                    DiceObject[13].SetActive(true);
                    StartCoroutine(CloseCutseen(13));
                    break;
                case 2:
                    DiceObject[14].SetActive(true);
                    StartCoroutine(CloseCutseen(14));
                    break;
                case 3:
                    DiceObject[15].SetActive(true);
                    StartCoroutine(CloseCutseen(15));
                    break;
                case 4:
                    DiceObject[16].SetActive(true);
                    StartCoroutine(CloseCutseen(16));
                    break;
                case 5:
                    DiceObject[17].SetActive(true);
                    StartCoroutine(CloseCutseen(17));
                    break;
            }
        }

        if (PartId == 3)
        {
            switch (Randompart)
            {
                case 0:
                    DiceObject[18].SetActive(true);
                    StartCoroutine(CloseCutseen(18));
                    break;
                case 1:
                    DiceObject[19].SetActive(true);
                    StartCoroutine(CloseCutseen(19));
                    break;
                case 2:
                    DiceObject[20].SetActive(true);
                    StartCoroutine(CloseCutseen(20));
                    break;
                case 3:
                    DiceObject[21].SetActive(true);
                    StartCoroutine(CloseCutseen(21));
                    break;
                case 4:
                    DiceObject[22].SetActive(true);
                    StartCoroutine(CloseCutseen(22));
                    break;
                case 5:
                    DiceObject[23].SetActive(true);
                    StartCoroutine(CloseCutseen(23));
                    break;
            }
        }
    }

    IEnumerator CloseCutseen(int CloseID)
    {        
        DiceCG[CloseID].Play();
        yield return new WaitForSeconds(9);
        DiceCG[CloseID].Stop();
        DiceObject[CloseID].SetActive(false);        
    }

    // Start is called before the first frame update
    void Start()
    {
        PartID = new int[4];
        PartID[0] = 0;
        PartID[1] = 0;
        PartID[2] = 0;
        PartID[3] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (PartID[0]) //Control Head Part
        {
            case 0:
                InformationText[0].text = "<i>Jack's Head</i> \n <size=25>•Pyro Jack : Launch Jack's head forward, deals damage over time at your mouse position</size>";
                //ExamplePictures[0].color = Color.red;
                MonsterPart[0].GetComponent<Animator>().SetInteger("PartID", 0);
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Jack_SH");
                break;
            case 1:
                InformationText[0].text = "<i>Wendigo's Head</i> \n <size=25> •Screech  : Emits howling screech that damages the enemy. </size>";
                //ExamplePictures[0].color = Color.blue;
                MonsterPart[0].GetComponent<Animator>().SetInteger("PartID", 1);
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Wendigo_SH");
                break;
            case 2:
                InformationText[0].text = "<i>Frankenstein's Head</i> \n <size=25> •Magnetic Pulse : Creates magnetic field that pulls enemy in. </size>";
                //ExamplePictures[0].color = Color.green;
                MonsterPart[0].GetComponent<Animator>().SetInteger("PartID", 2);
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Frank_SH");
                break;
            case 3:
                InformationText[0].text = "<i>Mecha Dino's Head</i> \n <size=25> •Rapid Fire : Fire off bullets rapidly at your mouse position.</size>";
                MonsterPart[0].GetComponent<Animator>().SetInteger("PartID", 3);
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Dino_SH");
                break;
            case 4:
                InformationText[0].text = "<i>Phitahkhon's Head</i> \n <size=25> •Crunch : Relentlessly bites the enemy.</size>";
                MonsterPart[0].GetComponent<Animator>().SetInteger("PartID", 4);
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Peetahkhon_SH");
                break;
            case 5:
                InformationText[0].text = "<i>Oni's Head</i> \n <size=25>•Spirit Cannon : Shoot condensed spirit energy beam.</size>";
                MonsterPart[0].GetComponent<Animator>().SetInteger("PartID", 5);
                SkillPicture[0].sprite = Resources.Load<Sprite>("Monster skill icons/Oni_SH");
                break;
        }

        switch (PartID[1]) //Control Body Part
        {
            case 0:
                InformationText[1].text = "<i>Jack's Body</i> \n <size=25> •Levitate : Fly off the ground </size>";
                MonsterPart[1].GetComponent<Animator>().SetInteger("PartID", 0);
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Jack_SB");
                break;
            case 1:
                InformationText[1].text = "<i>Wendigo's BOdy</i> \n <size=25> •High Jump : Performs powerful jump. </size>";
                MonsterPart[1].GetComponent<Animator>().SetInteger("PartID", 1);
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Wendigo_SB");
                break;
            case 2:
                InformationText[1].text = "<i>Frankenstein's Body</i> \n <size=25> •Heavy Tackle : Tackles the enemy, knocking the enemy back. </size>";
                MonsterPart[1].GetComponent<Animator>().SetInteger("PartID", 2);
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Frank_SB");
                break;
            case 3:
                InformationText[1].text = "<i>Mecha Dino's Body</i> \n <size=25> •Forcefield : Creates defensive shield at your mouse position </size>";
                MonsterPart[1].GetComponent<Animator>().SetInteger("PartID", 3);
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Dino_SB");

                break;
            case 4:
                InformationText[1].text = "<i>Phitahkhon's Body</i> \n <size=25> •Pursuit : Dash towards the enemy</size>";
                MonsterPart[1].GetComponent<Animator>().SetInteger("PartID", 4);
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Peetahkhon_SB");

                break;
            case 5:
                InformationText[1].text = "<i>Oni's Body</i> \n <size=25> •Earthquake : Stomps the ground to create earthquake.</size>";
                MonsterPart[1].GetComponent<Animator>().SetInteger("PartID", 5);
                SkillPicture[1].sprite = Resources.Load<Sprite>("Monster skill icons/Oni_SB");

                break;
        }

        switch (PartID[2]) //Control L hand Part
        {
            case 0:
                InformationText[2].text = "<i>Jack's Arm</i> \n <size=25> •Harvest : Slash the enemy with scythe, while stealing HP.</size>";
                //ExamplePictures[2].color = Color.red;
                MonsterPart[2].GetComponent<Animator>().SetInteger("PartID", 0);
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Jack_SA");
                break;
            case 1:
                InformationText[2].text = "<i>Wendigo's Arm</i> \n <size=25> •Beast Slash : Slash the enemy.</size>";
                //ExamplePictures[2].color = Color.blue;
                MonsterPart[2].GetComponent<Animator>().SetInteger("PartID", 1);
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Wendigo_SA");
                break;
            case 2:
                InformationText[2].text = "<i>Frankenstien's Arm</i> \n <size=25> •Shocking Punch : Punches the enemy with electric fist.</size>";
                //ExamplePictures[2].color = Color.green;
                MonsterPart[2].GetComponent<Animator>().SetInteger("PartID", 2);
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Frank_SA");
                break;
            case 3:
                InformationText[2].text = "<i>Mecha Dino's Arm</i> \n <size=25> •Severing Chainsaw : Slices enemy with chainsaw.</size>";
                MonsterPart[2].GetComponent<Animator>().SetInteger("PartID", 3);
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Dino_SA");
                break;
            case 4:
                InformationText[2].text = "<i>Phitahkhon's Arm</i> \n <size=25> •Haunting Claw : Launch the claw to slash the enemy.</size>";
                MonsterPart[2].GetComponent<Animator>().SetInteger("PartID", 4);
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Peetahkhon_SA");
                break;
            case 5:
                InformationText[2].text = "<i>Oni's Arm</i> \n <size=25> •Mace Strike : Strike enemy with mace.</size>";
                MonsterPart[2].GetComponent<Animator>().SetInteger("PartID", 5);
                SkillPicture[2].sprite = Resources.Load<Sprite>("Monster skill icons/Oni_SA");
                break;
        }

        switch (PartID[3]) //Control R Hand Part
        {
            case 0:
                InformationText[3].text = "<i>Jack's Arm</i> \n <size=25> •Harvest : Slash the enemy with scythe, while stealing HP.</size>";
                MonsterPart[3].GetComponent<Animator>().SetInteger("PartID", 0);
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Jack_SA");
                break;
            case 1:
                InformationText[3].text = "<i>Wendigo's Arm</i> \n <size=25>•Beast Slash : Slash the enemy.</size>";
                MonsterPart[3].GetComponent<Animator>().SetInteger("PartID", 1);
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Wendigo_SA");
                break;
            case 2:
                InformationText[3].text = "<i>Frankenstien's Arm</i> \n <size=25> •Shocking Punch : Punches the enemy with electric fist.</size>";
                MonsterPart[3].GetComponent<Animator>().SetInteger("PartID", 2);
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Frank_SA");
                break;
            case 3:
                InformationText[3].text = "<i>Mecha Dino's Arm</i> \n <size=25> •Severing Chainsaw : Slices enemy with chainsaw.</size>";
                MonsterPart[3].GetComponent<Animator>().SetInteger("PartID", 3);
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Dino_SA");
                break;
            case 4:
                InformationText[3].text = "<i>Phitahkhon's Arm</i> \n <size=25> •Haunting Claw : Launch the claw to slash the enemy.</size>";
                MonsterPart[3].GetComponent<Animator>().SetInteger("PartID", 4);
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Peetahkhon_SA");
                break;
            case 5:
                InformationText[3].text = "<i>Oni's Arm</i> \n <size=25> •Mace Strike : Strike enemy with mace.</size>";
                MonsterPart[3].GetComponent<Animator>().SetInteger("PartID", 5);
                SkillPicture[3].sprite = Resources.Load<Sprite>("Monster skill icons/Oni_SA");
                break;
        }
    }
}
