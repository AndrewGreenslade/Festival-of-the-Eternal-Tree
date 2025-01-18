using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;

    public float moveSpeed;
    private bool isFacingRight = true;    //the enemy always starts facing in the right direction

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //control direction the enemy is moving in
        if (transform.position.x >= rightPoint.position.x)
        {
            isFacingRight = false;
        }
        else if (transform.position.x <= leftPoint.position.x)
        {
            isFacingRight = true;
        }

        int direction = isFacingRight == true ? 1 : -1;

        //move enemy in said direction
        rigidbody2D.AddForce(new Vector3(direction, 0, 0) * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);

        //flip enemy sprite if needed
        spriteRenderer.flipX = isFacingRight == true ? true : false;
    }
}
