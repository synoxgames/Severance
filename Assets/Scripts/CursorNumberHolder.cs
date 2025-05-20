using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorNumberHolder : MonoBehaviour
{
    public bool isHoldingDown = false;
    public bool isDeleting = false;
    public List<Number> numbersHeld = new List<Number>();
    public static CursorNumberHolder instance;

    public void Awake() {
        if (instance != this) Destroy(instance);
        instance = this;
    }

    private void Update() {
        if (isHoldingDown && !Input.GetKey(KeyCode.Mouse0) && !BinManager.instance.hasBinOpen) ClearList();
        isHoldingDown = Input.GetKey(KeyCode.Mouse0);
        isDeleting = Input.GetKey(KeyCode.Delete);

        if (!isHoldingDown && !BinManager.instance.hasBinOpen) ClearList();


        if (Input.GetKeyDown(KeyCode.Alpha1) && !BinManager.instance.hasBinOpen) {
            BinManager.instance.GetBin(0).ChangeOpenStatus(true);
        } else if (Input.GetKeyUp(KeyCode.Alpha1)) {
            BinManager.instance.GetBin(0).ChangeOpenStatus(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !BinManager.instance.hasBinOpen) {
            BinManager.instance.GetBin(1).ChangeOpenStatus(true);
        } else if (Input.GetKeyUp(KeyCode.Alpha2)) {
            BinManager.instance.GetBin(1).ChangeOpenStatus(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && !BinManager.instance.hasBinOpen) {
            BinManager.instance.GetBin(2).ChangeOpenStatus(true);
        } else if (Input.GetKeyUp(KeyCode.Alpha3)) {
            BinManager.instance.GetBin(2).ChangeOpenStatus(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && !BinManager.instance.hasBinOpen) {
            BinManager.instance.GetBin(3).ChangeOpenStatus(true);
        } else if (Input.GetKeyUp(KeyCode.Alpha4)) {
            BinManager.instance.GetBin(3).ChangeOpenStatus(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && !BinManager.instance.hasBinOpen) {
            BinManager.instance.GetBin(4).ChangeOpenStatus(true);
        } else if (Input.GetKeyUp(KeyCode.Alpha5)) {
            BinManager.instance.GetBin(4).ChangeOpenStatus(false);
        }
    }

    public void AddToNumbers(Number toAdd) {
        if (!isHoldingDown) return;

        toAdd.SetHoldState(true);
        numbersHeld.Add(toAdd);
    }

    public void ClearList() {
        foreach (Number n in numbersHeld) {
            n.SetHoldState(false);
            n.OnMouseExit();
        }

        numbersHeld.Clear();
    }
}
