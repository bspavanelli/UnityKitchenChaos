using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjectSO", menuName = "KitchenObject/KitchenObjectSO")]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
