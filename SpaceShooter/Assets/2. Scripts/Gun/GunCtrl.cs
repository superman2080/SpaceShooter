using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunCtrl : MonoBehaviour
{
    public string gunName;
    [Range(0, 100)]
    public int gunDamage;
    public Transform firePos;
    public MeshRenderer muzzleFlash;
    public GameObject muzzleLight;
    protected GameObject gun;
    protected PlayerCtrl player;
    [Range(0.1f, 0.5f)]
    public float muzzleTime;

    private void Start()
    {
        muzzleFlash.enabled = false;
        muzzleLight.SetActive(false);
    }

    public virtual void InitialWeapon()
    {
        player = GameObject.FindWithTag("PLAYER").GetComponent<PlayerCtrl>();
        if (player.weaponTr.childCount > 0)                 //기존의 총 삭제
        {
            for (int i = 0; i < player.weaponTr.childCount; i++)
            {
                Destroy(player.weaponTr.GetChild(i).gameObject);
            }
        }
        //위치, 회전, 부모 초기화
        transform.parent = player.weaponTr;
        transform.position = player.weaponTr.position;
        transform.eulerAngles = player.weaponTr.eulerAngles;
        //
        player.gameObject.GetComponent<WeaponController>().nowWeapon = this;        //갈아 끼우기
        player.attDamage = gunDamage;
        //transform.SetParent(player.weaponTr);
        //muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();

        //muzzleFlash.enabled = false;
    }

    public virtual void WeaponBehavior()
    {
        if (player != null)
            player.audioSource.PlayOneShot(player.fireSFX, 0.8f);
        StartCoroutine(ShowMuzzleFlash(muzzleTime));
    }

    protected IEnumerator ShowMuzzleFlash(float t)
    {
        //x = 0, 0.5f y = 0, 0.5f
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;

        float scale = Random.Range(0.5f, 1.5f);

        muzzleFlash.transform.localScale = Vector3.one * scale;

        float angle = Random.Range(0, 360f);
        muzzleFlash.transform.localEulerAngles = new Vector3(muzzleFlash.transform.localEulerAngles.x, muzzleFlash.transform.localEulerAngles.y, angle);
        //
        /*
         * 
            1. Quaternion 사원수(x, y, z, w)

            2. Rotation (x, y, z)

        */
        //

        muzzleFlash.enabled = true;
        muzzleLight.SetActive(true);
        yield return new WaitForSeconds(t);
        muzzleFlash.enabled = false;
        muzzleLight.SetActive(false);
    }
}
