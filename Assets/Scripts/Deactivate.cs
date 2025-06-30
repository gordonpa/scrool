using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    bool deactivateScheduled = false;
    void OnCollisionExit(Collision player)
    {
        if (PlayerController.isDead) return;
        if (player.gameObject.tag == "Player" && !deactivateScheduled)
        {
            Invoke("SetInactive", 4.0f);
            deactivateScheduled = true;
        }

    }

    void SetInactive()
    {
        deactivateScheduled = false;
        this.gameObject.SetActive(false);
    }
}
