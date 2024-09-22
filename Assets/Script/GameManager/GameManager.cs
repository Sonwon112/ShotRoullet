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

        // 테스트용
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
            MainCamera.transform.position = Vector3.SmoothDamp(MainCamera.transform.position, cameraTargetPos.position, ref vel,0.5f); // Spawn지점으로 이동
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
        //Animation실행 부분
        Chair.GetComponent<StreamerChair>().setCanSit(true);
    }

    public void showGun()
    {
        Gun.SetActive(true);
        
    }

    public void ReloadGun()
    {
        SetGunPos(GunPos[DEALER]); // 애니메이션에서 호출
        Gun.GetComponent<Gun>().setBulletList(bulletSpawn.getCurrRoundBulletList());
        int repeatCnt = bulletSpawn.getCurrRoundBulletCnt(currRound);
        for(int i = 0; i < repeatCnt; i++)
        {
            // 장전 애니메이션
        }
        //내려놓는 애니메이션 실행
        Invoke(nameof(EndReload),3f);
    }

    
    // 내려놓는 애니메이션 종료 후 함수 호출
    public void EndReload() {
        SetGunPos(GunPos[TABLE]);// 애니메이션 호출
        // 손 원위치 애니메이션 호출
    }

    public void startGame()
    {
        Debug.Log("StartGame");
        showGun();
        //게임 룰 설명 과정
        SetGunPos(GunPos[BULLET]); // 추후 애니메이션에서 호출 예정
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
