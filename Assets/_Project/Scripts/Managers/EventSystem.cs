using System;

public static class EventSystem
{
    public static Action OnStartGame;
    public static void CallStartGame() => OnStartGame?.Invoke();

    public static Action<GameResult> OnGameOver;
    public static void CallGameOver(GameResult gameResult) => OnGameOver?.Invoke(gameResult);

    public static Action OnNewLevelLoad;
    public static void CallNewLevelLoad() => OnNewLevelLoad?.Invoke();
}
