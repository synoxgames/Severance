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
        if (isHoldingDown && !Input.GetKey(KeyCode.Mouse0)) ClearList();
        isHoldingDown = Input.GetKey(KeyCode.Mouse0);
        isDeleting = Input.GetKey(KeyCode.Delete);
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
