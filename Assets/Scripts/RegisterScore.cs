using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RegisterScore : MonoBehaviour
{
    void Awake()
    {
        // 确保 GameData 已初始化（Awake 顺序可能不同）
        if (GameData.singleton != null)
        {
            GameData.singleton.scoreText = GetComponent<TMP_Text>();
            Debug.Log("ScoreText assigned: " + (GameData.singleton.scoreText != null));
        }
        else
        {
            Debug.LogError("GameData.singleton is null! 检查是否挂载了 GameData 脚本并打了 'gamedata' Tag");
        }
    }
}
