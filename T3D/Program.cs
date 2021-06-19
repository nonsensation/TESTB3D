using System;
using System.Collections.Generic;
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
                Logger.Fatal( ex.Message );
                Logger.Info( Environment.NewLine + "Press any key to exit." );
                Console.ReadKey();

                return;
            }
        }
    }

    public enum LogSeverity
    {
        Note,
        Info,
        Warn,
        Error,
        Fatal,
        Debug,
    }

    public static class Logger
    {
        public static void Log( string msg )
        {
            Console.WriteLine( msg );
        }

        private static readonly Dictionary<LogSeverity , (ConsoleColor bgColor, ConsoleColor fgColor)> LoggerColorMap = new() {
            [ LogSeverity.Info  ] = (ConsoleColor.Black   , ConsoleColor.Gray       ) ,
            [ LogSeverity.Note  ] = (ConsoleColor.Black   , ConsoleColor.White      ) ,
            [ LogSeverity.Warn  ] = (ConsoleColor.Black   , ConsoleColor.DarkYellow ) ,
            [ LogSeverity.Error ] = (ConsoleColor.Black   , ConsoleColor.DarkRed    ) ,
            [ LogSeverity.Fatal ] = (ConsoleColor.DarkRed , ConsoleColor.Gray       ) ,
            [ LogSeverity.Debug ] = (ConsoleColor.Black   , ConsoleColor.DarkCyan   ) ,
        };

        public static void Log( LogSeverity severity , string msg )
        {
            var (oldBackgroundColor,oldForegroundColor) = (Console.BackgroundColor,Console.ForegroundColor);

            (Console.BackgroundColor,Console.ForegroundColor) = LoggerColorMap[ severity ];

            Log( msg );

            (Console.BackgroundColor,Console.ForegroundColor) = (oldBackgroundColor,oldForegroundColor);
        }

        public static void Note( string msg ) => Log( LogSeverity.Note , msg );
        public static void Info( string msg ) => Log( LogSeverity.Info , msg );
        public static void Warn( string msg ) => Log( LogSeverity.Warn , msg );
        public static void Error( string msg ) => Log( LogSeverity.Error , msg );
        public static void Fatal( string msg ) => Log( LogSeverity.Fatal , msg );
        public static void Debug( string msg ) => Log( LogSeverity.Debug , msg );

    }

    public class Window : GameWindow
    {
        public Window( GameWindowSettings gameWindowSettings , NativeWindowSettings nativeWindowSettings )
            : base( gameWindowSettings , nativeWindowSettings )
        {

        }
    }
}
