using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{   
    private Collider2D col = null;
    public static Enemy Instance;
    float a = 0, x=0, rotation, positionX,firstpositionX;
    public float moveSpeed = 2.3f, distance;    
    public float enemyHealth = 100;
    public float damage = 20f;
    private Rigidbody2D rb;

    public GameObject finishGame;
    
    public Slider slider;
    public bool moveRight;
    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }

    }
    
    void Start()
    {
        if(gameObject.name.Equals("Boss")){
            damage = 50f;
            enemyHealth = 201f;
            moveSpeed = 2f;
        }
        col = gameObject.GetComponentInChildren<PolygonCollider2D>();
        positionX = transform.position.x;
        firstpositionX = transform.position.x;
        rb = this.GetComponent<Rigidbody2D>();
        slider.maxValue = enemyHealth;
        slider.value = enemyHealth;
    }


    void Update()
    {
        if(a==0){
            EnemyMove();
        }
        
        if(enemyHealth <= 0 && gameObject.name.Equals("Boss"))
        {
            finishGame.SetActive(true);
        }

        if(enemyHealth <= 0){
            Destroy(gameObject);
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        Animator anim = GetComponent<Animator>();
        if (other.gameObject.name.Equals("Player"))
        {
            a = 1;
            if(Mathf.Abs(transform.position.x - other.transform.position.x) > distance){
                if(other.transform.position.x - transform.position.x > 0){
                    transform.localScale = new Vector2(-1.2341f,1.2341f);
                }
                else{
                    transform.localScale = new Vector2(1.2341f,1.2341f);
                }
                transform.position = Vector2.MoveTowards(transform.position, other.transform.position, moveSpeed*Time.deltaTime);           
            }
            
            
            if(Mathf.Abs(transform.position.x - other.transform.position.x) <= distance){                
                anim.SetBool("attack",false);
            }
            else{
                anim.SetBool("attack",true);               
            }
            
                      
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        a=0;
    }
    
    void EnemyMove()
    {

        // if(moveRight){
        //     transform.Translate(2*Time.deltaTime*moveSpeed,0,0);
        //     transform.localScale = new Vector2(1.2341f,1.2341f);

        // }
        // else{
        //     transform.Translate(-2*Time.deltaTime*moveSpeed,0,0);
        //     transform.localScale = new Vector2(-1.2341f,1.2341f);
            
        // }

        
        if (positionX - firstpositionX > 5f)
        {
            x = 1;
            // transform.localScale = new Vector2(1.2341f,1.2341f);
            

        }
        if (positionX - firstpositionX < 0)
        {
            x = 0;
            // transform.localScale = new Vector2(-1.2341f,1.2341f);
            
        }
        positionX = transform.position.x;

        if (x == 0 && positionX - firstpositionX < firstpositionX + 5)
        {
            transform.position = new Vector3(transform.position.x + 2f * Time.deltaTime, transform.position.y, transform.position.z);
            transform.localScale = new Vector2(-1.2341f,1.2341f);
            
        }

        else
        {
            transform.position = new Vector3(transform.position.x - 2f * Time.deltaTime, transform.position.y, transform.position.z);    
            transform.localScale = new Vector2(1.2341f,1.2341f);
            
        }


    }





}

