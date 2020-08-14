using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    [SerializeField] private AudioClip doorIsLocked;
    [SerializeField] private AudioClip doorUnlocked;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.hasKey)
        {
            audioSource.clip = doorUnlocked;
            audioSource.Play();

            Destroy(gameObject, audioSource.clip.length);
            //GameManager.hasKey = false;
        }
        else
        {
            audioSource.clip = doorIsLocked;
            audioSource.Play();
        }
    }
}