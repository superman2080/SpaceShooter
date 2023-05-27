using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float speed;
    public float killTime;
    public GameObject sparkEffect;
    public PlayerCtrl player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletMove());
    }

    private IEnumerator BulletMove()
    {
        float dT = 0;

        while (true)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            dT += Time.deltaTime;
            yield return null;
            if (dT > killTime)
                break;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BULLET"))
            return;
        if(collision.gameObject.GetComponent<MonsterCtrl>() != null)
        {
            collision.gameObject.GetComponent<MonsterCtrl>().hp -= player.attDamage;
        }
        ContactPoint cp = collision.GetContact(0);

        GameObject sE = Instantiate(sparkEffect, cp.point, Quaternion.LookRotation(-cp.normal));
        Destroy(sE, 0.4f);
        Destroy(gameObject);
    }
}
