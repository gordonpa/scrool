using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoroutineRunner : MonoBehaviour
{
    // 单例，任何地方都能直接访问
    public static CoroutineRunner Instance { get; private set; }

    void Awake()
    {
        // 简单的单例写法
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);   // 切场景也不销毁
        }
        else
        {
            Destroy(gameObject);            // 防止重复
        }
    }

    // 向外界暴露一个“延时恢复”的调用接口
    public void ReactivateAfter(GameObject target, float delay)
    {
        StartCoroutine(DelayedReactivate(target, delay));
    }

    private IEnumerator DelayedReactivate(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (target != null)                // 防止物体提前被销毁
            target.SetActive(true);
    }
}