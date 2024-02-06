using System;

public static class EventSystem
{
    public static Action OnStartGame;
    public static void CallStartGame() => OnStartGame?.Invoke();

    public static Action<GameResult> OnGameOver;
    public static void CallGameOver(GameResult gameResult) => OnGameOver?.Invoke(gameResult);

    public static Action OnNewLevelLoad;
    public static void CallNewLevelLoad() => OnNewLevelLoad?.Invoke();

    public static Action<int> OnHpBarChange;
    public static void CallHpBarChange(int hpValue) => OnHpBarChange?.Invoke(hpValue);

    public static Action<int,float> OnShieldBarChange;
    public static void CallShieldBarChange(int shiledValue, float duration) => OnShieldBarChange?.Invoke(shiledValue, duration);
    
    public static Action<int, float> OnNitroBarChange;
    public static void CallNitroBarChange(int nitroValue, float duration) => OnNitroBarChange?.Invoke(nitroValue, duration);

    public static Action<int, float> OnMagnetBarChange;
    public static void CallMagnetBarChange(int magnetValue, float duration) => OnMagnetBarChange?.Invoke(magnetValue, duration);
}
