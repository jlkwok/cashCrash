using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 0;
    float timer = 0;
    [SerializeField] int value;
    [SerializeField] int damage;

    // Start is called before the first frame update
    void Start()
    {
        if(damage == 0){
            damage = 10;
        }
    }

    public void setSpeed(float s){
        this.speed = s;
    }

    public int getValue()
    {
        return value;
    }

    public int getDamage()
    {
        return (int)(damage * Random.Range(1, 1.25f));
    }

    public void increaseValue(int increase){
        value += increase;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(speed, 0, 0); 
        timer += Time.deltaTime;
        if(timer >= 10.0 && speed > 0){
            GameObject.Destroy(gameObject);
        }
    }

    public void Collision(){
        GameObject.Destroy(gameObject);
    }
}
