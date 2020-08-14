using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    public new string name = "Animal";
    public int hunger = 1;
    private int currentHunger;
    public AudioClip animalSound;

    [SerializeField] private float animalWalkSpeed = 1;
    private float posX = 0, posY = 0;
    private Vector3 walkPos;

    private bool isFeedingTime = false;
    public GameObject bowl;

    private bool isColliding = false;

    private Animator anim;

    public Text txtHunger;

    public float maxX, minX, maxY, minY;

    private void Start()
    {
        anim = GetComponent<Animator>();

        walkPos = transform.position;

        currentHunger = hunger;

        InvokeRepeating("HungerUI", 0, 1);
    }

    private void Update()
    {
        if (GameManager.currentFoodInBowl >= currentHunger)
        {
            isFeedingTime = true;
        }

        if (!isColliding)
        {
            if (!isFeedingTime)
                WalkRandomly();
            else
                FeedingTime();
        }
    }

    private void WalkRandomly()
    {
        if (posX == 0)
            posX = (float)UnityEngine.Random.Range(minX, maxX);
        if (posY == 0)
            posY = (float)UnityEngine.Random.Range(minY, maxY);

        walkPos = new Vector3(posX, posY, 0);

        if (transform.position != walkPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, walkPos, animalWalkSpeed * Time.deltaTime);

            anim.SetBool("Walk", true);
            WalkAnimation(walkPos);
        }
        else
        {
            posX = 0;
            posY = 0;

            anim.SetBool("Walk", false);
        }
    }

    private void WalkAnimation(Vector3 _walkPos)
    {
        if (math.distance(_walkPos.x, transform.position.x) > math.distance(_walkPos.y, transform.position.y))
        {
            if (_walkPos.x > transform.position.x)
            {
                anim.SetFloat("SpeedX", 1);
                anim.SetFloat("SpeedY", 0);
            }
            else if (_walkPos.x < transform.position.x)
            {
                anim.SetFloat("SpeedX", -1);
                anim.SetFloat("SpeedY", 0);
            }
        }
        else if (math.distance(_walkPos.x, transform.position.x) < math.distance(_walkPos.y, transform.position.y))
        {
            if (_walkPos.y > transform.position.y)
            {
                anim.SetFloat("SpeedX", 0);
                anim.SetFloat("SpeedY", 1);
            }
            else if (_walkPos.y < transform.position.y)
            {
                anim.SetFloat("SpeedX", 0);
                anim.SetFloat("SpeedY", -1);
            }
        }
    }

    private void FeedingTime()
    {
        if (bowl.transform.position != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, bowl.transform.position, animalWalkSpeed * 2 * Time.deltaTime);

            anim.SetBool("Walk", true);
            WalkAnimation(bowl.transform.position);
        }
        else
        {
            anim.SetBool("Walk", false);
            currentHunger = hunger;

            GameManager.gm.NextLevel(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isColliding = true;

        Vector3 hit = col.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);
        float x = 0, y = 0;

        if (Mathf.Approximately(angle, 0))
        {   //Down
            y = (float)1 / 20;
        }
        if (Mathf.Approximately(angle, 180))
        {   //Up
            y = (float)-1 / 20;
        }
        if (Mathf.Approximately(angle, 90))
        {
            Vector3 cross = Vector3.Cross(Vector3.forward, hit);
            if (cross.y > 0)
            {   // left side of the player
                x = (float)1 / 20;
            }
            else
            {   // right side of the player
                x = (float)-1 / 20;
            }
        }

        Vector3 newPos = new Vector3(x, y, 0);
        transform.position += newPos;

        walkPos = transform.position;

        posX = (float)UnityEngine.Random.Range(-200, 200) / 100;
        posY = (float)UnityEngine.Random.Range(200, 470) / 100;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

    private void HungerUI()
    {
        txtHunger.text = "HUNGER: " + currentHunger;
    }
}