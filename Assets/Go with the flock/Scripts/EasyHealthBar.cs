using System.Collections;
using UnityEngine;
using Pixelplacement;

public class EasyHealthBar : MonoBehaviour
{
    
    [SerializeField]
    GameObject barPrefab;
    public Vector2 offset = Vector2.up;

    Transform bar;
    Vector3 initialBarScale;

    FlockingMember _fm;
    FlockingMember FlockingMember { get
        {
            if (_fm == null)
                _fm = GetComponent<FlockingMember>();
            return _fm;
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        bar.localPosition = offset;
        int currentHP = FlockingMember.stats.health;
        int maxHP = FlockingMember.stats.maxHealth;

        updateHealth(currentHP /(float) maxHP);
    }

    void updateHealth( float t)
    {
        Vector3 newSc = new Vector3(t * initialBarScale.x, initialBarScale.y, initialBarScale.z);
        bar.localScale = newSc;
    }

    public void Initialize()
    {
        bar = Instantiate(barPrefab, transform).transform;
        initialBarScale = bar.localScale;
    }

}