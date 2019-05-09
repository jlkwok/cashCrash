using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccoon : MonoBehaviour
{
    public float speed = .03f;
    float xLimit = 10f;
    float xStart;
    bool moveRight = true;

    // Start is called before the first frame update
    void Start()
    {
        xStart = this.transform.position.x;
    }

    public void setSpeed(float s){
        this.speed = s;
    }

    public void SetLimit(float limit){
        xLimit = limit;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveRight){
            this.transform.position += new Vector3(speed, 0, 0); 
        }
        else{
            this.transform.position -= new Vector3(speed, 0, 0); 
        }

        if(moveRight && this.transform.position.x > xStart + xLimit){
            moveRight = false;
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }

        if(!moveRight && this.transform.position.x < xStart){
            moveRight = true;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    public void Collision(){
        GameObject.Destroy(gameObject);
    }
}
