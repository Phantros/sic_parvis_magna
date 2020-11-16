using System;

namespace sic_parvis_magna
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new sic_parvis_magna())
                game.Run();
        }
    }
#endif
}
