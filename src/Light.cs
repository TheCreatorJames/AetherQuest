using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    public class Light
    {
        private float Power = 1000.0f;
        private Vector3 WorldPosition = new Vector3(1440 / 2, 900 / 2, .56f);
        private Color Color = Color.Red;
        private float LightRadius = 200.0f;

        private static Dungeon cDungeon;
        private static List<Light> lights;

        private static Vector2 vector;

        public static Vector2 getStartVector()
        {
            return vector;
        }

        public static Light[] getLights()
        {
            if (lights == null)
            {
                lights = new List<Light>();
            }

            if (DungeonManager.getInstance().getCurrentDungeon() == null) return lights.ToArray();

            if (cDungeon != DungeonManager.getInstance().getCurrentDungeon())
            {
                cDungeon = DungeonManager.getInstance().getCurrentDungeon();
                vector = InputManager.getInstance().getCurrentPlayer().getVector();
                lights.Clear();
            }
            else return lights.ToArray();

            
           

            TorchChunk[] chunks = DungeonManager.getInstance().getCurrentDungeon().getTorchChunks();
            

            foreach(TorchChunk chunk in chunks)
            {
                lights.Add(chunk.getLight());
            }

            return lights.ToArray();
        }

        public float getPower()
        {
            return Power;
        }

        public Vector3 getWorldPos()
        {
            return WorldPosition;
        }

        public Color getColor()
        {
            return Color;
        }

        public float getLightRadius()
        {
            return LightRadius;
        }

        internal void setVector(Vector3 vector3)
        {
            WorldPosition = vector3;
        }

        internal void setLightRadius(int p)
        {
            LightRadius = p;
        }

        internal void setPower(int p)
        {
            Power = p;
        }

        internal void setColor(Microsoft.Xna.Framework.Color color)
        {
            Color = color;
        }
    }
}
