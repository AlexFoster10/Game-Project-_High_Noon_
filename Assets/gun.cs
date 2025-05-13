using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private gunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] private float smooth;
    [SerializeField] private float swayMult;

    [Header("References")]
    public timer timerScript;
    public float speedCurve;
    float timeSinceLastShot;
    Vector2 walkInput;

    [Header("Bobbing")]
    public float speedCurve1;
    float curveSin { get => Mathf.Sin(speedCurve1); }
    float curveCos { get => Mathf.Cos(speedCurve1); }

    public Vector3 travelLimit = Vector3.one * 0.025f;
    public Vector3 bobLimit = Vector3.one * 0.01f;
    Vector3 bobPosition;

    public float bobExaggeration;

    [Header("Bob Rotation")]
    public Vector3 multiplier;
    Vector3 bobEulerRotation;

    public GameObject passScreenUI;

    private void Start()
    {
        //playerShoot.shootInput += Shoot;
        gunData.currentAmmo = 1;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / gunData.fireRate / 60;

    void GetInput()
    {
        walkInput.x = Input.GetAxis("Horizontal");
        walkInput.y = Input.GetAxis("Vertical");
        walkInput = walkInput.normalized;
    }

    void mouseSway()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMult;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMult;

        Quaternion rotX = Quaternion.AngleAxis(-mouseY, Vector3Int.right);
        Quaternion rotY = Quaternion.AngleAxis(-mouseX, Vector3Int.up);

        Quaternion targRot = rotX * rotY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targRot, smooth * Time.deltaTime);

    }
    public void Shoot()
    {
        if(gunData.currentAmmo > 0)
        {
             if (CanShoot())
            {
                if(Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxD)) {

                    Debug.Log(hitInfo.transform.name);
                    if(hitInfo.transform.tag == "Target") {
                        timerScript.LevelPassed();
                    }
                    else
                    {
                        timerScript.LevelFailed();
                    }

                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0f;
                OnGunShot();
            }
        }
    }


    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward*10,Color.red);
        mouseSway();
        GetInput();


    }


    private void OnGunShot()
    {
        //not implemented
    }



}
