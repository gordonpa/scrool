using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    static public GameObject dummyTraveller;
    static public GameObject lastPlatform;

    void Awake()
    {
        dummyTraveller = new GameObject("dummy");
    }

    public static void RunDummy()
    {
        GameObject p = Pool.singleton.GetRandom();
        if (p == null) return;

        if (lastPlatform != null)
        {
            if(lastPlatform.tag == "platformTSection")
                dummyTraveller.transform.position = lastPlatform.transform.position +
                    PlayerController.player.transform.forward * 20;
            else
                dummyTraveller.transform.position = lastPlatform.transform.position +
                    PlayerController.player.transform.forward * 10;

            if (lastPlatform.tag == "stairsUp")
                dummyTraveller.transform.Translate(0, 5, 0);
        }

        lastPlatform = p;
        p.SetActive(true);
        p.transform.position = dummyTraveller.transform.position;
        p.transform.rotation = dummyTraveller.transform.rotation;

        if (p.tag == "stairsDown")
        {
            dummyTraveller.transform.Translate(0, -5, 0);
            p.transform.Rotate(0, 180, 0);
            p.transform.position = dummyTraveller.transform.position;
        }

    }

}
