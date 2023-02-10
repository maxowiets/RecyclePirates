using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieManager : MonoBehaviour
{
    public List<Die> dieList;
    public float throwSpeed;

    public void ThrowDie(int dieNumber)
    {
        Die newDie = null;
        switch (dieNumber)
        {
            case 2:
                newDie = Instantiate(dieList[0], Camera.main.transform.position + Camera.main.transform.right * Random.Range(-5f, 5f) + Vector3.down * Random.Range(1f, 3f), Quaternion.Euler(Camera.main.transform.forward));
                break;
            case 4:
                newDie = Instantiate(dieList[1], Camera.main.transform.position + Camera.main.transform.right * Random.Range(-5f, 5f) + Vector3.down * Random.Range(1f, 3f), Quaternion.Euler(Camera.main.transform.forward));
                break;
            case 6:
                newDie = Instantiate(dieList[2], Camera.main.transform.position + Camera.main.transform.right * Random.Range(-5f, 5f) + Vector3.down * Random.Range(1f, 3f), Quaternion.Euler(Camera.main.transform.forward));
                break;
            case 8:
                newDie = Instantiate(dieList[3], Camera.main.transform.position + Camera.main.transform.right * Random.Range(-5f, 5f) + Vector3.down * Random.Range(1f, 3f), Quaternion.Euler(Camera.main.transform.forward));
                break;
            case 10:
                newDie = Instantiate(dieList[4], Camera.main.transform.position + Camera.main.transform.right * Random.Range(-5f, 5f) + Vector3.down * Random.Range(1f, 3f), Quaternion.Euler(Camera.main.transform.forward));
                break;
            case 12:
                newDie = Instantiate(dieList[5], Camera.main.transform.position + Camera.main.transform.right * Random.Range(-5f, 5f) + Vector3.down * Random.Range(1f, 3f), Quaternion.Euler(Camera.main.transform.forward));
                break;
            case 20:
                newDie = Instantiate(dieList[6], Camera.main.transform.position + Camera.main.transform.right * Random.Range(-5f, 5f) + Vector3.down * Random.Range(1f, 3f), Quaternion.Euler(Camera.main.transform.forward));
                break;
            default:
                break;
        }
        newDie.rb.velocity = Camera.main.transform.forward * throwSpeed;
        newDie.rb.angularVelocity = Random.onUnitSphere * 5f;
    }
}
