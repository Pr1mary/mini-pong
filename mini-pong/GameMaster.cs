using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace mini_pong
{
    class GameMaster
    {
        private static Random rand = new Random();

        public static Vector2 ObjCenter(GraphicsDeviceManager graphics)
        {
            Vector2 center = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            return center;
        }
        ///<summary>
        /// fill "Player1" for player one, "Player2" for player two, and "Ball" for ball object in objName
        ///</summary>
        public static Vector2 ObjPos(string objName, GraphicsDeviceManager graphics)
        {
            int multiplier;
            switch (objName)
            {
                case "Player1":
                    multiplier = 1;
                    break;
                case "Player2":
                    multiplier = 7;
                    break;
                case "Ball":
                    multiplier = 4;
                    break;
                default:
                    multiplier = 4;
                    break;
            }
            Vector2 pos = new Vector2(multiplier * (graphics.PreferredBackBufferWidth / 8), graphics.PreferredBackBufferHeight / 2);
            return pos;
        }
        /// <summary>
        /// return random boolean
        /// </summary>
        /// <returns></returns>
        public static bool RandBool()
        {
            double boolVal = rand.NextDouble();
            if (boolVal >= .5)
                return true;
            else
                return false;
        }

        public static double RandMult(int maxMult)
        {
            return rand.Next(1, maxMult);
        }
    }
}
