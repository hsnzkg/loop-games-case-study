namespace Project.Scripts.InputManagement
{
    public static class InputManager
    {
        private static IInputReceiver s_inputReceiver;
        public static PlayerInputData Data { get; private set; }

        public static void Initialize()
        {
            Data = new PlayerInputData();
            s_inputReceiver = new MobileInputReceiver(Data);
        }

        public static void Enable()
        {
            Data.Reset();
        }
        
        public static void Disable()
        {
            Data.Reset();
        }

        public static void Dispose()
        {
            Data.Dispose();
        }
    }
}