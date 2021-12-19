using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdB: MonoBehaviour

{
    float posX, posY, posZ;
    float ySpeed;
    public float gravity; 
    public float clickImpulse;
    
    public Sprite flapUpSprite;
    public Sprite flapMidSprite;
    public Sprite flapDownSprite;

    int animCount = 0;

     public AudioSource flapSound;
     public AudioSource hitSound;
     public AudioSource scoreSound;
     public AudioSource finishSound;
     

     public Text scoreText;

     int score = 0;
    

     // Use this for initialization
    void Start(){
        posX = transform.position.x; 
        posY = transform.position.y; 
        posZ = transform.position.z; ySpeed = 0.0f;
    }
    void Flap()
    {
        ySpeed = clickImpulse;
        flapSound.Play();
    }

    void Update () 
        {
        if (Input.GetMouseButtonUp(0))
            Flap();
        
        animCount = animCount + 1;
        if (ySpeed <= 0.0f)
        animCount = 2;
        if (animCount == 4)
        animCount = 0;
        if (animCount == 1)
        GetComponent<SpriteRenderer>().sprite = flapUpSprite;
        else if (animCount == 3)
        GetComponent<SpriteRenderer>().sprite = flapDownSprite;
        else
        GetComponent<SpriteRenderer>().sprite = flapDownSprite;
        float angle = 9.0f * ySpeed;
        if (angle > 30.0f)
            angle = 30.0f;
        if (angle < -30.0f)
            angle = -30.0f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        scoreText.text = score.ToString();
        }
    

    void FixedUpdate()
    {
        ySpeed -= gravity * Time.fixedDeltaTime; 
        posY += ySpeed * Time.fixedDeltaTime;
        transform.localPosition = new Vector3(posX, posY, posZ);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "score")
        {
            scoreSound.Play();
            score = score + 1;
        }
        else if (collision.gameObject.tag == "finish")
        
        {
            transform.parent.GetComponent<SideScroller>().xSpeed = 20;
            transform.parent.Find("bonusround").gameObject.SetActive(true);
            finishSound.Play();
        } 
        else
        {
        transform.parent.GetComponent<SideScroller>().xSpeed = 0;
        transform.parent.Find("gameover").gameObject.SetActive(true);
        hitSound.Play();
        }
    }

}