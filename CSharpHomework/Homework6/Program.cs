using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Homework6
{
    public class Record
    {
        public Int32 Id { get; set; }
        public DateTime CreationTime { get; set; }
        public String FullName { get; set; }
        public Int32 Age { get; set; }
        public Int32 Height { get; set; }
        public DateTime BirthDate { get; set; }
        public String BirthPlace { get; set; }
    }

    public class RecordSerializer
    {
        public String Serialize(Record record)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(record.Id).Append(RecordDelimiter);
            builder.Append(record.CreationTime.ToString(CreationTimeFormat)).Append(RecordDelimiter);
            builder.Append(record.FullName).Append(RecordDelimiter);
            builder.Append(record.Age).Append(RecordDelimiter);
            builder.Append(record.Height).Append(RecordDelimiter);
            builder.Append(record.BirthDate.ToString(BirthDateFormat)).Append(RecordDelimiter);
            builder.Append(record.BirthPlace);
            return builder.ToString();
        }

        public Record Deserialize(String source)
        {
            String[] fields = source.Split(RecordDelimiter);
            if (fields.Length != FieldsCount)
                throw new InvalidOperationException("Bad format of source");
            Record record = new Record();
            record.Id = Int32.Parse(fields[0]);
            record.CreationTime = DateTime.ParseExact(fields[1], CreationTimeFormat, CultureInfo.InvariantCulture);
            record.FullName = fields[2];
            record.Age = Int32.Parse(fields[3]);
            record.Height = Int32.Parse(fields[4]);
            record.BirthDate = DateTime.ParseExact(fields[5], BirthDateFormat, CultureInfo.InvariantCulture);
            record.BirthPlace = fields[6];
            return record;
        }

        public const Char RecordDelimiter = '#';
        public const String CreationTimeFormat = "dd.MM.yyyy HH:mm";
        public const String BirthDateFormat = "dd.MM.yyyy";
        public const Int32 FieldsCount = 7;
    }

    public class DataManager
    {
        public DataManager(String filename)
        {
            _filename = filename;
        }

        public Record[] LoadRecord()
        {
            if (!File.Exists(_filename))
                return Array.Empty<Record>();
            using (StreamReader reader = new StreamReader(_filename))
                return reader.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(line => _serializer.Deserialize(line)).ToArray();
        }

        public void AppendRecord(Record record)
        {
            using (StreamWriter writer = new StreamWriter(_filename, true))
                writer.WriteLine(_serializer.Serialize(record));
        }

        private readonly String _filename;
        private readonly RecordSerializer _serializer = new RecordSerializer();
    }

    class Program
    {
        private static void ShowRecords(Record[] records)
        {
            foreach (Record record in records)
                Console.WriteLine($"{record.Id}\t" +
                                  $"{record.CreationTime.ToString(RecordSerializer.CreationTimeFormat)}\t" +
                                  $"\"{record.FullName}\"\t" +
                                  $"{record.Age}\t" +
                                  $"{record.Height}\t" +
                                  $"{record.BirthDate.ToString(RecordSerializer.BirthDateFormat)}\t" +
                                  $"\"{record.BirthPlace}\"");
        }

        private static void AppendRecord(DataManager dataManager)
        {
            Record record = new Record();
            Console.Write("ID: ");
            record.Id = Int32.Parse(Console.ReadLine() ?? String.Empty);
            record.CreationTime = DateTime.Now;
            Console.Write("Full name: ");
            record.FullName = Console.ReadLine();
            Console.Write("Age: ");
            record.Age = Int32.Parse(Console.ReadLine() ?? String.Empty);
            Console.Write("Height: ");
            record.Height = Int32.Parse(Console.ReadLine() ?? String.Empty);
            Console.Write("Birth date: ");
            record.BirthDate = DateTime.ParseExact(Console.ReadLine() ?? String.Empty, RecordSerializer.BirthDateFormat, CultureInfo.InvariantCulture);
            Console.Write("Birth place: ");
            record.BirthPlace = Console.ReadLine();
            dataManager.AppendRecord(record);
        }

        static void Main(string[] args)
        {
            const String filename = "data.txt";
            DataManager dataManager = new DataManager(filename);
            Console.Write("Enter mode (1 - show all records, 2 - add new record):");
            switch (Console.ReadLine())
            {
                case "1":
                    Record[] records = dataManager.LoadRecord();
                    ShowRecords(records);
                    break;
                case "2":
                    AppendRecord(dataManager);
                    break;
                default:
                    Console.WriteLine("Bad mode");
                    break;
            }
            Console.WriteLine("That's all folks !!!");
            Console.ReadLine();
        }
    }
}
