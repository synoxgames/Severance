using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinManager : MonoBehaviour
{
    public static BinManager instance;
    public bool hasBinOpen = false;

    public void Awake() {
        if (instance != this) Destroy(instance);
        instance = this;
    }

    public Bin[] bin = new Bin[5];

   public Bin GetBin(int index) {
        return bin[index];
    }

}
