using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CanvasGroup))]
public class UIPanel : MonoBehaviour
{
    [HideInInspector]
    public CanvasGroup canvasGroup;

    public virtual void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Open()
    {
        //canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 1;
    }

    public virtual void Close()
    {
        canvasGroup.alpha = 0;
        //canvasGroup.gameObject.SetActive(false);
    }

    //public virtual IEnumerator ShowAndHide()
    //{
    //    Open();

    //    yield return new WaitForSeconds(1f);

    //    Close();
    //}
}
