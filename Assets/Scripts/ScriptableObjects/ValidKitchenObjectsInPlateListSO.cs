using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ValidKitchenObjectsInPlateListSO", menuName = "ValidKitchenObjectsInPlateList/ValidKitchenObjectsInPlateListSO")]
public class ValidKitchenObjectsInPlateListSO : ScriptableObject {
    public List<KitchenObjectSO> validKitchenObjectsInPlateList;
}
