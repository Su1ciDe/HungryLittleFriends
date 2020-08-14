using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image imgKey;
    public Text txtFood;

    private void Start()
    {
        InvokeRepeating("UIUpdate", 0, .5f);
    }

    private void Update()
    {
    }

    private void UIUpdate()
    {
        txtFood.text = "FOOD: " + GameManager.currentFoodCount;

        if (GameManager.hasKey==true)
        {
            imgKey.gameObject.SetActive(true);
        }
        else
        {
            imgKey.gameObject.SetActive(false);
        }
    }
}