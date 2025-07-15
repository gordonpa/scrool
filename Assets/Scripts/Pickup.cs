using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {


            GameData.singleton.UpdateScore(10);
            gameObject.SetActive(false);

            CoroutineRunner.Instance.ReactivateAfter(gameObject, 5f);

        }
    }
}
