using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TopDownRacer.Models;
using TopDownRacer.Sprites;
using TopDownRacer.States;

namespace TopDownRacer.Controller
{
    [XmlRoot("Map", IsNullable = false)]
    public class XmlMapReader
    {
        public static XmlMapReader LoadMap(String file)
        {
            using (var stream = File.OpenRead("Maps/" + file + ".xml"))
            {
                return FromStream(stream);
            }
        }

        public static XmlMapReader FromStream(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(XmlMapReader));
            return (XmlMapReader)serializer.Deserialize(stream);
        }
        [XmlAttribute("x")]
        public int x;
        [XmlAttribute("y")]
        public int y;
        [XmlAttribute("o")]
        public int Orientation;
        internal Vector2 getSpawnpoint()
        {
            return new Vector2(x, y);
        }
        internal float getOrientation()
        {
            return MathHelper.ToRadians(Orientation);
        }

        [XmlElement("sprite")]
        public List<SpriteXml> Sprites;

        public List<Sprite> getSprites()
        {
            List<Sprite> spritelist = new List<Sprite>();
            foreach (var sprite in Sprites)
            {
                spritelist.Add(sprite.Sprites);
            }
            return spritelist;
        }
    }

    public class SpriteXml
    {
        [XmlAttribute("c")]
        public String classe;

        [XmlAttribute("x")]
        public Int32 X;

        [XmlAttribute("y")]
        public Int32 Y;

        [XmlAttribute("w")]
        public Int32 Width;

        [XmlAttribute("h")]
        public Int32 Height;

        [XmlAttribute("o")]
        public Int32 Orientation = 0;

        [XmlAttribute("n")]
        public String Name;

        [XmlAttribute("i")]
        public int CheckpointID;

        [XmlAttribute("a")]
        public int AmountOfCheckpoints;

        public Sprite Sprites
        {
            get
            {
                switch (classe)
                {
                    case "Player":
                        Debug.WriteLine("Player");
                        return new Player(State.playerTexture[Game1.rnd.Next(State.playerTexture.Count)], X, Y)
                        {
                            Name = Name,
                            Input = new Input()
                            { },
                            Color = new Color(Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255)),
                            Rotation = MathHelper.ToRadians(Orientation)
                        };

                    case "Bumper":
                        Debug.WriteLine("Bumper");
                        return new Bumper(State.bumperTexture, Orientation, Width, Height)
                        {
                            Position = new Vector2(X, Y)
                        };

                    case "Checkpoint":
                        Debug.WriteLine("Checkpoint");
                        return new Checkpoint(State.checkpointTexture, Orientation, Width, Height)
                        {
                            Position = new Vector2(X, Y),
                            checkpointId = CheckpointID
                        };

                    case "Finishline":
                        Debug.WriteLine("Finishline");
                        return new Finishline(State.finishlineTexture, Orientation, Width, Height)
                        {
                            Position = new Vector2(X, Y),
                            amountCheckpoint = AmountOfCheckpoints
                        };

                    default:
                        throw new Exception();
                }
            }
        }
    }
}