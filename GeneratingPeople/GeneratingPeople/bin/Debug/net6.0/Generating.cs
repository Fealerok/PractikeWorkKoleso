using Microsoft.Data.Sqlite;
using RandomizationData;


namespace GeneratingPeople
{

    abstract class Creator
    {
        abstract public People Create();
    }
    class PeopleCreator : Creator
    {
        public override People Create()
        {
            return new ConcretePeople();
        }
    }
    public abstract class People { }
    public class ConcretePeople : People
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string brand { get; set; }
        public string carNumber { get; set; }
        public string breakdown { get; set; }
        public long phone { get; set; }
        public int price { get; set; }
        public ConcretePeople()
        {
            Generating generate = new Generating();
            name = generate.name;
            surname = generate.surname;
            patronymic = generate.patronymic;
            brand = generate.brand;
            carNumber = generate.carNumber;
            breakdown = generate.breakdown;
            phone = generate.phone;
            price = generate.price;
            DB();
        }
        void DB()
        {
            using (var connection = new SqliteConnection("Data Source='Requests.db'"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "VACUUM";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO Requests (Name, Surname, Patronymic, Brand, CarNumber, Breakdown, Phone, Price) VALUES ('{name}', '{surname}', '{patronymic}', '{brand}', '{carNumber}', '{breakdown}', {phone}, {price})";
                command.ExecuteNonQuery();
            }
        }
    }
}