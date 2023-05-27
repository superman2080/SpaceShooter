using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class MonsterCtrl : MonoBehaviour
{
    public int hp = 100;
    [Header("Relate to attack")]
    public Transform attPoint;
    [Range(1, 5)]
    public float attRadius;
    [Range(5, 20)]
    public float attDamage;
    [Range(10, 20)]
    public float detectDist;
    public Slider hpBar;
    private bool isExecutedDeadCor;
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        isExecutedDeadCor = false;
        player = GameObject.FindWithTag("PLAYER");
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        HPBar();
        if (Vector3.Distance(transform.position, player.transform.position) < detectDist && hp > 0)
            agent.SetDestination(player.transform.position);

        if (hp > 0)
            animator.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));
        else
        {
            if (!isExecutedDeadCor)
                StartCoroutine(DeadCor(3f));
        }
    }

    public void Attack()
    {
        foreach (var item in Physics.OverlapSphere(attPoint.position, attRadius))
        {
            if (item.CompareTag("PLAYER"))
            {
                item.GetComponent<PlayerCtrl>().hp -= attDamage;
            }
        }
    }

    public void HPBar()
    {
        hpBar.value = hp / 100f;
        hpBar.transform.LookAt(player.transform);
    }

    IEnumerator DeadCor(float destroyTime)
    {
        isExecutedDeadCor = true;
        agent.velocity = Vector3.zero;
        animator.Play("die");
        float dT = 0;
        while (true)
        {
            dT += Time.deltaTime;
            if (dT > destroyTime)
                break;

            yield return null;
        }
        Destroy(gameObject);
    }
}
