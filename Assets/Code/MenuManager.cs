using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] items;
    public AudioClip makeItemSound; // Assign your sound effect in the Inspector
    Transform camTrans;

    void Start()
    {
        camTrans = Camera.main.transform;
    }

    public void MakeItem(int itemNum)
    {
        if (makeItemSound != null)
        {
            Vector3 position = camTrans.position + camTrans.forward;
            AudioSource.PlayClipAtPoint(makeItemSound, position);
        }
        Vector3 instantiatePosition = camTrans.position + camTrans.forward;
        Instantiate(items[itemNum], instantiatePosition, Quaternion.identity);
    }
}