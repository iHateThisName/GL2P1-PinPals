/// <summary>
/// Represents power ups in the game.
/// 
/// The Enum Names is the powerUp and the Enum Values is the powerUp number.
/// To get the power up as string use Enum.GetName(typeof(EnumPowerUp), EnumPowerUp.None) or EnumPowerUp.None.ToString()
/// </summary>

public enum EnumPowerUp
{
    None = 0,
    Shrink = 1,
    Grow = 2,
    Bomb = 3,
    SlowTime = 4,
    GravityControl = 5,
    Freeze = 6,
}
