using UnityEngine;

public class GiveFood : MonoBehaviour
{
    public Animal animal;
    private int hunger;

    private void Awake()
    {
        hunger = animal.hunger;
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (GameManager.currentFoodCount >= hunger)
            {
                SwitchBowlGraphics();
                GameManager.gm.PlayAnimalSound();
                GameManager.gm.PutFoodInBowl(GameManager.currentFoodCount);
                GameManager.currentFoodCount = 0;
            }
        }
    }

    private void SwitchBowlGraphics()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
        }
    }
}