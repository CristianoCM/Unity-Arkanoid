using UnityEngine;

public class Level : MonoBehaviour
{
    // State Variables
    int breakableBlocks;

    // Cached Component References
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void increaseBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void decreaseBreakableBlocks()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
