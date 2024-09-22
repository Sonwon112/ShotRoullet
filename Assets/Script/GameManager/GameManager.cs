using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int PLAYER = 0;
    private const int VIEWER = 1;
    private const int DEALER = 2;
    private const int BULLET = 3;
    private const int TABLE = 3;
    

    public Transform[] CameraPos;
    public Transform BulletLookPos;
    public Transform[] GunPos;
    public BulletSpawn bulletSpawn;

    private Transform cameraTargetPos;
    private bool camMove = false;
    private Vector3 vel = Vector3.zero;

    public GameObject MainCamera;
    public GameObject Gun;

    private Streamer streamer;
    private ParticleSystem smokeParticle;
    private GameObject Dealer;
    private GameObject Chair;
    private int currRound = 0;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
        if(objects.Length > 0 ) 
            streamer = objects[0].GetComponent<Streamer>();

        smokeParticle = transform.Find("Smoke").gameObject.GetComponent<ParticleSystem>();
        Dealer = transform.Find("Dealer").gameObject;
        Chair = transform.Find("StreamerChair").gameObject;
    }

    // Update is called once per frame
    void Update()
    {   

        // �׽�Ʈ��
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(streamer != null)
            {
                streamer.setGameMode(GameMode.PLAY);
                //Chair.GetComponent<StreamerChair>().setCanSit(true);
            }
        }

        if (camMove)
        {
            MainCamera.transform.position = Vector3.SmoothDamp(MainCamera.transform.position, cameraTargetPos.position, ref vel,0.5f); // Spawn�������� �̵�
            if (MainCamera.transform.position == cameraTargetPos.position) { 
                camMove= false;
                if(cameraTargetPos == CameraPos[PLAYER])
                {
                    streamer.setCanCameraMove(true);
                    ReloadGun();
                }
            }
        }
    }

    public void ShowUpDealer()
    {
        smokeParticle.Play();
        Dealer.SetActive(true);
        //Animation���� �κ�
        Chair.GetComponent<StreamerChair>().setCanSit(true);
    }

    public void showGun()
    {
        Gun.SetActive(true);
        
    }

    public void ReloadGun()
    {
        SetGunPos(GunPos[DEALER]); // �ִϸ��̼ǿ��� ȣ��
        Gun.GetComponent<Gun>().setBulletList(bulletSpawn.getCurrRoundBulletList());
        int repeatCnt = bulletSpawn.getCurrRoundBulletCnt(currRound);
        for(int i = 0; i < repeatCnt; i++)
        {
            // ���� �ִϸ��̼�
        }
        //�������� �ִϸ��̼� ����
        Invoke(nameof(EndReload),3f);
    }

    
    // �������� �ִϸ��̼� ���� �� �Լ� ȣ��
    public void EndReload() {
        SetGunPos(GunPos[TABLE]);// �ִϸ��̼� ȣ��
        // �� ����ġ �ִϸ��̼� ȣ��
    }

    public void startGame()
    {
        Debug.Log("StartGame");
        showGun();
        //���� �� ���� ����
        SetGunPos(GunPos[BULLET]); // ���� �ִϸ��̼ǿ��� ȣ�� ����
        //ShowBullet();
        Invoke(nameof(ShowBullet),7f);

    }

    public void SetGunPos(Transform pos)
    {
        Gun.transform.parent = pos;
        Gun.transform.localPosition = new Vector3(0, 0, 0);
        Gun.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    public void ShowBullet()
    {
        camMove = true;
        cameraTargetPos = CameraPos[BULLET];
        streamer.setCanCameraMove(false);
        MainCamera.transform.LookAt(BulletLookPos);
        bulletSpawn.SpawnBullet();
    }

    public void ReturningCameraPos()
    {
        camMove = true;
        cameraTargetPos = CameraPos[PLAYER];
    }

    // ----------- Getter Setter
    public int getCurrRound()
    {
        return currRound;
    }
}
