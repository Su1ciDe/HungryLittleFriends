using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && collision.isTrigger)
        {
            GameManager.hasKey = true;

            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
