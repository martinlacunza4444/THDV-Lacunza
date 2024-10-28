using UnityEngine;

[CreateAssetMenu(fileName = "AmmoData", menuName = "ScriptableObjects/AmmoDataSO", order = 1)]
public class AmmoDataSO: ScriptableObject
{
   public int ammoAmount = 30; // Cantidad de munición que otorgará
}