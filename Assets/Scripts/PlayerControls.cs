using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3;

    private Vector3 touchPosition;
    private Vector3 mPos;

    private Animator anim;

    private bool isColliding = false;

    private void Start()
    {
        anim = GetComponent<Animator>();

        touchPosition = transform.position;
        mPos = transform.position;
    }

    private void Update()
    {
        if (!isColliding)
        {
            //Move();

            MoveTest();
        }
    }

    private void Move()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
        }

        if (transform.position != touchPosition)
        {
            transform.position = UnityEngine.Vector2.MoveTowards(transform.position, touchPosition, moveSpeed * Time.deltaTime);

            anim.SetBool("walking", true);
            WalkAnimation(touchPosition);
        }
        else
        {
            anim.SetBool("walking", false);
            isColliding = false;
        }
    }

    private void MoveTest()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mPos.z = 0;
        }

        if (transform.position != mPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, mPos, moveSpeed * Time.deltaTime);

            anim.SetBool("walking", true);
            WalkAnimation(mPos);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    private void WalkAnimation(Vector3 _touchPos)
    {
        if (math.distance(_touchPos.x, transform.position.x) > math.distance(_touchPos.y, transform.position.y))
        {
            if (_touchPos.x > transform.position.x)
            {
                anim.SetFloat("SpeedX", 1);
                anim.SetFloat("SpeedY", 0);
            }
            else if (_touchPos.x < transform.position.x)
            {
                anim.SetFloat("SpeedX", -1);
                anim.SetFloat("SpeedY", 0);
            }
        }
        else if (math.distance(_touchPos.x, transform.position.x) < math.distance(_touchPos.y, transform.position.y))
        {
            if (_touchPos.y > transform.position.y)
            {
                anim.SetFloat("SpeedX", 0);
                anim.SetFloat("SpeedY", 1);
            }
            else if (_touchPos.y < transform.position.y)
            {
                anim.SetFloat("SpeedX", 0);
                anim.SetFloat("SpeedY", -1);
            }
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

        touchPosition = transform.position;
        mPos = transform.position;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}