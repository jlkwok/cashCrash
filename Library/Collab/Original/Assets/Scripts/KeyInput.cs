using UnityEngine;
using System.Collections;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection
public class KeyInput : MonoBehaviour
{
    [SerializeField] private bool isCamera = false;
    private CharacterController _charController;
    [SerializeField] AudioClip movementSound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = movementSound;
    }

    void Update()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        float dist = 1.5f;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(movePlayerUp());
            //transform.position = new Vector3(xPos, yPos + (dist * Time.deltaTime), zPos);
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !isCamera)
        {
            StartCoroutine(movePlayerLeft());
            //transform.position = new Vector3(xPos - dist, yPos, zPos);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(movePlayerDown());
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !isCamera)
        {
            StartCoroutine(movePlayerRight());
            //transform.position = new Vector3(xPos + dist, yPos, zPos);
        }
        audioSource.Play();
    }

    IEnumerator movePlayerUp()
    {
        float origYPos = transform.position.y;
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        while (yPos - origYPos < 1.5f)
        {
            yPos = transform.position.y;
            if (yPos + (1.5 * Time.deltaTime) < origYPos + 1.5f)
            {
                transform.position = new Vector3(xPos, yPos + (10.0f * Time.deltaTime), zPos);
                yield return new WaitForSeconds(0.01f);
            }
        }
        transform.position = new Vector3(xPos, origYPos + 1.5f, zPos);
    }

    IEnumerator movePlayerDown()
    {
        float origYPos = transform.position.y;
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        while (origYPos - yPos < 1.5f)
        {
            yPos = transform.position.y;
            if (yPos - (10.0f * Time.deltaTime) > origYPos - 10.0f)
            {
                transform.position = new Vector3(xPos, yPos - (10.0f * Time.deltaTime), zPos);
                yield return new WaitForSeconds(0.01f);
            }
        }
        transform.position = new Vector3(xPos, origYPos - 1.5f, zPos);
    }

    IEnumerator movePlayerLeft()
    {
        float origXPos = transform.position.x;
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        while (xPos - origXPos < 1.5f)
        {
            xPos = transform.position.x;
            if (xPos - (10.0f * Time.deltaTime) > origXPos - 1.5f)
            {
                transform.position = new Vector3(xPos - (10.0f * Time.deltaTime), yPos, zPos);
                yield return new WaitForSeconds(0.01f);
            }
        }
        transform.position = new Vector3(origXPos - 1.5f, yPos, zPos);
    }

    IEnumerator movePlayerRight()
    {
        float origXPos = transform.position.x;
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        while (xPos - origXPos < 1.5f)
        {
            xPos = transform.position.x;
            if (xPos + (10.0 * Time.deltaTime) < origXPos + 10.0f)
            {
                transform.position = new Vector3(xPos + (10.0f * Time.deltaTime), yPos, zPos);
                yield return new WaitForSeconds(0.01f);
            }
        }
        transform.position = new Vector3(xPos + 1.5f, yPos, zPos);
    }
}
