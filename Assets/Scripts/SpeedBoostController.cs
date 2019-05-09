using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostController : MonoBehaviour
{
   AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        Debug.Log(player);
        if (player != null)
        {
            StartCoroutine(playNoise());
            this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
        }

    }

    IEnumerator playNoise()
    {
        audio.Play();
        yield return new WaitForSeconds(2.0f);
        GameObject.Destroy(gameObject);
    }
}
