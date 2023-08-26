using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    public float maxSpeed = 3.9f;
    public float jumpHeight = 5f;
    public float gravityScale = 1f;
    public MapData2D map;
    public bool yView;

    public float x { get; private set; }
    public float y { get; private set; }
    public float z { get; private set; }
    public float w { get; private set; }

    bool isGrounded = false;
    int moveForward = 0;
    int moveRight = 0;
    Rigidbody r3d;
    CapsuleCollider mainCollider;
    BoxCollider groundChecker;
    Vector2 rotation = Vector2.zero;
    float sensitivity = 5;
    float maxRotationY = 88;
    List<GameObject> currentCollisions = new List<GameObject>();

    void Start()
    {
        r3d = GetComponent<Rigidbody>();
        r3d.collisionDetectionMode = CollisionDetectionMode.Continuous;
        mainCollider = GetComponent<CapsuleCollider>();
        groundChecker = GetComponent<BoxCollider>();
        groundChecker.isTrigger = true;
    }

    void Update()
    {
        rotation.x += Input.GetAxis("Mouse X") * sensitivity;
        rotation.y += Input.GetAxis("Mouse Y") * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -maxRotationY, maxRotationY);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);

        transform.localRotation = xQuat;

        bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        moveForward = 0;
        moveForward += up ? 1 : 0;
        moveForward -= down ? 1 : 0;

        moveRight = 0;
        moveRight += right ? 1 : 0;
        moveRight -= left ? 1 : 0;
        
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            r3d.velocity = new Vector3(r3d.velocity.x, jumpHeight, r3d.velocity.z);
        }
    }
    public int GetIntX() { return Mathf.RoundToInt(x); }
    public int GetIntY() { return Mathf.RoundToInt(y); }
    public int GetIntZ() { return Mathf.RoundToInt(z); }
    public int GetIntW() { return Mathf.RoundToInt(w); }

    public void SetX(float x) { this.x = x; }
    public void SetY(float y) { this.y = y; }
    public void SetZ(float z) { this.z = z; }
    public void SetW(float w) { this.w = w; }
    public void SwitchSpace() { }

    public void Reset()
    {
        currentCollisions.Clear();
        rotation = Vector2.zero;
    }

    void FixedUpdate()
    {
        if(currentCollisions.Count > 0)
        {
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }

        Vector3 groundMovement = maxSpeed * ( transform.forward * moveForward + transform.right * moveRight);
        
        r3d.velocity = new Vector3(groundMovement.x, r3d.velocity.y, groundMovement.z);
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == gameObject)
        {
            return;
        }

        currentCollisions.Add(col.gameObject);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == gameObject)
        {
            return;
        }

        currentCollisions.Remove(col.gameObject);
    }
}
