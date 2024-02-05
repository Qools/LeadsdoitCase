using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public enum Status
    {
        wait,
        ready
    }

    [SerializeField]
    private Status status;
    public static T Instance;

    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<T>();
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Log(string _message)
    {
        string tm = string.Format("<color=white>[{0}]</color> ---> {1}", Instance.name, _message);

        Debug.Log(tm);
    }

    public void SetStatus(Status _status)
    {
        status = _status;

        Log("Status : " + status);
    }

    public Status GetStatus()
    {
        return status;
    }

    public virtual IEnumerator WaitInit(System.Action _initAction = null)
    {
        if (_initAction != null)
        {
            _initAction();
        }
        while (GetStatus() == Status.wait)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

    }

    public virtual IEnumerator WaitUntilBoolCheck(bool _boolToCheck)
    {

        while (_boolToCheck == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);

    }
}
