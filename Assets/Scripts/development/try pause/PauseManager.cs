using System.Collections.Generic;

public class PauseManager: IPauseHandler
{
    //private readonly List<IPauseHandler> handlers = new List<IPauseHandler>();
    public bool IsPaused { get; private set; }
/*    public void Register(IPauseHandler handler)
    {
        handlers.Add(handler);
    }

    public void UnRegister(IPauseHandler handler)
    {
        handlers.Remove(handler);
    }*/
    public void SetPaused(bool isPaused)
    {
        IsPaused = isPaused;
/*        foreach (IPauseHandler handler in handlers)
        {
            handler.SetPaused(isPaused);
        }*/
    }
}
