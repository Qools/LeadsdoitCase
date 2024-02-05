using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Xenmentia
{
    namespace UI
    {
        public class CustomActionButton : CustomButtonBase
        {
            public UnityEvent action;
            public override void OnEnable()
            {
                base.OnEnable();
                button.onClick.AddListener(OnButtonPressed);
            }
            
            public override void OnButtonPressed()
            {
                base.OnButtonPressed();
                //action.Invoke();
                if (delayed)
                {
                    
                    StartCoroutine(DelayedAction());
                }
                else
                {
                    action.Invoke();
                    
                }
            }
            
            IEnumerator DelayedAction()
            {
                yield return new WaitForSeconds(0.15f);
                action.Invoke();
            }
        }

    }
}
