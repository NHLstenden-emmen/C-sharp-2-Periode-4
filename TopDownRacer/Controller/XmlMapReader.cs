using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
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
            using (var stream = File.OpenRead("../../../Maps/XMLFile1.xml"))
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

        [XmlAttribute("t")]
        public String Texture;

        public Sprite Sprites
        {
            get {
                switch (classe)
                {
                    case "Player":
                        Debug.WriteLine("Player");
                        return new Player(Game1.playerTexture)
                        {
                            Name = "Simchaja",
                            Input = new Input()
                            { },
                            Color = Color.Blue,
                        };
                    default:
                        Debug.WriteLine("Bumper");
                        return new Bumper(Game1.bumperTexture, Orientation, Width, Height)
                        {
                            Position = new Vector2(X, Y)
                        };
                }
            }
        }

        //public  Rectangle { get { return new Rectangle(this.X, this.Y, this.Width, this.Height); } }
    }
}
