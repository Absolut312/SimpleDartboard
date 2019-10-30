namespace SimpleDartboard.PAL.Models
{
    public class DartGameSetting
    {
        public DartGameSetting()
        {
            PlayerOne = new PlayerGameSetting();
            PlayerOne.IsFirstSelected = true;
            PlayerTwo = new PlayerGameSetting();
            PlayerTwo.IsFirstSelected = false;
        }
        public PlayerGameSetting PlayerOne;
        public PlayerGameSetting PlayerTwo;
    }
}