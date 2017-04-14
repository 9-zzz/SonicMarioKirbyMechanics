using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KirbyController : MonoBehaviour
{
    public static KirbyController S;

    public Rigidbody2D rb2d;

    // Monster interaction variables. Put in separate script?
    public int monsterInMouthID = 0;

    public GameObject puffProjectile;
    public GameObject monsterProjectile;

    public float suckDistance = 5.0f;
    public float suckSpeed = 5.0f;
    public float speed = 10.0f;
    public float jumpSpeed = 20.0f;
    public float puffSpeed = 5.0f;

    public KeyCode suckKey = KeyCode.X;
    public KeyCode jumpKey = KeyCode.Z;
    public KeyCode puffKey = KeyCode.UpArrow;
    public KeyCode absorbKey = KeyCode.DownArrow;
    public KeyCode ditchKey = KeyCode.Space;

    public bool puffed = false;
    public bool canMove = true;
    public bool canJump = true;
    public bool isSucking = false;
    public bool inPostPuffFall = false;
    public bool monsterInMouth = false;
    public bool hasMonsterAbility = false;

    Animator anim;
    GameObject mouth;
    Vector3 targetMouthScale;
    ParticleSystem mouthPS;
    SpriteRenderer bodySprite;
    Color originalColor;

    float xinput;

    public AudioClip jumpSound;
    public AudioClip puffOutSound;

    void Awake()
    {
        S = this;
        rb2d = GetComponent<Rigidbody2D>();
        mouth = transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
        bodySprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        originalColor = bodySprite.color;
    }

    // Use this for initialization
    void Start()
    {
        targetMouthScale = mouth.transform.localScale;
        mouth.transform.localScale = Vector2.zero;
        mouthPS = transform.GetChild(2).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(suckKey) && !monsterInMouth && !inPostPuffFall && !hasMonsterAbility)
        {
            isSucking = true;
            canMove = false;
            canJump = false;

            rb2d.velocity = new Vector2(0, rb2d.velocity.y);

            mouth.transform.localScale = Vector2.MoveTowards(mouth.transform.localScale, targetMouthScale, Time.deltaTime * 3);

            RaycastHit2D hit = Physics2D.Raycast(mouth.transform.position, (mouth.transform.right * transform.localScale.x), suckDistance);
            Debug.DrawLine(mouth.transform.position, hit.point, Color.blue);

            if (hit)
            {
                if (hit.collider.gameObject.tag == "kirbyFood")
                {
                    // Should be using rigidbody code to do this. maybe effector or other fancy unity physics2d stuff?
                    hit.collider.gameObject.transform.position = Vector3.MoveTowards(hit.collider.gameObject.transform.position, transform.position, 
                        Time.deltaTime * suckSpeed);

                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }

        }
        else
        {
            mouth.transform.localScale = Vector2.MoveTowards(mouth.transform.localScale, new Vector2(0.3f, 0), Time.deltaTime * 3);
        }

        if (Input.GetKeyDown(suckKey) && !puffed && !monsterInMouth && !hasMonsterAbility)
        {
            mouthPS.Play();
            inPostPuffFall = false; // Recover from the initial puffProjectile shooting.
        }

        // Let go of suck key. Leave isSucking state.
        if (Input.GetKeyUp(suckKey) && !puffed)
        {
            canMove = true;
            //canJump = true;
            isSucking = false;

            mouthPS.Clear();
            mouthPS.Stop();
        }

        // Shoots air puff projectile.
        if (Input.GetKeyDown(suckKey) && puffed)
        {
            inPostPuffFall = true; // Kirby can't immediately go into a sucking state right after shooting a puffProjectile.

            canMove = true;

            Instantiate(puffProjectile, mouth.transform.position, Quaternion.identity);
            TogglePuffedState();
        }

        // What if it's a hold the button down ability? 
        if (Input.GetKeyDown(suckKey) && hasMonsterAbility)
        {
            // Do monster stuff. maybe put this in another script.
        }

        if (!isSucking)
        {
            // Regular jump.
            if (Input.GetKeyDown(jumpKey) && canJump)
            {
                //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed); // Keeps sideways momentum.
                rb2d.velocity = new Vector2(0, jumpSpeed);
                canJump = false;
            }

            // Kirby's regular jump is unaffected by a monster in mouth, but can't puff.
            if (!monsterInMouth)
            {
                // Initial puff. Fill body with air. Entered puffed state.
                if (Input.GetKeyDown(puffKey))
                {
                    rb2d.velocity = new Vector2(0, puffSpeed);

                    AudioSource.PlayClipAtPoint(jumpSound, transform.position);

                    if (!puffed)
                        TogglePuffedState();
                }

                // Puff jumps. Shorter jumps in puffed state. Less gravity.
                if (Input.GetKeyDown(jumpKey) && puffed)
                    rb2d.velocity = new Vector2(0, puffSpeed);

                // If in the puffed state and holding up (puff) key. Float forever upwards.
                if (Input.GetKey(puffKey) && puffed)
                    rb2d.velocity = new Vector2(0, puffSpeed);

                // ENTER HOLDING DOWN JUMP SLIDE THING
            }
        }

        if (monsterInMouth)
        {
            if (Input.GetKeyDown(suckKey))
                ExhaleMonster();

            if (Input.GetKeyDown(absorbKey))
                AcquireMonsterAbility();
        }

        if (Input.GetKeyDown(ditchKey) && hasMonsterAbility)
            DitchMonsterAbility();

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Explains 'FixedUpdate' -> http://unity3d.com/learn/tutorials/topics/scripting/update-and-fixedupdate
    void FixedUpdate()
    {
        // Explains 'GetAxis' -> https://unity3d.com/learn/tutorials/topics/scripting/getaxis
        xinput = Input.GetAxis("Horizontal");

        // Now cannot change direction you're facing while sucking.
        if (xinput != 0 && !isSucking)
            transform.localScale = new Vector2(Mathf.Sign(xinput) * transform.localScale.y, transform.localScale.y);

        if (canMove)
            rb2d.velocity = new Vector2(xinput * speed, rb2d.velocity.y);

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
            canJump = true;
    }

    void TogglePuffedState()
    {
        puffed = !puffed;

        //print(puffed);

        // Larger slower.
        if (puffed)
        {
            transform.localScale *= 2;
            speed /= 2;
            rb2d.gravityScale /= 2;
        }

        // Smaller faster.
        if (puffed == false)
        {
            AudioSource.PlayClipAtPoint(puffOutSound, transform.position);
            transform.localScale /= 2;
            speed *= 2;
            rb2d.gravityScale *= 2;
        }
    }

    // Happens when the monster COLLIDES with Kirby.
    public void InhaleMonster(int monsterID)
    {
        monsterInMouthID = monsterID;
        monsterInMouth = true;
        transform.localScale *= 2;
        speed /= 2;
        isSucking = false;              // Careful in order of setting bools....? :(
        canMove = true;
        canJump = true;                 // Will also be set to true when you let go of the suck key... bug?
        mouthPS.Stop();

        anim.SetBool("monsterInMouth", true);
    }

    public void ExhaleMonster()
    {
        monsterInMouth = false;
        transform.localScale /= 2;
        speed *= 2;
        Instantiate(monsterProjectile, mouth.transform.position, Quaternion.identity);

        inPostPuffFall = true;

        anim.SetBool("monsterInMouth", false);
    }

    void AcquireMonsterAbility()
    {
        monsterInMouth = false;
        transform.localScale /= 2;
        speed *= 2;

        anim.SetBool("monsterInMouth", false);

        //inPostPuffFall = true; // ???

        // monsterID is set in InhaleMonster()
        // Do stuff here...
        if (monsterInMouthID == 0) // BLUE
        {
            bodySprite.color = Color.blue;
        }

        if (monsterInMouthID == 1) // RED 
        {
            bodySprite.color = Color.red;
        }

        hasMonsterAbility = true;
    }

    void DitchMonsterAbility()
    {
        print("hi");
        bodySprite.color = originalColor;
        hasMonsterAbility = false;
    }

}
