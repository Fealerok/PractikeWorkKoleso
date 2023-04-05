namespace RandomizationData
{
    public class Generating
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string brand { get; set; }
        public string carNumber { get; set; }
        public string breakdown { get; set; }
        public long phone { get; set; }
        public int price { get; set; }

        List<string> breakDownList = new List<string>() {"Спущено колесо", "Пробито колесо", 
            "Порезы шины", "Нехватка шипов", "Грыжи на шине",
            "Вмятины на дисках", "Избыточное давление", "Износ протектора"};
        public Generating()
        {
            GetFIO(new Random().Next(0, 2));
            GetBrand();
            GetCarNumber();
            GetBreakDown();
            GetPhone();
            GetPrice();
        }
        void GetFIO(int value)
        {
            if (value == 0)
            {
                name = File.ReadAllLines("namesFemale.txt")[new Random().Next(0, File.ReadAllLines("namesFemale.txt").Length)];
                surname = File.ReadAllLines("surnameFemale.txt")[new Random().Next(0, File.ReadAllLines("surnameFemale.txt").Length)];
                patronymic = File.ReadAllLines("patronymicFemale.txt")[new Random().Next(0, File.ReadAllLines("patronymicFemale.txt").Length)];
            }
            else
            {
                name = File.ReadAllLines("namesMale.txt")[new Random().Next(0, File.ReadAllLines("namesMale.txt").Length)];
                surname = File.ReadAllLines("surnameMale.txt")[new Random().Next(0, File.ReadAllLines("surnameMale.txt").Length)];
                patronymic = File.ReadAllLines("patronymicMale.txt")[new Random().Next(0, File.ReadAllLines("patronymicMale.txt").Length)];
            }
        }
        void GetBrand()
        {
            brand = File.ReadAllLines("brands.txt")[new Random().Next(0, File.ReadAllLines("brands.txt").Length)];
        }
        void GetPhone()
        {
            phone = new Random().NextInt64(89000000000, 90000000000);
        }
        void GetBreakDown()
        {
            breakdown = breakDownList[new Random().Next(0, breakDownList.Count)];
        }
        void GetCarNumber()
        {
            string number;
            string engAlph = "АВЕКМНОРСТУХ";
            int region = new Random().Next(01, 100);
            number = $"{engAlph[new Random().Next(0, engAlph.Length)]}{new Random().Next(0, 9)}{new Random().Next(0, 9)}{new Random().Next(0, 9)}{engAlph[new Random().Next(0, engAlph.Length)]}{engAlph[new Random().Next(0, engAlph.Length)]}{region}";
            carNumber = number;
        }
        void GetPrice()
        {
            price = new Random().Next(50000, 200000);
        }
    }
}