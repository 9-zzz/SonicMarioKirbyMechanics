using UnityEngine;
using System.Collections;

public class KirbyStatus : MonoBehaviour
{
    public static KirbyStatus S;

    public GameObject kirbySpit;

    ParticleSystem mouthPS;
    GameObject mouth;
    SpriteRenderer bodySprite;
    Vector2 targetMouthScale;

    public int monsterInMouthID;

    public bool canSuck = true;

    void Awake()
    {
        S = this;

        mouth = transform.GetChild(0).gameObject;
        bodySprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        targetMouthScale = mouth.transform.localScale;
        mouth.transform.localScale = Vector2.zero;
        mouthPS = transform.GetChild(2).GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(canSuck)
        {
            if (Input.GetKey(KeyCode.V))
            {
                mouth.transform.localScale = Vector2.MoveTowards(mouth.transform.localScale, targetMouthScale, Time.deltaTime * 3);

                //RaycastHit2D hit = Physics2D.CircleCast(transform.position, 5, transform.position + new Vector3(1, 0, 0) * transform.localScale.x, 20);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position + new Vector3(7, 0, 0) * transform.localScale.x);

                if (hit.point != new Vector2(0, 0))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.blue);
                    if (hit.collider.gameObject.tag == "kirbyFood")
                    {
                        print("wee");
                        hit.collider.gameObject.transform.position = Vector3.MoveTowards(hit.collider.gameObject.transform.position, transform.position, Time.deltaTime * 3);
                    }
                }
            }
            else
            {
                mouth.transform.localScale = Vector2.MoveTowards(mouth.transform.localScale, Vector2.zero, Time.deltaTime * 3);
            }

            if (Input.GetKeyDown(KeyCode.V))
                mouthPS.Play();

            if (Input.GetKeyUp(KeyCode.V))
                mouthPS.Stop();

        } // End of canSuck if-statement

        if (!canSuck)
        {
            mouth.transform.localScale = Vector2.MoveTowards(mouth.transform.localScale, Vector2.zero, Time.deltaTime * 3);
            mouthPS.Stop();

            if (Input.GetKeyDown(KeyCode.V))
            {
                Instantiate(kirbySpit, transform.position, transform.rotation);
                KirbyMovement.S.Shrink();
                canSuck = true;
                mouthPS.Play();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
              // should probably be a switch case or something.
                if(monsterInMouthID == 0) // 0 is BLUE
                {
                    KirbyMovement.S.Shrink();
                    bodySprite.color = Color.blue;
                }

                if(monsterInMouthID == 1) // 1 is GREEN
                {
                    KirbyMovement.S.Shrink();
                    bodySprite.color = Color.red;
                }

            }
        }

    }

}
