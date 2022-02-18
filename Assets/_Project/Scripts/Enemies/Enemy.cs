using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Properties")]
    [SerializeField] [Range(0, 10)] protected float speed;
    [SerializeField] [Range(0, 10)] protected float attackDistance;

    public int direction;

    [Header("Raycast Properties")]
    public LayerMask layerGround;
    public LayerMask layerHit;
    public float lengthGround;
    public float lengthWall;

    [Range(0, 10)] public float lengthPlayer;
    protected float playerDistance;

    public Transform rayPointGround;
    public Transform rayPointWall;
    public Transform rayPointPlayer;
    protected Transform player;

    public RaycastHit2D hitGround;
    public RaycastHit2D hitWall;
    public RaycastHit2D hitPlayer;

    protected Animator animator;

    protected Rigidbody2D rb;

    protected weapon weapon;

    private void Initialization()
    {
        player = FindObjectOfType<Player>().transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        direction = (int) transform.localScale.x;
        weapon = GetComponentInChildren<weapon>();
        
    }

    protected virtual void Awake()
    {
        Initialization();
    }

    protected virtual void Update()
    {
        GetDistancePlayer();  
    }

    protected virtual void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector2(direction, transform.localScale.y);
    }

    protected virtual RaycastHit2D RaycastGround()
    {
        hitGround = Physics2D.Raycast(rayPointGround.position, Vector2.down, lengthGround, layerGround);
        Color color = hitGround ? Color.red : Color.green;
        Debug.DrawRay(rayPointGround.position, Vector2.down * lengthGround, color);
        return hitGround;
    }

    protected virtual RaycastHit2D RaycastWall()
    {
        hitWall = Physics2D.Raycast(rayPointWall.position, Vector2.right * direction, lengthWall, layerGround);
        Color color = hitWall ? Color.black : Color.blue;
        Debug.DrawRay(rayPointWall.position, Vector2.right * direction * lengthWall, color);
        return hitWall;
    }

    protected virtual RaycastHit2D RaycastPlayer()
    {
        hitPlayer = Physics2D.Raycast(rayPointPlayer.position, Vector2.right * direction, lengthPlayer, layerHit);
        Color color = hitPlayer ? Color.green : Color.white;
        Debug.DrawRay(rayPointPlayer.position, Vector2.right * direction * lengthPlayer, color);
        return hitPlayer;
    }

    protected void GetDistancePlayer()
    {
        playerDistance = player.position.x - transform.position.x;
    }
}
