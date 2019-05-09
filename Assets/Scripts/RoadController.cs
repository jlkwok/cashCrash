using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    [SerializeField] private GameObject healthPrefab;
    public int direction = 1;
    private float spawnTimer = 0;
    public float speedMod = 1f; 
    public float spawnDelaySeconds = 1;
    public int difficultyMod = 1;
    private float spawnDelayRand;

    // Start is called before the first frame update
    void Start()
    {
        float rand = Random.Range(.75f, 1.5f);
        spawnDelayRand = spawnDelaySeconds * rand;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnDelayRand){
            int carIndex = randCarIndex();
            GameObject carObject = Instantiate(cars[carIndex]) as GameObject;
            CarController car = carObject.GetComponent<CarController>();
            car.increaseValue(difficultyMod / 10);
            car.setSpeed(speedMod * direction);

            carObject.transform.position = this.transform.position;
            carObject.transform.position -= new Vector3(20 * direction, 0, 1);
            if(direction == -1){
                carObject.transform.Rotate(0, 0, 180, Space.Self);
            }

            spawnTimer = 0;
            float rand = Random.Range(.75f, 1.5f);
            spawnDelayRand = spawnDelaySeconds * rand;
        }
    }

    private int randCarIndex(){
        float randInt = Random.Range(1, 100);
        if(difficultyMod > 200){
            randInt = randInt * 2;
        }
        else{
            randInt = randInt + (difficultyMod / 100f);
        }

        if(randInt <= 70){
            return 0;
        }
        if(randInt <= 90){
            return 1;
        }
        else if(randInt <= 92){
            return 3;
        }
        else {
            return 2;
        }
    }


    public void setDirection(int direction){
        this.direction = direction;
    }

    public void setSpeedMod(float speedMod){
        this.speedMod = speedMod;
    }

    public void setSpawnDelaySeconds(float seconds){
        this.spawnDelaySeconds = seconds;
    }

    public void setDifficultyMod(int difficultyMod){
        this.difficultyMod = difficultyMod;
    }
}
