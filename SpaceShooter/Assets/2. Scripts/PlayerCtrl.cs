using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public float hp = 100;
    [Header("Related to move")]
    [Range(0.5f, 10f)]
    public float jumpPow;
    public GameObject compPos;
    public bool canJump;

    [Range(0.5f, 10f)]
    public float normalSpeed;
    [Range(0.5f, 10f)]
    public float dashSpeed;
    private float originSpeed;

    [Range(100f, 1000f)]
    public float mounseSensivity;

    private float mx, my;
    private float curRotX;

    private Rigidbody rb;

    [Header("Related to attack")]
    public GameObject bulletPrefab;
    public Transform weaponTr;
    public AudioSource audioSource;
    public AudioClip fireSFX;


    public int attDamage;

    [Header("Related to UI")]
    public Slider hpBar;


    // Start is called before the first frame update
    void Start()
    {
        mounseSensivity = PlayerPrefs.GetInt("MS");
        rb = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        audioSource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        HPBar();
        PlayerMove();
    }

    void PlayerMove()
    {
        //Player Move
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (Input.GetKey(KeyCode.LeftShift))
            originSpeed = dashSpeed;
        else
            originSpeed = normalSpeed;

        transform.Translate(dir * originSpeed * Time.deltaTime);
        //

        //Player Jump

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var item in Physics.OverlapSphere(compPos.transform.position, 0.1f))
            {
                if (item.CompareTag("FLOOR"))
                    canJump = true;
            }
            if(canJump == true)
            {
                rb.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
                canJump = false;
            }
        }

 
        //

        //Player Mouse Control

        mx = Input.GetAxis("Mouse X") * mounseSensivity * Time.deltaTime;
        Vector3 rotY = new Vector3(0, mx, 0);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotY));

        
        my = Input.GetAxis("Mouse Y") * mounseSensivity * Time.deltaTime;
        curRotX -= my;
        curRotX = Mathf.Clamp(curRotX, -80, 80);

        Camera.main.transform.localEulerAngles = new Vector3(curRotX, 0, 0);
        //
    }

    private void HPBar()
    {
        hpBar.value = hp / 100f;
    }


}
