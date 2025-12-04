using UnityEngine;

public class DusmeAlgilayici : MonoBehaviour
{
    public Transform baslangicNoktasi;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>().enabled = false;

            other.transform.position = baslangicNoktasi.position;

            other.GetComponent<CharacterController>().enabled = true;

        }
    }
}
