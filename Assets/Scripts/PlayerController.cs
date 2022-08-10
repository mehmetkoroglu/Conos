using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator playerAnimator;
    public Slider slider;
    public static PlayerController Instance;
    bool isGrounded = false;
    bool facingRight = true;
    bool dialogPlant = false;

    public float playerHealth = 100, damage = 15;

    public float moveSpeed = 4f;
    public float jumpSpeed = 250f, jumpFrequency = 0.1f, nextJumpTime;

    public Transform groundCheckPosition, damageText;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;

    public GameObject deadMenu, mysticPlant;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = playerHealth;
        slider.value = playerHealth;
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        OnGroundCheck();

        if (playerHealth <= 0 || gameObject.transform.position.y <= -35)
        {
            AudioManager.soundPlay("dead2");
            gameObject.SetActive(false);
            deadMenu.SetActive(true);
        }

        if (playerRB.velocity.x < 0 && facingRight)
        {
            FlipFace();
        }
        else if (playerRB.velocity.x > 0 && !facingRight)
        {
            FlipFace();
        }

        if((Input.GetAxis("Vertical") > 0 || Input.GetKey(KeyCode.Space)) && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad))
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.gameObject.name.Equals("Acc")){
            playerHealth -= other.gameObject.GetComponentInParent<Enemy>().damage;
            slider.value = playerHealth;
            Instantiate(damageText,new Vector3(transform.position.x+0.3f,transform.position.y+0.3f,transform.position.z),Quaternion.identity).GetComponent<TextMesh>().text = Enemy.Instance.damage.ToString();
        }

        if (other.gameObject.name.Equals("Plant1") && !dialogPlant)
        {
            damage = 25;
            mysticPlant.SetActive(true);
            dialogPlant = true;
        }
    }

    public void MysticPlantClose()
    {
        mysticPlant.SetActive(false);
    }

    void Jump()
    {
        playerRB.AddForce(new Vector2(0f, jumpSpeed));
    }

    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer);
        playerAnimator.SetBool("isGroundedAnim", isGrounded);
    }

    void HorizontalMove()
    {
        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
    }

    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }
}
