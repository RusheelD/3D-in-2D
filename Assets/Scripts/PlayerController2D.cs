using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float maxSpeed = 3.75f;
    public float jumpHeight = 5.25f;
    public float gravityScale = 1f;
    public MapData2D map;
    public bool yView;

    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Rigidbody2D r2d;
    PolygonCollider2D groundChecker;
    BoxCollider2D mainBoxCollider;
    CapsuleCollider2D mainCapsuleCollider;
    Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainBoxCollider = GetComponent<BoxCollider2D>();
        mainCapsuleCollider = GetComponent<CapsuleCollider2D>();
        groundChecker = GetComponent<PolygonCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        yView = false;

    }

    public int GetIntX()
    {
        return Mathf.RoundToInt(x);
    }
    public int GetIntY()
    {
        return Mathf.RoundToInt(y);
    }
    public int GetIntZ()
    {
        return Mathf.RoundToInt(z);
    }

    public void SetX(float newX)
    {
        x = newX;
    }
    public void SetY(float newY)
    {
        y = newY;
    }
    public void SetZ(float newZ)
    {
        z = newZ;
    }


    public void SwitchPlane()
    {
        float newX = (map.plane == Planes2D.X) ? x : z;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (yView)
        {
            return;
        }

        if (map.plane == Planes2D.X)
        {
            x = transform.position.x;
        } else
        {
            z = transform.position.x;
        }
        y = transform.position.y;

        bool moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        int dir = 0;
        dir -= moveLeft ? 1 : 0;
        dir += moveRight ? 1 : 0;
        moveDirection = dir;

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        bool doJump = Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow);
        if (doJump && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }
    }

    void FixedUpdate()
    {
        // Check if player is grounded
        Collider2D[] groundColliders = new Collider2D[5];            
        int numGroundColliders = groundChecker.OverlapCollider(new ContactFilter2D(), groundColliders);

        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (numGroundColliders > 0)
        {
            for (int i = 0; i < numGroundColliders; i++)
            {
                if (groundColliders[i] != mainBoxCollider && groundColliders[i] != mainCapsuleCollider && groundColliders[i].transform.name != "Wall")
                {
                    isGrounded = true;
                    break;
                }
            }
        }
        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

    }
}
