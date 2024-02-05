using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


[System.Serializable]
public class PlayerData{

    public bool firstOpen;
    public bool isTutorialCompleted;
    public int openCount;
    public bool muteSFX;
    public bool vibrateOn;   
    public bool isRateUs;   
    public string version;
    public int level;
       

    public PlayerData(bool setFirstOpen = false, bool setTutorialComplete = false,int setOpenCount = 0, 
                        string setVersion = "0.0.1", bool vBool = true, bool _rateUs = false, int lvl = 1)
    {
        this.firstOpen = setFirstOpen;
        this.isTutorialCompleted = setTutorialComplete;
        this.openCount = setOpenCount;    
        this.version = setVersion;
        this.vibrateOn = vBool;
        this.isRateUs = _rateUs;
        this.level = lvl;
    }

}
