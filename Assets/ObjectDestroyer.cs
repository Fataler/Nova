using Nova;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private PrefabLoader prefabLoader;

    private void Awake()
    {
        prefabLoader = GetComponentInParent<PrefabLoader>();
    }

    public void DestroyGameObject()
    {
        //Destroy(gameObject);
        if (prefabLoader != null)
        {
            prefabLoader.ClearPrefab();
        }
    }

    public void DestroyInSeconds(int seconds)
    {
        Destroy(gameObject, seconds);
    }
}
