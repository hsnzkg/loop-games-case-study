namespace Project.Scripts.InputManagement
{
    public static class InputManager
    {
        private static IInputReceiver s_inputReceiver;
        private static PlayerInputData s_data;

        public static void Initialize()
        {
            SetData(new PlayerInputData());
            s_inputReceiver = new MobileInputReceiver(GetData());
        }

        private static void SetData(PlayerInputData value)
        {
            s_data = value;
        }

        public static PlayerInputData GetData()
        {
            return s_data;
        }

        public static void Enable()
        {
            GetData().Reset();
        }
        
        public static void Disable()
        {
            GetData().Reset();
        }

        public static void Dispose()
        {
            GetData().Dispose();
        }
    }
}