using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagedMentBased<T> : MonoBehaviour where T : MonoBehaviour
{
    // Destroy 여부 확인용
    //싱글톤을 제네릭으로 상속가능하게 만든 베이스 스크립트
    private static bool _ShuttingDown = false;
    private static object _Lock = new object();
    private static T _Instance;

    public static T Inst
    {
        get
        {
            // 게임 종료 시 Object 보다 싱글톤의 OnDestroy 가 먼저 실행 될 수도 있다. 
            // 해당 싱글톤을 gameObject.Ondestory() 에서는 사용하지 않거나 사용한다면 null 체크를 해주자
            if (_ShuttingDown)
            {
                Debug.Log("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
                return null;
            }

            lock (_Lock)    //Thread Safe
            {
                if (_Instance == null)
                {
                    // 인스턴스 존재 여부 확인
                    _Instance = (T)FindObjectOfType(typeof(T));
                    if (_Instance == null)
                    {
                        // 새로운 게임오브젝트를 만들어서 싱글톤 Attach
                        var singletonObject = new GameObject();
                        _Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _Instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        _ShuttingDown = true;
    }

    private void OnDestroy()
    {
        _ShuttingDown = true;
    }

    public virtual void Init()
    {

    }
}
