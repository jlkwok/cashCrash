using UnityEngine;
using System.Collections;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection
public class KeyInput : MonoBehaviour
{
    private CharacterController _charController;
    private Vector3 camPos;
    private float modifier;
    public bool moving = false;
    [SerializeField] AudioClip movementSound;
    AudioSource audioSource;
    public Animator animator;

    private static float moveSpeed = 10.0f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        modifier = 1;
    }

    public void SetMoveSpeed(float mod)
    {
        moveSpeed = mod;
    }

    void Update()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        float dist = 1.5f;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !moving)
        {

            StartCoroutine(movePlayerUp());
            audioSource.PlayOneShot(movementSound, 0.5F);
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !moving)
        {
            StartCoroutine(movePlayerLeft());
            audioSource.PlayOneShot(movementSound, 0.5F);
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !moving)
        {
            StartCoroutine(movePlayerDown());
            audioSource.PlayOneShot(movementSound, 0.5F);
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !moving)
        {
            StartCoroutine(movePlayerRight());
            audioSource.PlayOneShot(movementSound, 0.5F);
        }
    }

    IEnumerator movePlayerUp()
    {
        this.moving = true;
        animator.SetBool("Moving", true);
        float yPos = transform.position.y;
        float origYPos = transform.position.y;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        while (yPos < origYPos + 1.5f)
        {
            yPos = transform.position.y;
            if (yPos + Time.deltaTime * moveSpeed < origYPos + 1.5f)
            {
                transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
                camPos = Camera.main.transform.position;
                Camera.main.transform.position = new Vector3(camPos.x, transform.position.y, camPos.z);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        transform.position = new Vector3(transform.position.x, origYPos + 1.5f, transform.position.z);
        camPos = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(camPos.x, transform.position.y, camPos.z);
        this.moving = false;
        animator.SetBool("Moving", false);
    }

    IEnumerator movePlayerDown()
    {
        this.moving = true;
        animator.SetBool("Moving", true);
        float yPos = transform.position.y;
        float origYPos = transform.position.y;
        transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        while (yPos > origYPos - 1.5f)
        {
            yPos = transform.position.y;
            if (yPos - Time.deltaTime * moveSpeed > origYPos - 1.5f)
            {
                transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
                camPos = Camera.main.transform.position;
                Camera.main.transform.position = new Vector3(camPos.x, transform.position.y, camPos.z);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }

        transform.position = new Vector3(transform.position.x, origYPos - 1.5f, transform.position.z);
        camPos = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(camPos.x, transform.position.y, camPos.z);
        this.moving = false;
        animator.SetBool("Moving", false);
    }

    IEnumerator movePlayerLeft()
    {
        this.moving = true;
        animator.SetBool("Moving", true);
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        float origXPos = transform.position.x;
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        while (xPos > origXPos - 1.5f)
        {
            xPos = transform.position.x;
            if (xPos - Time.deltaTime * moveSpeed > origXPos - 1.5f)
            {
                transform.position = new Vector3(xPos - (moveSpeed * Time.deltaTime), yPos, zPos);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        transform.position = new Vector3(origXPos - 1.5f, yPos, zPos);
        this.moving = false;
        animator.SetBool("Moving", false);
    }

    IEnumerator movePlayerRight()
    {
        animator.SetBool("Moving", true);
        this.moving = true;
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        float origXPos = transform.position.x;
        transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        while (xPos < origXPos + 1.5f)
        {
            xPos = transform.position.x;
            if (xPos + Time.deltaTime * moveSpeed < origXPos + 1.5f)
            {
                transform.position = new Vector3(xPos + (moveSpeed * Time.deltaTime), yPos, zPos);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        transform.position = new Vector3(origXPos + 1.5f, yPos, zPos);
        this.moving = false;
        animator.SetBool("Moving", false);
    }
}
