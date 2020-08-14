using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public Animal animal;

    public AudioSource audioS;

    public static int currentFoodCount = 0;
    public static int currentFoodInBowl = 0;

    public static bool hasKey;

    private void Awake()
    {
        if (gm == null)
            gm = this;

        LevelSetup();
    }

    private void Start()
    {
        LevelSetup();
    }

    private void LevelSetup()
    {
        currentFoodCount = 0;
        currentFoodInBowl = 0;
        hasKey = false;
    }

    public static void AddFood(int foodQuality)
    {
        currentFoodCount += foodQuality;
    }

    public void PutFoodInBowl(int foodCount)
    {
        currentFoodInBowl = foodCount;
    }

    public void PlayAnimalSound()
    {
        audioS.clip = animal.animalSound;
        audioS.Play();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void NextLevel(int curLevel)
    {
        SceneManager.LoadScene(curLevel + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }


}