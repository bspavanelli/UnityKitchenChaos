using System;
using System.Collections;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO platesKitchenObjectSO;
    [SerializeField] private float spawnPlateTimerMax;
    [SerializeField] private int platesSpawnedAmountMax;

    private int platesSpawnedAmount;
    private float spawnPlateTimer;

    [Header("Kitchen Objects To Place In Plate")]
    [SerializeField] private ValidKitchenObjectsInPlateListSO validKitchenObjectsInPlateListSO;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0f;

            if (KitchenGameManager.Instance.IsGamePlaying() && platesSpawnedAmount < platesSpawnedAmountMax) {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, new EventArgs());
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // Player is empty handed
            if (platesSpawnedAmount > 0) {
                // There´s at least one plate here
                platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(platesKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, new EventArgs());
            }
        } else {
            // Player has something in hand
            if (CanKitchenObjectBePlacedInPlate(player.GetKitchenObject().GetKitchenObjectSO())) {
                // Object in hand can be placed in plate
                if (platesSpawnedAmount > 0) {
                    // There´s at least one plate here
                    platesSpawnedAmount--;

                    // Guardar qual objeto que está na mão
                    KitchenObjectSO oldPlayerKitchenObjectInHand = player.GetKitchenObject().GetKitchenObjectSO();

                    // Remover esse objeto
                    player.GetKitchenObject().DestroySelf();
                    // Adicionar o prato nas mãos do player
                    KitchenObject.SpawnKitchenObject(platesKitchenObjectSO, player);

                    PlateKitchenObject playerPlateKitchenObject = player.GetKitchenObject().GetPlateKitchenObject();
                    // Adicionar o objeto que estava na mão antes, no prato
                    playerPlateKitchenObject.TryAddIngredient(oldPlayerKitchenObjectInHand);
                }
            } else {
                PlateKitchenObject playerPlateKitchenObject = player.GetKitchenObject().GetPlateKitchenObject();
                if (playerPlateKitchenObject != null && !playerPlateKitchenObject.HasPlate()) {
                    if (platesSpawnedAmount > 0) {
                        // There´s at least one plate here
                        platesSpawnedAmount--;

                        playerPlateKitchenObject.SetHasPlate(true);

                        OnPlateRemoved?.Invoke(this, new EventArgs());
                    }
                }
            }
        }
    }

    IEnumerator AddIngredient(PlateKitchenObject plateKitchenObject, KitchenObjectSO kitchenObjectSO) {
        yield return null;
        plateKitchenObject.TryAddIngredient(kitchenObjectSO);
    }

    private bool CanKitchenObjectBePlacedInPlate(KitchenObjectSO kitchenObjecSOToVerify) {
        foreach (KitchenObjectSO kitchenObjectSO in validKitchenObjectsInPlateListSO.validKitchenObjectsInPlateList) {
            if (kitchenObjectSO == kitchenObjecSOToVerify) {
                return true;
            }
        }
        return false;
    }
}
