using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GunCtrl nowWeapon;
    private GunCtrl lastWeapon;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        lastWeapon = nowWeapon;
        if (nowWeapon != null)
            nowWeapon.InitialWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GunRay();
            if (nowWeapon == null)
                return;
            nowWeapon.WeaponBehavior();
        }
        if (nowWeapon == null)
            return;
        if (lastWeapon != nowWeapon)
        {
            lastWeapon = nowWeapon;
            nowWeapon.InitialWeapon();
        }
    }
    private void GunRay()
    {
        //만약 내가 보는 곳에서 총이 존재하다면
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 2))
        {
            if (hit.transform.CompareTag("GUN"))
            {
                Vector3 originPos = hit.transform.position;
                if (nowWeapon != null)
                    Instantiate(Resources.Load("Prefabs/" + nowWeapon.gunName) as GameObject, originPos, Quaternion.identity);              //기존의 총과 바꾸기 위해 생성
                nowWeapon = hit.transform.GetComponent<GunCtrl>();
            }
        }
    }
}
