public abstract class IModule
{
    protected GameState gameState;
    public IModule(GameState gameState)
    {
        this.gameState = gameState;
    }
    public virtual void Initialize() { }
    public virtual void Update() { }
    public virtual void Destroy() { }
}
