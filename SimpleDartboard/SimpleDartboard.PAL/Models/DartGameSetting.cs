namespace SimpleDartboard.PAL.Models
{
    public class DartGameSetting
    {
        public DartGameSetting()
        {
            PlayerOne = new PlayerGameSetting();
            PlayerTwo = new PlayerGameSetting();
        }
        public PlayerGameSetting PlayerOne;
        public PlayerGameSetting PlayerTwo;
    }
}