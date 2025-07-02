// EnemyView.cs
using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] protected EnemyScriptableObject enemyData;
    [SerializeField] protected Transform castPosition;
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected Transform attackPosition;
    [SerializeField] protected Transform playerTransform;

    protected Animator enemyAnimator;
    protected Rigidbody2D rb2D;
    protected Vector3 baseScale;
    protected EnemyController controller;

    public Transform CastPosition => castPosition;
    public Rigidbody2D Rigidbody => rb2D;
    public Animator EnemyAnimator => enemyAnimator;
    public Transform PlayerCheck => playerCheck;
    public Transform AttackPosition => attackPosition;
    public EnemyController Controller => controller;
    public Transform PlayerTransform => playerTransform;


    protected virtual void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;

        controller = new EnemyController(this, enemyData, playerTransform);
    }

    protected virtual void Update()
    {
        controller.UpdateController();
    }

    protected virtual void FixedUpdate()
    {
        controller.FixedUpdateController();
    }

    public void FlipDirection(bool faceRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceRight ? Mathf.Abs(baseScale.x) : -Mathf.Abs(baseScale.x);
        transform.localScale = scale;
    }

    protected virtual void OnDrawGizmos()
    {
        if (enemyData != null && attackPosition != null)
        {
            Gizmos.DrawWireSphere(attackPosition.position, enemyData.attackRadius);
        }
    }

    public void AnimationAttackTrigger()
    {
        controller.AnimationAttackTrigger();
    }

    public void AnimationFinishedTrigger()
    {
        controller.AnimationFinishedTrigger();
    }

    public void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}