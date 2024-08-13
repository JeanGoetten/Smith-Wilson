using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static int bossLife = 10;

    public static bool bossIsAlive = true;

    private void Start()
    {
        bossIsAlive = true; 
    }

    private void Update()
    {
        if (bossLife <= 0)
        {
            StartCoroutine(BossDying()); 
        }
    }

    public void Hitted()
    {
        StartCoroutine(LifeDecreaseWithCoolDown()); 
    }
    public IEnumerator LifeDecreaseWithCoolDown()
    {
        yield return new WaitForSeconds(1);
        bossLife--; 
        Debug.Log(bossLife);
    }

    IEnumerator BossDying()
    {
        bossIsAlive = false;
        Destroy(this.gameObject);
        yield return new WaitForSeconds(5);
        ActionSceneManager.bossSafe = false; 
    }
}
