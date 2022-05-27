using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TopDownRacer.Models;
using TopDownRacer.Sprites;

namespace TopDownRacer.Controller
{
    [XmlRoot("Map", IsNullable = false)]
    public class XmlMapReader
    {
        public static XmlMapReader LoadMap(String file)
        {
            using (var stream = File.OpenRead("../../../Maps/L-shape.xml"))
            {
                return FromStream(stream);
            }
        }

        public static XmlMapReader FromStream(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(XmlMapReader));
            return (XmlMapReader)serializer.Deserialize(stream);
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
        public Int32 Orientation;

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
                        return new Player(Game1.playerTexture[Game1.rnd.Next(Game1.playerTexture.Count)], X, Y)
                        {
                            Name = Name,
                            Input = new Input()
                            { },
                            Color = new Color(Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255), Game1.rnd.Next(0, 255)),
                        };

                    case "Bumper":
                        Debug.WriteLine("Bumper");
                        return new Bumper(Game1.bumperTexture, Orientation, Width, Height)
                        {
                            Position = new Vector2(X, Y)
                        };

                    case "Checkpoint":
                        Debug.WriteLine("Checkpoint");
                        return new Checkpoint(Game1.checkpointTexture, Orientation, Width, Height)
                        {
                            Position = new Vector2(X, Y),
                            checkpointId = CheckpointID
                        };

                    case "Finishline":
                        Debug.WriteLine("Finishline");
                        return new Finishline(Game1.finishlineTexture, Orientation, Width, Height)
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