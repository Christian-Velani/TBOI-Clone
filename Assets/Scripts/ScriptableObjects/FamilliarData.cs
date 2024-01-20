using UnityEngine;

[CreateAssetMenu(fileName = "Familliar.asset", menuName = "Familliars/FamilliarObject")]
public class FamilliarData : ScriptableObject
{
    public string familliarType;
    public float speed;
    public float fireDelay;
    public GameObject bulletPreFab;
}
