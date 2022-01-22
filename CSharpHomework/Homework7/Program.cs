using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Homework7
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
        public String SerializeRecord(Record record)
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

        public String SerializeRecords(IList<Record> records)
        {
            return String.Join("\r\n", records.Select(SerializeRecord));
        }

        public Record DeserializeRecord(String source)
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

        public IList<Record> DeserializeRecords(String source)
        {
            return source.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(DeserializeRecord).ToList();
        }

        public const Char RecordDelimiter = '#';
        public const String CreationTimeFormat = "dd.MM.yyyy HH:mm";
        public const String BirthDateFormat = "dd.MM.yyyy";
        public const Int32 FieldsCount = 7;
    }

    public class RecordFilter
    {
        public RecordFilter(DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }

        public DateTime FromDate { get; }
        public DateTime ToDate { get; }
    }

    public enum RecordOrdering
    {
        None = 0,
        Asc = 1,
        Desc = 2
    }

    public class DataManager
    {
        public DataManager(String filename)
        {
            _filename = filename;
        }

        public IList<Record> LoadRecords(RecordFilter filter = null, RecordOrdering ordering = RecordOrdering.None)
        {
            if (filter == null && ordering == RecordOrdering.None)
                return LoadAllRecords();
            IEnumerable<Record> records = LoadRecords();
            if (filter != null)
                records = records.Where(record => filter.FromDate <= record.CreationTime && record.CreationTime <= filter.ToDate);
            if (ordering == RecordOrdering.Asc)
                records = records.OrderBy(record => record.CreationTime);
            if (ordering == RecordOrdering.Desc)
                records = records.OrderByDescending(record => record.CreationTime);
            return records.ToList();
        }

        public Record GetRecord(Int32 id)
        {
            return LoadAllRecords().FirstOrDefault(record => record.Id == id);
        }

        public void AddRecord(Record record)
        {
            IList<Record> records = LoadRecords();
            if (records.FirstOrDefault(r => r.Id == record.Id) != null)
                throw new InvalidOperationException("ID must be unique !!!");
            records.Add(record);
            SaveAllRecords(records);
        }

        public void UpdateRecord(Record record)
        {
            IList<Record> records = LoadRecords();
            for (Int32 index = 0; index < records.Count; ++index)
            {
                if (records[index].Id == record.Id)
                    records[index] = record;
            }
            SaveAllRecords(records);
        }

        public void RemoveRecord(Int32 id)
        {
            IList<Record> records = LoadRecords();
            Record record = records.FirstOrDefault(r => r.Id == id);
            records.Remove(record);
            SaveAllRecords(records);
        }

        private IList<Record> LoadAllRecords()
        {
            if (!File.Exists(_filename))
                return new List<Record>();
            using (StreamReader reader = new StreamReader(_filename))
                return _serializer.DeserializeRecords(reader.ReadToEnd());
        }

        private void SaveAllRecords(IList<Record> records)
        {
            using (StreamWriter writer = new StreamWriter(_filename, false))
                writer.WriteLine(_serializer.SerializeRecords(records));
        }

        private readonly String _filename;
        private readonly RecordSerializer _serializer = new RecordSerializer();
    }

    class Program
    {
        private static Record GetRecord(DataManager dataManager)
        {
            Console.Write("Enter record ID: ");
            Int32 id = Int32.Parse(Console.ReadLine() ?? String.Empty);
            Record record = dataManager.GetRecord(id);
            if (record == null)
                Console.WriteLine("Unknown record ID !!!");
            return record;
        }

        private static void ShowRecord(Record record)
        {
            Console.WriteLine($"{record.Id}\t" +
                              $"{record.CreationTime.ToString(RecordSerializer.CreationTimeFormat)}\t" +
                              $"\"{record.FullName}\"\t" +
                              $"{record.Age}\t" +
                              $"{record.Height}\t" +
                              $"{record.BirthDate.ToString(RecordSerializer.BirthDateFormat)}\t" +
                              $"\"{record.BirthPlace}\"");
        }

        private static void FillRecordData(Record record)
        {
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
        }

        private static RecordFilter GetFilter()
        {
            Console.Write("Do you need filtering (y/n) ?");
            String needFiltering = Console.ReadLine();
            RecordFilter recordFilter = null;
            const String dateFormat = "dd.MM.yyyy";
            if (needFiltering == "y" || needFiltering == "Y")
            {
                Console.Write("From date: ");
                DateTime fromDate = DateTime.ParseExact(Console.ReadLine() ?? String.Empty, dateFormat, CultureInfo.InvariantCulture);
                Console.Write("To date: ");
                DateTime toDate = DateTime.ParseExact(Console.ReadLine() ?? String.Empty, dateFormat, CultureInfo.InvariantCulture);
                recordFilter = new RecordFilter(fromDate, toDate);
            }
            return recordFilter;
        }

        private static RecordOrdering GetOrdering()
        {
            Console.Write("Do you need ordering (y/n) ?");
            String needOrdering = Console.ReadLine();
            RecordOrdering recordOrdering = RecordOrdering.None;
            if (needOrdering == "y" || needOrdering == "Y")
            {
                Console.Write("Enter ordering direction (a - ASC, d - DESC):");
                String direction = Console.ReadLine();
                recordOrdering = direction == "d" || direction == "D" ? RecordOrdering.Desc : RecordOrdering.Asc;
            }
            return recordOrdering;
        }

        private static void ShowRecords(DataManager dataManager)
        {
            RecordFilter recordFilter = GetFilter();
            RecordOrdering recordOrdering = GetOrdering();
            IList<Record> records = dataManager.LoadRecords(recordFilter, recordOrdering);
            Console.WriteLine("Records:");
            foreach (Record record in records)
                ShowRecord(record);
        }

        private static void ShowRecord(DataManager dataManager)
        {
            Record record = GetRecord(dataManager);
            if (record != null)
            {
                Console.WriteLine("Record data:");
                ShowRecord(record);
            }
        }

        private static void AddNewRecord(DataManager dataManager)
        {
            Console.WriteLine("New record data:");
            Record record = new Record();
            Console.Write("ID: ");
            record.Id = Int32.Parse(Console.ReadLine() ?? String.Empty);
            FillRecordData(record);
            dataManager.AddRecord(record);
        }

        private static void UpdateRecord(DataManager dataManager)
        {
            Record record = GetRecord(dataManager);
            if (record != null)
            {
                Console.WriteLine("Old record data:");
                ShowRecord(record);
                Console.WriteLine("New record data:");
                FillRecordData(record);
                dataManager.UpdateRecord(record);
            }
        }

        private static void RemoveRecord(DataManager dataManager)
        {
            Record record = GetRecord(dataManager);
            if (record != null)
                dataManager.RemoveRecord(record.Id);
        }

        static void Main(string[] args)
        {
            const String filename = "data.txt";
            DataManager dataManager = new DataManager(filename);
            Console.Write("Enter mode (1 - show all records, 2 - show record, 3 - add new record, 4 - update record, 5 - remove record):");
            switch (Console.ReadLine())
            {
                case "1":
                    ShowRecords(dataManager);
                    break;
                case "2":
                    ShowRecord(dataManager);
                    break;
                case "3":
                    AddNewRecord(dataManager);
                    break;
                case "4":
                    UpdateRecord(dataManager);
                    break;
                case "5":
                    RemoveRecord(dataManager);
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
