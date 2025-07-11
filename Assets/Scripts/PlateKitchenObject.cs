using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnHasPlateChangedEventArgs> OnHasPlateChanged;
    public class OnHasPlateChangedEventArgs : EventArgs {
        public bool hasPlate;
    }

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private ValidKitchenObjectsInPlateListSO validKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;
    private bool hasPlate;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
        hasPlate = true;
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (!validKitchenObjectSOList.validKitchenObjectsInPlateList.Contains(kitchenObjectSO)) {
            // Not a valid ingredient
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
            // Already has this type
            return false;
        } else {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
    }

    public bool HasPlate() {
        return hasPlate;
    }

    public void SetHasPlate(bool hasPlate) {
        this.hasPlate = hasPlate;
        
        OnHasPlateChanged?.Invoke(this, new OnHasPlateChangedEventArgs {
            hasPlate = hasPlate
        });
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }
}
