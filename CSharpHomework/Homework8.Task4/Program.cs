using System;
using System.Xml.Linq;

namespace Homework8.Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter person data:");
            Console.Write("Enter full name: ");
            String fullname = Console.ReadLine() ?? String.Empty;
            Console.Write("Enter street: ");
            String street = Console.ReadLine();
            Console.Write("Enter house number: ");
            Int32 houseNumber = Int32.Parse(Console.ReadLine() ?? String.Empty);
            Console.Write("Enter flat number: ");
            Int32 flatNumber = Int32.Parse(Console.ReadLine() ?? String.Empty);
            Console.Write("Enter mobile phone number: ");
            String mobilePhone = Console.ReadLine();
            Console.Write("Enter flat phone number: ");
            String flatPhone = Console.ReadLine();
            XDocument document = new XDocument(new XElement("Person",
                                                            new XAttribute("name", fullname),
                                                            new XElement("Address",
                                                                         new XElement("Street", street),
                                                                         new XElement("HouseNumber", houseNumber),
                                                                         new XElement("FlatNumber", flatNumber)),
                                                            new XElement("Phones",
                                                                         new XElement("MobilePhone", mobilePhone),
                                                                         new XElement("FlatPhone", flatPhone))));
            document.Save("output.xml");
            Console.WriteLine("That's all folks !!!");
            Console.ReadLine();
        }
    }
}
