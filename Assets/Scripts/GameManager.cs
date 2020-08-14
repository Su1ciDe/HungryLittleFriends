using UnityEngine;
using System.Collections;
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
        StartCoroutine(_StartGame());
    }

    private IEnumerator _StartGame()
    {
        float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(1);
    }

    public void NextLevel(int curLevel)
    {
        StartCoroutine(_NextLevel(curLevel));
    }

    private IEnumerator _NextLevel(int curLevel)
    {
        float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(curLevel + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}