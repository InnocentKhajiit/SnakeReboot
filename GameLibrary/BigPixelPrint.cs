﻿using SquareRectangle;
using System;
using ToolsLibrary;

namespace GameLibrary
{
    public abstract class ConsoleWriter : DrawnRectangle<SignConsole>, IWriter
    {
        public abstract int Length { get; }

        public ConsoleWriter(int width, int height, ICoordPrint<SignConsole> location) : base(width, height, location) { }
        public abstract void WriteLine(string value);
    }
    public class BigPixelPrint : ConsoleWriter
    {
        Letters Library { get; }
        public override int Length { get; }
        public BigPixelPrint(int width, int height, ICoordPrint<SignConsole> location, Letters library) : base(width, height, location)
        {
            if(Height < 5 || Width < 5)
            {
                throw new Exception("неверные размеры формы");
            }
            Library = library;
            Length = Width / 6;
        }
        void PrintChar(char value, int position)
        {
            Coord coord = (position * 6, 0);
            for(int i = 0; i < 5; i++)
            {
                var charValue = Library[value];
                for (int j = 0; j < 5; j++)
                {
                    Location.Print(coord + (1 + i, j), charValue[i + j * 5] ? new SignConsole(' ', ConsoleColor.White) : new SignConsole(' '), this);
                }
            }
        }
        public override void WriteLine(string str)
        {
            str = str.ToUpper();
            for(int i = 0; i < Math.Min(str.Length, Length); i++)
            {
                PrintChar(str[i], i);
            }
        }

        public override void Load() { }

        public override void Close()
        {
            var nullValue = new string(' ', Length);
            WriteLine(nullValue);
        }

        public override void Hide()
        {
            var nullValue = new string(' ', Length);
            WriteLine(nullValue);
        }
    }
}