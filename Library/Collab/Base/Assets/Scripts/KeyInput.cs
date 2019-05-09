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
            transform.position = new Vector3(xPos, yPos + dist, zPos);
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !isCamera)
        {
            transform.position = new Vector3(xPos - dist, yPos, zPos);         
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = new Vector3(xPos, yPos - dist, zPos);
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !isCamera)
        {
            transform.position = new Vector3(xPos + dist, yPos, zPos);
        }
        audioSource.Play();
    }
}
