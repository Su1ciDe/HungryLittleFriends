using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    public int foodQuality = 1;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && collision.isTrigger)
        {
            GameManager.AddFood(foodQuality);

            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}