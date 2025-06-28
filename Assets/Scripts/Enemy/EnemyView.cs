using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyData;
    [SerializeField] private Transform castPosition;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform attackPosition;

    private Animator enemyAnimator;
    private Rigidbody2D rb2D;
    private Vector3 baseScale;
    private EnemyController controller;

    public Transform CastPosition => castPosition;
    public Rigidbody2D Rigidbody => rb2D;
    public Animator EnemyAnimator => enemyAnimator;
    public Transform PlayerCheck => playerCheck;
    public Transform AttackPosition => attackPosition;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;

        controller = new EnemyController(this, enemyData);
    }

    private void Update()
    {
        controller.UpdateController();
    }

    private void FixedUpdate()
    {
        controller.FixedUpdateController();
    }

    public void FlipDirection(bool faceRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceRight ? Mathf.Abs(baseScale.x) : -Mathf.Abs(baseScale.x);
        transform.localScale = scale;
        Debug.Log("FlipDirection: " + (faceRight ? "Right" : "Left"));
    }

    public void SetEnemyController(EnemyController controller)
    {
        this.controller = controller;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPosition.position, enemyData.attackRadius);
    }

    public void AnimationAttackTrigger()
    {
        controller.AnimationAttackTrigger();
    }

    public void AnimationFinishedTrigger()
    {
        controller.AnimationFinishedTrigger();
    }
}
