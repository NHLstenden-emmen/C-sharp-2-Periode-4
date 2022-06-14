using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TopDownRacer.Models
{
    public class Input
    {
        public List<Keys> Left = new List<Keys>() { Keys.A, Keys.Left, Keys.J, Keys.NumPad4 };
        public List<Keys> Right = new List<Keys>() { Keys.D, Keys.Right, Keys.L, Keys.NumPad6 };
        public List<Keys> Up = new List<Keys>() { Keys.W, Keys.Up, Keys.I, Keys.NumPad8 };
        public List<Keys> Down = new List<Keys>() { Keys.S, Keys.Down, Keys.K, Keys.NumPad5 };
        public Keys Pauze = Keys.P;
    }
}