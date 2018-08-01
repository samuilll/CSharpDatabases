using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XmlDEmo
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryDto[] libraries = GetLibraries();

            XmlSerializer serializer = new XmlSerializer(typeof(LibraryDto[]));

            using (var streamWriter = new StreamWriter("libraries.xml"))
            {
                serializer.Serialize(streamWriter,libraries);
            }


            using (var areader = new StreamReader("libraries.xml"))
            {
                var deserializedLibraries = (LibraryDto[])serializer.Deserialize(areader);

                Console.WriteLine(deserializedLibraries.First().Section.Books.First().Name);
                ;
            }

            string reader;

            using (var streamReader = new StreamReader("libraries.xml"))
            {
                reader = streamReader.ReadToEnd();
            }

            XDocument document = XDocument.Load("libraries.xml");


            var elements = document.Root.Elements();


            foreach (var library  in elements)
            {

                Console.WriteLine(library.Element("Name").Value); ;

                var section =library.Element("Section");

                //var books = document.Root.Element("Library").Element("Section").Element("Books").Elements("Book").ToList();

                 var books = section.Element("Books").Elements("Book").ToList();

                 var bookAuthors = books.Select(b=>b.Element("BookAuthor").Value).ToList();

                 Console.WriteLine(string.Join(Environment.NewLine, bookAuthors));

                ;
            }
           // var books = document.Root.Element("Library").Element("Section").Element("Books");
        //  var p =  document.Root.Elements().Where(x => x.Element("Library").Element("Section").Element("books").Element("book").Element("BookAuthor").Value == "Stephen King").FirstOrDefault();

            ;

            ;

        }

        private static LibraryDto[] GetLibraries()
        {
            LibraryDto firstLibrary = new LibraryDto
            {
                LibraryName = "Jo Bowl",
                Section = new SectionDto()
                {
                    Name = "Horror",
                    Books = new BookDto[]
                    {
                        new BookDto() {
                            Name = "It",
                            Author = "Stephen King", Description = "Here you can put description about the book"
                        },
                        new BookDto() {
                            Name = "Frankenstein",
                            Author = "Mary Shelley", Description = "Here you can put description about the book"
                        }
                    }
                },
                CardPrice = 20.30m
            };

            LibraryDto secondLibrary = new LibraryDto
            {
                LibraryName = "Kevin Sanchez",
                Section = new SectionDto()
                {
                    Name = "Comedy",
                    Books = new BookDto[]
                    {
                        new BookDto()
                        {
                            Name = "The Diary of a Nobody",
                            Author = "George Grossmith and Weeden Grossmith",
                            Description = "Here you can put description about the book"
                        },
                        new BookDto()
                        {
                            Name = "Queen Lucia",
                            Author = "E F Benson",
                            Description = "Here you can put description about the book"
                        }
                    }
                },
                CardPrice = 43.35m
            };

            return new LibraryDto[] { firstLibrary, secondLibrary };
        }
    }
}
