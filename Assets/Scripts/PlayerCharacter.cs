using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int money;
    [SerializeField] private GameObject speedIcon;
    [SerializeField] private GameObject moneyIcon;

    private Rect healthLabel;
    private int healthTextWidth;

    private GUIStyle moneyGUIStyle;
    private Rect moneyLabel;
    public Font myFont;
    private int moneyTextWidth;

    private Scene currentScene;
    private string sceneName;

    private float speedTimer = 0;
    private float moneyTimer = 0;
    private int moneyModifier = 1;

    // leaderboard
    private static string leaderboardPath;
    private SortedList sl = new SortedList(10);
    private bool submitted = false;

    // for killed indicator
    public GameObject playAgainButton;
    public GameObject returnToMenuButton;
    public GameObject submitNameButton;
    public Slider HealthBar;
    public Text youEarned;
    public Text youGotTop;
    public GameObject nameInput;

    bool hitLeft = false;
    bool hitRight = false;


    
    // Start is called before the first frame update
    void Start()
    {
        leaderboardPath = Application.persistentDataPath + "/leaderboard.dat";
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        playAgainButton.SetActive(false);
        returnToMenuButton.SetActive(false);
        nameInput.SetActive(false);
        submitNameButton.SetActive(false);
        health = 100;
        money = 0;

        moneyGUIStyle = new GUIStyle();
        moneyGUIStyle.fontSize = 30;
        moneyGUIStyle.font = myFont;
        moneyGUIStyle.normal.textColor = Color.white;

        moneyTextWidth = 175;
        healthTextWidth = 100;
        youEarned.enabled = false;
        youGotTop.enabled = false;
        
        speedIcon.GetComponent<Image>().color = Color.grey;
        moneyIcon.GetComponent<Image>().color = Color.grey;

    }

    void Update(){
        if (health == 0)
        {
            Debug.Log(money);
            LoadScore();
            youEarned.text = "you earned $" + money.ToString();
            youEarned.alignment = TextAnchor.MiddleCenter;
            youEarned.enabled = true;
            playAgainButton.SetActive(true);
            returnToMenuButton.SetActive(true);
            if ((!sl.ContainsKey(money)) && (sl.Count < 10 || (int)sl.GetKey(0) < money))
            {
                if (!submitted)
                {
                    youGotTop.enabled = true;
                    nameInput.SetActive(true);
                    submitNameButton.SetActive(true);
                }
            }
            GameObject.Destroy(gameObject.GetComponent<KeyInput>());
            GameObject.Destroy(GameObject.Find("Main Camera").GetComponent<KeyInput>());
        }

        if(moneyTimer > 0){
            moneyIcon.GetComponent<Image>().color = Color.white;
            moneyTimer -= Time.deltaTime;
            if(moneyTimer <= 0){
                moneyIcon.GetComponent<Image>().color = Color.grey;
                moneyModifier = 1;
            }
        }

        if(speedTimer > 0){
            speedIcon.GetComponent<Image>().color = Color.white;
            speedTimer -= Time.deltaTime;
            if(speedTimer <= 0){
                speedIcon.GetComponent<Image>().color = Color.grey;
                gameObject.GetComponent<KeyInput>().SetMoveSpeed(10f);
            }
        }


        if(hitLeft){
            StartCoroutine(movePlayerLeft());
            hitLeft = false;
        }
        if(hitRight){
            StartCoroutine(movePlayerRight());
            hitRight = false;
        }
    }

    IEnumerator movePlayerLeft()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        float origXPos = transform.position.x;
        while (xPos > origXPos - 1.5f)
        {
            xPos = transform.position.x;
            if (xPos - Time.deltaTime * 15.0f > origXPos - 1.5f)
            {
                transform.position = new Vector3(xPos - (15.0f * Time.deltaTime), yPos, zPos);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        transform.position = new Vector3(origXPos - 1.5f, yPos, zPos);
    }

    IEnumerator movePlayerRight()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        float origXPos = transform.position.x;
        while (xPos < origXPos + 1.5f)
        {
            xPos = transform.position.x;
            if (xPos + Time.deltaTime * 15.0f < origXPos + 1.5f)
            {
                transform.position = new Vector3(xPos + (15.0f * Time.deltaTime), yPos, zPos);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        transform.position = new Vector3(origXPos + 1.5f, yPos, zPos);
    }

    public void SaveScore()
    {
        for(int i = 0; i < sl.Count; i++) {
            Debug.Log(sl.GetKey(i));
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(leaderboardPath, FileMode.OpenOrCreate);
        String name = nameInput.GetComponent<InputField>().text;
        Debug.Log(name);
        Debug.Log(leaderboardPath);
        sl.Add(money, name);
        while (sl.Count > 10)
        {
            sl.RemoveAt(0);
        }
        bf.Serialize(file, sl);
        file.Close();
        nameInput.SetActive(false);
        submitNameButton.SetActive(false);
        youGotTop.enabled = false;
        submitted = true;
    }

    void LoadScore()
    {
        if (File.Exists(leaderboardPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(leaderboardPath, FileMode.Open);
            sl = (SortedList)bf.Deserialize(file);
            file.Close();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CarController car = other.GetComponent<CarController>();
        HealthPackController hp = other.GetComponent<HealthPackController>();
        ValueDoublerController vd = other.GetComponent<ValueDoublerController>();
        SpeedBoostController sd = other.GetComponent<SpeedBoostController>();
        Raccoon rac = other.GetComponent<Raccoon>();


        if (car != null)
        {
            //blinking = true;
            car.Collision();
            if (health > 0){
                health -= car.getDamage();
                money += car.getValue() * moneyModifier;
                gameObject.GetComponent<ParticleSystem>().Play();

                if(car.getDamage() < 0){
                    gameObject.GetComponents<AudioSource>()[1].Play();
                }
                else{
                    gameObject.GetComponents<AudioSource>()[0].Play();
                }

                if(health < 0){
                    health = 0;
                }

                if(health > 100){
                    health = 100;
                }
                HealthBar.value = health;
                if(car.speed < 0){
                    hitLeft = true;
                }
                else{
                    hitRight = true;
                }

            }
        }
        else if(rac != null){
            if (this.transform.position.x < rac.transform.position.x){
                hitLeft = true;
            }
            else{
                hitRight = true;
            }
        }
        else if (vd != null){
            moneyModifier = 2;
            moneyTimer = 5f;
        }
        else if (sd != null){
            gameObject.GetComponent<KeyInput>().SetMoveSpeed(20f);
            speedTimer = 5f;
        }
        else if (hp != null && health < 100) {
            health += 10;
            HealthBar.value = health;
        }
    }

    void OnGUI()
    {
        if (sceneName == "Endless") {
            moneyLabel = new Rect(10, 50, moneyTextWidth, 22);
            GUI.Label(moneyLabel, "money: $" + money, moneyGUIStyle);

            healthLabel = new Rect(350, 10, healthTextWidth, 22);
            GUI.Label(healthLabel, health + "/100", moneyGUIStyle);
        }
    }
}
