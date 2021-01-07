namespace BrickWorks
{
    using BrickWorks.Controller;

    public class StartUp
    {
        public static void Main()
        {
            //command pattern
            var engine = new Engine();
            engine.Run();
        }
    }
}
