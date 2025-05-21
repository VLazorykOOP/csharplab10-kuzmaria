using System;

namespace FacultyLifeSimulation
{
    public delegate void FacultyDayHandler(object sender, FacultyDayEventArgs e);

    public class FacultyDayEventArgs : EventArgs
    {
        public string Message { get; set; } = "";
        public int Day { get; set; }
    }

    public class Faculty
    {
        public string Name { get; }
        public event FacultyDayHandler? FacultyDayEvent;

        private Random random = new Random();

        public Faculty(string name)
        {
            Name = name;
        }

        public void Simulate(int days)
        {
            Console.WriteLine($"🔬 Симуляція життя факультету \"{Name}\" протягом {days} днів.\n");

            for (int day = 1; day <= days; day++)
            {
                Console.WriteLine($"📅 День {day}");

                // 1 з 3 шансів на День факультету
                if (random.Next(0, 3) == 0)
                {
                    Console.WriteLine("🎉 Відбувається День факультету!");
                    FacultyDayEventArgs e = new FacultyDayEventArgs { Day = day };

                    FacultyDayEvent?.Invoke(this, e);
                    Console.WriteLine(e.Message);
                }
                else
                {
                    Console.WriteLine("😴 Звичайний день. Нічого не сталося.");
                }

                Console.WriteLine(new string('-', 50));
                System.Threading.Thread.Sleep(500); // для ефекту симуляції
            }
        }
    }

    public class Dean
    {
        private string[] speeches = {
            "Декан привітав студентів з перемогами.",
            "Декан подякував викладачам за роботу.",
            "Декан вручив грамоти найкращим студентам."
        };

        private Random rnd = new Random();

        public void OnFacultyDay(object sender, FacultyDayEventArgs e)
        {
            e.Message += $"🧑‍🏫 Декан: {speeches[rnd.Next(speeches.Length)]}\n";
        }
    }

    public class Students
    {
        private string[] actions = {
            "Студенти влаштували флешмоб.",
            "Студенти провели турнір з Dota 2.",
            "Студенти підготували виставу."
        };

        private Random rnd = new Random();

        public void OnFacultyDay(object sender, FacultyDayEventArgs e)
        {
            e.Message += $"🧑‍🎓 Студенти: {actions[rnd.Next(actions.Length)]}\n";
        }
    }

    public class Teachers
    {
        private string[] activities = {
            "Викладачі організували відкриту лекцію.",
            "Викладачі провели конференцію.",
            "Викладачі провели брейн-ринг."
        };

        private Random rnd = new Random();

        public void OnFacultyDay(object sender, FacultyDayEventArgs e)
        {
            e.Message += $"👩‍🏫 Викладачі: {activities[rnd.Next(activities.Length)]}\n";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Введіть кількість днів для симуляції:");
            int days = int.Parse(Console.ReadLine() ?? "5");

            Faculty faculty = new Faculty("ФІТ");
            Dean dean = new Dean();
            Students students = new Students();
            Teachers teachers = new Teachers();

            faculty.FacultyDayEvent += dean.OnFacultyDay;
            faculty.FacultyDayEvent += students.OnFacultyDay;
            faculty.FacultyDayEvent += teachers.OnFacultyDay;

            faculty.Simulate(days);

            Console.WriteLine("\n🔚 Симуляція завершена. Натисніть Enter для виходу.");
            Console.ReadLine();
        }
    }
}
