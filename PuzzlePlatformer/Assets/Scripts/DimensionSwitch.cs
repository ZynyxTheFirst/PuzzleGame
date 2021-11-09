using UnityEngine;

public class DimensionSwitch : MonoBehaviour
{
    [SerializeField] private GameObject normalRealm;
    [SerializeField] private GameObject shadowRealm;
    private bool _realmSwitch;

    public void UpdateDimentionSwitching()
    {
        _realmSwitch = !_realmSwitch;
    }
    void Update()
    {
        if (_realmSwitch)
        {
            normalRealm.SetActive(false);
            shadowRealm.SetActive(true);
        }
        else
        {
            shadowRealm.SetActive(false);
            normalRealm.SetActive(true);
            
        }
    }
}
