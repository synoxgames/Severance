using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour {

    public NumberType binType;
    private EmotionHandiler emotions = new EmotionHandiler();
    public Animator anim;

    public void ChangeOpenStatus(bool value) {
        BinManager.instance.hasBinOpen = value;
        anim.SetBool("isOpened", value);
    }


    int WOE = 0;
    int DREAD = 0;
    int FROLIC = 0;
    int MALICE = 0;



}