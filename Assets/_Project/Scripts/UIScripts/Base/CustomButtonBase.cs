using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace Xenmentia
{
    namespace UI
    {

        [RequireComponent(typeof(Button))]
        public class CustomButtonBase : MonoBehaviour
        {
            
            [HideInInspector]
            public Button button;
            public bool delayed;
            
            public virtual void OnEnable()
            {
                button = GetComponent<Button>();
            }
            
            public void OnClickButtonAnimation(Transform t, float defaultScale)
            {
                
                float newScale = defaultScale * 1.1f;
                //GameManager.Instance.Vibrate(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
                //AudioManager.Instance.PlaySound("button_click");
                t.gameObject.GetComponent<Button>().interactable = false;
                
                t.DOScale(newScale, 0.1f).SetEase(Ease.Linear).OnComplete(() => {
                    
                    t.DOScale(defaultScale, 0.12f).SetEase(Ease.Linear).OnComplete(() => {
                        
                        t.gameObject.GetComponent<Button>().interactable = true;
                    });
                    
                });
            }
            
            public virtual void OnButtonPressed()
            {
                OnClickButtonAnimation(button.transform, button.transform.localScale.x);
            }
            
            public void OnDisable()
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }

    
    
}
