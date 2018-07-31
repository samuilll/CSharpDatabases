using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace P_02CatalogOfMusicalAlbums
{
    class StartUp
    {
        static void Main(string[] args)
        {
            XDocument doc = new XDocument();

            doc.Add(
                new XElement("albums",
                    new XElement("album",
                        new XElement("name","MaterOfPuppets"),
                        new XElement("artist","Metallica"),
                        new XElement("year","1987"),
                        new XElement("producer","EmiRecords"),
                        new XElement("price","$19"),
                        new XElement("songs",
                            new XElement("song",
                                new XElement("title","Battery"),
                                new XElement("duration","5:05")
                                        ),
                             new XElement("song",
                                new XElement("title","The thing that should"),
                                new XElement("duration","4:08")
                                          ),
                              new XElement("song",
                                new XElement("title","Sanatorium"),
                                new XElement("duration","6:10")
                                          )
                                     )
                                ),
                    new XElement("album",
                        new XElement("name","Echoes"),
                        new XElement("artist","Pink Floyd"),
                        new XElement("year","1972"),
                        new XElement("producer","KonjoREc"),
                        new XElement("price","$23"),
                        new XElement("songs",
                            new XElement("song",
                                new XElement("title","Dogs"),
                                new XElement("duration","23:00")
                                        ),
                             new XElement("song",
                                new XElement("title","Cats"),
                                new XElement("duration","4:09")
                                          ),
                              new XElement("song",
                                new XElement("title","Mouses"),
                                new XElement("duration","9:08")
                                          )
                                     )
                                  )
                ));

            doc.Save("../../../albums.xml");
        }
    }
}
