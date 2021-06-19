using System;
using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace T3D
{
    public class Program
    {
        public static void Main( string[] args )
        {
            var windowSettings = GameWindowSettings.Default;
            windowSettings.RenderFrequency = 60.0f;
            windowSettings.UpdateFrequency = 60.0f;

            var nativeWindowSettings = NativeWindowSettings.Default;
            nativeWindowSettings.API = ContextAPI.OpenGL;
            nativeWindowSettings.APIVersion = new Version( 4 , 5 );
            nativeWindowSettings.AutoLoadBindings = true;
            nativeWindowSettings.Flags = ContextFlags.Debug | ContextFlags.ForwardCompatible;
            nativeWindowSettings.IsEventDriven = false;
            nativeWindowSettings.IsFullscreen = false;
            nativeWindowSettings.Location = new Vector2i( 100 , 100 );
            nativeWindowSettings.Profile = ContextProfile.Core;
            nativeWindowSettings.Size = new Vector2i( 1280 , 720 );

            try
            {
                using( var window = new Window( windowSettings , nativeWindowSettings ) )
                {
                    window.MakeCurrent();
                    window.Run();
                }
            }
            catch( GLFWException ex )
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine( ex.Message );
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine( Environment.NewLine + "Press any key to exit." );
                Console.ReadKey();

                return;
            }
        }
    }

    public class Window : GameWindow
    {
        public Window( GameWindowSettings gameWindowSettings , NativeWindowSettings nativeWindowSettings )
            : base( gameWindowSettings , nativeWindowSettings )
        {

        }
    }
}
