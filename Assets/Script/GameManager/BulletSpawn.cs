using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    private const int MAX_BULLET_CNT = 16;
    private int[] roundBulletCnt = {4, 8};
    private int currRound = 0;
    private GameManager gameManager;

    public float spawnTerm;
    public GameObject[] bulletList; // 0 : liveAmmunition, 1 : blank bullet

    private bool isSpawn = false;
    private List<int> currRoundBulletList = new List<int>();
    private List<GameObject> spawnedBulletList = new List<GameObject>();
    private int totalSize = 0;
    private int currIndex = 0;

    private float term = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawn)
        {
            term += Time.deltaTime;
            if(term > spawnTerm)
            {
                InstantiateBullet();
                term = 0f;
            }
        }
    }
    public List<int> getCurrRoundBulletList()
    {
        return currRoundBulletList;
    }

    public int getCurrRoundBulletCnt(int currRound)
    {
        return roundBulletCnt[currRound];
    }

    //애니메이션에서 실행
    public void clearBulletOnTable()
    {
        foreach(GameObject tmp in spawnedBulletList)
        {
            Destroy(tmp);
        }
    }

    void InstantiateBullet()
    {
        if(currIndex < totalSize)
        {
            int tmp = currRoundBulletList[currIndex];
            spawnedBulletList.Add(Instantiate(bulletList[tmp],transform.position,transform.rotation));
            currIndex++;
        }
        else
        {
            isSpawn = false;
            Invoke(nameof(ReturningCameraPos), 3f);
        }
    }

    public void ReturningCameraPos()
    {
        gameManager.ReturningCameraPos();
    }
    

    public void SpawnBullet()
    {
        isSpawn = true;
        currRound = gameManager.getCurrRound();
        selectBullet();
    }

    void selectBullet()
    {   
        currRoundBulletList.Clear();
        totalSize = roundBulletCnt[currRound];
        if(currRound == 0)
        {
            currRoundBulletList.Add(0);
            currRoundBulletList.Add(0);
            currRoundBulletList.Add(1);
            currRoundBulletList.Add(1);
            return;
        }
        for (int i = 0; i < totalSize; i++)
        {
            int tmp = Random.Range(0, 2);
            currRoundBulletList.Add(tmp);
        }
    }
}
