using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1000f;
    public float mag;
    public bool canShoot = true;
    public bool started = false;
    public Touch touch;
    public bool firstCube = false;
    public bool hitAgain = false;
    public bool checkMag = false;

    private Rigidbody2D rb;
    private Vector2 direction;
    public Vector2 dir;
    private GameObject trail;
    private LevelManager levelManager;

    // Use this for initialization
    void Start()
    {
        moveSpeed = 10000.0f;
        mag = 10f;
        rb = GetComponent<Rigidbody2D>();
        trail = GameObject.Find("Trail");
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        if (started && mag <= 0)
        {
            levelManager.dead = true;
        }

        if (levelManager.dead)
        {
            trail.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0) && canShoot && !levelManager.failed)
        {
            ShootBall();
            StartCoroutine(StartTheGame());
        }

        if (checkMag)
        {
            mag = rb.velocity.magnitude;
        }
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    private void ShootBall()
    {
        trail.SetActive(true);
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            direction.x = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x);
            direction.y = (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y);
            direction = direction.normalized;
            dir.x = direction.x;
            dir.y = direction.y;
            Vector2 force = dir * moveSpeed * Time.deltaTime;
            rb.AddForce(force);
            Debug.Log(rb.velocity.magnitude);
            //rb.velocity = force;
            checkMag = false;
            mag = 10f;
            canShoot = false;
            started = true;
        }
    }
   
    private IEnumerator StartTheGame(){
        yield return new WaitForSeconds(1.0f);
        checkMag = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Exit"){
            levelManager.passed = true;
        }
    }

}
