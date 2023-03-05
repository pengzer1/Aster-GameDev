using System.Collections;
using UnityEngine;

namespace AG.CombatComponent
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth;
        [SerializeField]
        private int curHealth;
        Rigidbody rigid;
        BoxCollider boxCollider;
        Material mat;

        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
            mat = GetComponentInParent<MeshRenderer>().material;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Melee")
            {
                WeaponManager weapon = other.GetComponent<WeaponManager>();
                curHealth -= weapon.attack;

                StartCoroutine(OnDamage());
            }
        }

        IEnumerator OnDamage()
        {
            mat.color = Color.red;

            yield return new WaitForSeconds(0.1f);

            mat.color = curHealth > 0 ? Color.white : Color.gray;
        }
    }
}