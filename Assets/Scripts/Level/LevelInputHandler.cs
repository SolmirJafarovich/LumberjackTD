using UnityEngine;

public class LevelInputHandler
{
    private readonly System.Action onRestart;

    public LevelInputHandler(System.Action onRestart)
    {
        this.onRestart = onRestart;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Regenerating level...");
            onRestart.Invoke();
        }
    }
}
