internal class GameOverState : ISceneState
{
    public GameOverState(SceneStateController controller) : base(controller)
    {
        sceneName = SceneStateEnum.GameOver.ToString();
    }
}