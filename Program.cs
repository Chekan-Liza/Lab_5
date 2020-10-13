using System;
namespace Lab_5
{
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class Printer
    {
        public static void iAmPrinting(Organization obj)
        { Console.WriteLine($"Это {obj.GetType()}"); }
    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    interface IConfirmed 
    {
        bool DocConfirmed(); 
        bool NotConfirmed(); 
    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public abstract class Organization 
    {
        public string Name { get; set; } 
        public Organization(string name)
        { Name = name; }
    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class Document : Organization
    {
        public int Number { get; set; }
        public string DocName { get; set; }
        public Document(string name, int number) : base(name)
        { Number = number;
          DocName = Name + "(- имя док-та)";
        }
        public virtual string DocumentName() { return DocName; }
        public void Display()
        { Console.WriteLine($"Документ №{Number} (Oрганизация: \"{Name}\")."); }
        public virtual void TypeDoc() { Console.WriteLine("Это документ"); }
        protected bool IsConfirmed;
    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class Receipt : Document, IConfirmed 
    {
        public string Pay_LName { get; set; }
        public double Sum { get; set; }
        public Receipt(string name, int number, string Pay_LName, double Sum) : base(name, number)
            { this.Pay_LName = Pay_LName;
              this.Sum = Sum;
            }
        public override string DocumentName() { return base.DocumentName(); }
        public bool DocConfirmed()
        {   this.IsConfirmed = true;
            return true; }
        public bool NotConfirmed()
        {   this.IsConfirmed = false;
            return true; }
        public override void TypeDoc()
        {   if (this.IsConfirmed) { Console.WriteLine("Квитанция подтверждена."); }
        }
        public override string ToString()
        {   if (this.IsConfirmed)
            {   return "Оплата подтверждена."; }
                return "Оплата не подтверждена.";
        }
        public void DisplayInfo() { Console.WriteLine($"Квитанция на сумму {Sum} б.р. Фамилия плательщика: {Pay_LName}."); }
    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class Invoice : Document
    {
        public int Count { get; set; }
        public string Product { get; set; }
        public Invoice(string name, int number, int count, string product) : base(name, number)
            { Count = count;
              Product = product; }
        public override string DocumentName() { return base.DocumentName(); }
        public void DisplayInfo() { Console.WriteLine($"Накладная: продукция - {Product}; количество - {Count}."); }
    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class Cheque : Document
    {
        public double Amount { get; set; }
        public string To_Whom { get; set; }
        public Cheque(string name, int number, double amount, string to_whom) : base(name, number)
        {
            Amount = amount;
            To_Whom = to_whom;
        }
        public void DisplayInfo() { Console.WriteLine($"Чек на сумму {Amount} б.р. Получатель (кому выдан): {To_Whom}."); }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {   return false; }
            Organization a = obj as Cheque;
            if (a as Cheque == null)
            {   return false;
            }
                return true;
        }
        public override int GetHashCode() { return base.GetHashCode(); }
    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    class Program
    {
        static void Main(string[] args)
        {
            Receipt Receipt_1 = new Receipt("Грустинка", 1, "Лысенков", 320.50);
            Receipt_1.Display();
            Receipt_1.DisplayInfo();

            Console.WriteLine();

            Invoice Invoice_1 = new Invoice("Веселушечка", 5, 1400, "Хорошее настроение");
            Invoice_1.Display();
            Invoice_1.DisplayInfo();

            Console.WriteLine();

            Cheque Cheque_1 = new Cheque("Котики", 4, 540.00, "Вислоухий");
            Cheque_1.Display();
            Cheque_1.DisplayInfo();

            Console.WriteLine();

            if (Receipt_1 is Receipt) //Оп-р is проверяет совместимость результ. выр-я с зад. типом или на соотв. шаблону
            {   Console.WriteLine("Объект Receipt_1 принадлежит классу Receipt."); }
            else
            {   Console.WriteLine("Объект Receipt_1 не принадлежит классу Receipt."); }

            Console.WriteLine();

            Printer.iAmPrinting(Receipt_1);
            Printer.iAmPrinting(Invoice_1);
            Printer.iAmPrinting(Cheque_1);
        }
    }
}
    
