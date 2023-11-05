using UnityEngine;

public class emdDebuffer : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private GameObject player;
    [SerializeField] private Collider2D [] hitenemy;
    [SerializeField] private playerstats stats;
    [SerializeField] private float forceX;
    [SerializeField] private float forceY;
    [SerializeField] private GameObject debuffer;
    [SerializeField] private Transform groundCheck;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player.transform.localScale.x > 1)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
        }
        else
        {
            transform.localScale = new Vector2(-5, 5);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-forceX, forceY), ForceMode2D.Impulse);
        }
        Invoke("kill", 10f);
    }

    private void Update()
    {
        if(IsGrounded())
        {
            GetComponent<Animator>().speed = 1;
        }
    }

    private void kill()
    {
        Destroy(gameObject);
    }

    private void EnableDebuff()
    {
        debuffer.SetActive(true);
    }

    private void StopAnimation()
    {
        GetComponent<Animator>().speed = 0;
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(1.5f, 0.05f), 0f, stats.groundlayer);
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(1.5f, 0.05f));
    }
}
