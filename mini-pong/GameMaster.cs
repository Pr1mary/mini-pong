using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.CompilerServices;

namespace mini_pong
{
    class GameMaster
    {
        private static Random rand = new Random();
        /// <summary>
        /// reset game
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="objTarget"></param>
        /// <param name="ballPos"></param>
        /// <param name="randBool1"></param>
        /// <param name="randBool2"></param>
        /// <param name="multiplier"></param>
        /// <param name="gameTimer"></param>
        public static void GameReset(GraphicsDeviceManager graphics, String objTarget, Vector2 ballPos, bool randBool1, bool randBool2, float multiplier, double gameTimer)
        {
            //unsafe
            //{

            //}
            Vector2 _ballPos = ObjPos(objTarget, graphics);
            ballPos = _ballPos;
            randBool1 = RandBool();
            randBool2 = RandBool();
            multiplier = (float)RandMult(2);
            gameTimer = 0f;
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
        ///<summary>
        /// fill the multiplier for desired text position
        ///</summary>
        public static Vector2 TextPos(int xPos, int yPos, GraphicsDeviceManager graphics)
        {
            Vector2 pos = new Vector2(xPos * (graphics.PreferredBackBufferWidth / 8), yPos * graphics.PreferredBackBufferHeight / 8);
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
        /// <summary>
        /// will random the multiplier needed for the game
        /// </summary>
        /// <param name="maxMult"></param>
        /// <returns></returns>
        public static double RandMult(int maxMult)
        {
            return rand.Next(1, maxMult);
        }
    }
}
