using UnityEngine;

[CreateAssetMenu(fileName = "BurningRecipeSO", menuName = "BurningRecipe/BurningRecipeSO")]
public class BurningRecipeSO : ScriptableObject {
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burningTimeMax;
}
