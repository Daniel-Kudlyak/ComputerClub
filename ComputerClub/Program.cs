using System;
using System.Collections.Generic;

namespace ComputerClub
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ComputerClub computerClub = new ComputerClub(8);
            computerClub.Work();
        }
    }
    class ComputerClub
    {
        private int _money = 0;
        private List <Computer> _computers = new List <Computer>();
        private Queue<Human> _humans = new Queue<Human>();

        public ComputerClub(int computerCount)
        {
            Random rand = new Random(); 
            for (int i = 0; i < computerCount; i++)
            {
                _computers.Add(new Computer(rand.Next(5,15)));
            }
            CreateNewHuman(25);
        }

        private void CreateNewHuman(int count)
        {
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                _humans.Enqueue(new Human(rand.Next(20, 40), rand.Next(1,5)));
            }

        }
        public void Work()
        {
            while (_humans.Count > 0)
            {
                Console.WriteLine($"Money is {_money}");
                Human human = _humans.Dequeue();
                Console.WriteLine($"In queue is man  {human.Hours} hours and he have {human._money} \n ");
                Console.WriteLine("List computers:");
                ListComputer();
                Console.WriteLine("Choose free computer");
                int choose = Convert.ToInt32(Console.ReadLine());
                if (choose >=0 && choose < _computers.Count)
                {
                    if(_computers[choose].BusyComputer)
                    {

                        Console.WriteLine("You choose busy computer");
                    }
                    else
                    {
                        if (human.CheckMoney(_computers[choose]))
                        {
                            Console.WriteLine("Client buy hours");
                        
                            _money += human.Pay();
                            //_computers[_computers.Count].TakeThePlace(human);
                        }
                        else
                        {
                            Console.WriteLine("no money for this computer   ");
                        }
                    }
                } 
                else
                {
                    Console.WriteLine("This PC is not in ComputerClub");
                }
                Console.WriteLine("Next human - press anything button");
                Console.ReadKey();
                Console.Clear();
                SkipHours();
            }
        }  
        public void SkipHours()
        {
            foreach (var item in _computers)
            {
                item.SkipHours();
            }
        }
        private void ListComputer()
        {
            for (int i = 0; i < _computers.Count; i++)
            {
                Console.WriteLine($" Computer number {i}");
                _computers[i].ShowInfo();
            }
        }
    }
    class Computer
    {
        private Human _human;
        private int _timeleft;

        public int PriceForHours { get; private set; }
        public bool BusyComputer
        {
            get
            {
                return _timeleft > 0;
            }
        }
        public Computer(int priceForHours )
        {
            PriceForHours = priceForHours;
        }
        public void TakeThePlace(Human human )
        {
            _human = human;
            _timeleft = _human.Hours;
        }
        public void FreeThePlace()
        {
            _human = null;
        }
        public void SkipHours()
        {
            _timeleft--;
        }
        public void ShowInfo()
        {
            if (BusyComputer)
                Console.WriteLine($"Computer busy at a moment. Hours left {_timeleft}");
            else
                
                Console.WriteLine($"Comtuter not busy. Prise for hour {PriceForHours} ");
        }
    }
    class Human
    {
       private int moneyToPay;
        public int _money { get; private set; }
        public int Hours { get; private set;}


        public Human(int money, int hours)
        {
            _money = money;
            Hours = hours;
        }
        public bool CheckMoney(Computer computer)
        {
            moneyToPay = computer.PriceForHours * Hours;
            if (_money >= moneyToPay)
                return true;
            else
            {
                moneyToPay = 0;
                return false;
            }
        }
        public int Pay()
        {
            _money -= moneyToPay;
            return _money;
        }
    }
}
