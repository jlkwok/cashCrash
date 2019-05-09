using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject raccoonPrefab;

    [SerializeField] private GameObject[] roadPrefab;
    [SerializeField] private GameObject[] powerPrefab;

    private float laneYPos = 0;
    private float playerPos = 0;
    private int difficultyMod;
    private int lanesSinceGrass;
    private List<GameObject> roads;

    private float timer = 15f;


    // Start is called before the first frame update
    void Start()
    {
        roads = new List<GameObject>();
        lanesSinceGrass = 0;
        difficultyMod = 1;
        for(int i = 0; i < 30; i += 1){
            int roadIndex = randRoadIndex();
            if(lanesSinceGrass >= 5){
                roadIndex = 1;
            }
            GameObject roadObject = Instantiate(roadPrefab[roadIndex]) as GameObject;
            roadObject.transform.position = new Vector3(0f, i * 1.5f, 1f);
            roads.Add(roadObject);

            if(roadIndex == 0){
                RoadController road = roadObject.GetComponent<RoadController>();
                setRandomRoadVals(road);
                lanesSinceGrass++;
            }
            else{
                lanesSinceGrass = 0;
                int rand = Random.Range(1, 3);
                if (rand == 1)
                {
                    GameObject raccoon = Instantiate(raccoonPrefab) as GameObject;

                    raccoon.transform.position = roadObject.transform.position;
                    raccoon.transform.position += new Vector3((int)Random.Range(-6, 6) * 1.5f, 0f, -1.0f);
                    raccoon.GetComponent<Raccoon>().SetLimit(10 + Random.Range(-5,5));                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(laneYPos <= player.transform.position.y){
            int roadIndex = randRoadIndex();
            if(lanesSinceGrass > 6){
                roadIndex = 1;
            }

            GameObject roadObject = Instantiate(roadPrefab[roadIndex]) as GameObject;
            roads.Add(roadObject);
            if(roads.Count > 100){
                GameObject deallocateRoad = roads[0];
                roads.RemoveAt(0);
                GameObject.Destroy(deallocateRoad);
            }
            roadObject.transform.position = new Vector3(0f, playerPos + 45f, 1f);
            playerPos += 1.5f;

            if(roadIndex == 0){
                RoadController road = roadObject.GetComponent<RoadController>();
                setRandomRoadVals(road);
                lanesSinceGrass++;
            }
            else{
                lanesSinceGrass = 0;
                int rand = Random.Range(1, 3);
                if (rand == 1)
                {
                    GameObject raccoon = Instantiate(raccoonPrefab) as GameObject;

                    raccoon.transform.position = roadObject.transform.position;
                    raccoon.transform.position += new Vector3((int)Random.Range(-6, 6) * 1.5f, 0f, 0f);
                    raccoon.GetComponent<Raccoon>().SetLimit(10 + Random.Range(-5,5));
                }

            }

            laneYPos += 1.5f;
            difficultyMod += 1;

            int randInt = Random.Range(1, 7);
            if (randInt == 1)
            {
                GameObject powerPack = Instantiate(powerPrefab[Random.Range(0, 3)]) as GameObject;
                powerPack.transform.position = player.transform.position;
                powerPack.transform.position += new Vector3((int)Random.Range(-6, 6) * 1.5f, 12f, .5f);
                powerPack.transform.localScale = new Vector3(0.25f, 0.25f, 1.0f);
            }
        }
        
        timer -= Time.deltaTime;
        if(timer <= 0){
            GameObject deallocateRoad = roads[0];
            GameObject roadObject = Instantiate(roadPrefab[2]) as GameObject;
            float yPos = deallocateRoad.transform.position.y;
            roadObject.transform.position = new Vector3(0, yPos, -2);

            roads.RemoveAt(0);
            GameObject.Destroy(deallocateRoad);
            timer = 3f;
        }
    }

    private int randRoadIndex(){
        float randInt = Random.Range(1, 100);
        if(difficultyMod > 200){
            randInt = randInt * 2;
        }
        else{
            randInt = randInt * (1  + (difficultyMod / 100f));
        }

        if(randInt >= 33){
            return 0;
        }
        else{
            return 1;
        }
    }

    private void setRandomRoadVals(RoadController road){
        int direction = Random.Range(1, 3);
        if(direction == 2){
            road.setDirection(-1);
        }
        else{
            road.setDirection(1);
        }

        float randSpeedMod = Random.Range(-1f, 1f);
        randSpeedMod = Mathf.Max(-.05f, randSpeedMod);
        float speedMod = (float)(.1f + (randSpeedMod / 10f) + (difficultyMod / 1000f));

        road.setSpeedMod(speedMod);

        float randSpawnDelay = Random.Range(-.5f, .5f);
        float spawnDelay = Mathf.Max((.75f * (.2f / speedMod)) - randSpawnDelay, .5f);

        road.setSpeedMod(speedMod);
        road.setDifficultyMod(difficultyMod);
    }
}
