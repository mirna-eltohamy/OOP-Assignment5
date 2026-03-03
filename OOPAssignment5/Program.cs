#region p1q1
//Interface defines a contract that a class must implement. It defines what a class does but not how it does it.
//Interfaces are used instead of concrete classes to achieve loose coupling through abstraction which allows more flexibility , easy testing & maintainability
//achieve multiple inheritence - loose coupling - abstraction to define capability not implementation - polymorphism
#endregion

#region p1q2
//a
//same method name conflict - class currently provides same implementation for both 
//b
//Fix by explicit implementation - call through interface name
//class Translator : IEnglishSpeaker, IArabicSpeaker
//{
//    void IEnglishSpeaker.Greet()
//    {
//        Console.WriteLine("Hello");
//    }
//    void IArabicSpeaker.Greet()
//    {
//        Console.WriteLine("Ahlan");
//    }
//}
//c
//Greet() can only be called through interface reference pointing to Translator object only
//IEnglishSpeaker tEnglish = new Translator();
//IArabicSpeaker tArabic = new translator();
//tEnglish.Greet();
//tArabic.Greet();
#endregion

#region p1q3
//shallow copy 
//creates new object with same values but for reference types ref is copied - no new object
//deep copy
//creates new object with same values but for reference types new object is created and values are copied - no shared references
//So, if object is immutable shallow copy is fine but otherwise deep copy is used
//Risk with shallow copy with reference types is that changes are refleted in both objects -original and copy - due to shared reference
#endregion

#region p1q4
//Dev - Testing
//QA - Testing
//Title is string type - immutable - so each string is new object
//Dept is Department type - mutable - reference copied -  same object shared - changes reflect in both original and copy Employee
#endregion

#region p2
// Movie Ticket Booking System
using System;
namespace MovieTicketBookingSystem
{
    public interface IPrintable
    {
        public void PrintTicket();
    }
    public interface ITicketTracker: IPrintable
    {
        public string status {  get; set; }

        public void TicketBook()
        {
            if(status == "Available")
                status = "Booked";
           
        }
        public void TicketCancel()
        {
            if (status == "Booked")
                status = "Available";
        }
    }
    public abstract class Ticket : IPrintable, ITicketTracker, ICloneable
    {
        public int TicketId { get; set; }
        public string MovieName { get; set; }
        public decimal Price { get; protected set; }

        public decimal PriceAfterTax => Price * 1.14m;

        public string status { get; set; } = "Available";
        public Ticket(int id, string movieName)
        {
            TicketId = id;
            MovieName = movieName;
        }
        
        public void SetPrice(decimal price)
        {
            Console.WriteLine($"Setting price directly: {price}");
            Price = price;
        }

        public void SetPrice(decimal basePrice, decimal multiplier)
        {
            Price = basePrice * multiplier;
            Console.WriteLine($"Setting price with multiplier: {basePrice} x {multiplier} = {Price}");
        }

        public abstract void PrintTicket();
        public abstract object Clone();
    }

    public class StandardTicket : Ticket
    {
        public string SeatNumber { get; set; }

        public StandardTicket(int id, string movieName, string seat)
            : base(id, movieName)
        {
            SeatNumber = seat;
        }
        public override void PrintTicket()
        {
            Console.WriteLine($"Ticket #{TicketId} | {MovieName} | Price: {Price} EGP | Seat: {SeatNumber} | After Tax: {PriceAfterTax:F2} EGP | Status: {status}");

        }
        public override object Clone()
        {
            return (StandardTicket)this.MemberwiseClone();
            
        }
    }
    public class VIPTicket : Ticket
    {
        public bool LoungeAccess { get; set; }
        public decimal ServiceFee { get; set; }

        public VIPTicket(int id, string movieName, bool lounge, decimal fee)
            : base(id, movieName)
        {
            LoungeAccess = lounge;
            ServiceFee = fee;
        }
        
        public override void PrintTicket()
        {
            Console.WriteLine($"Ticket #{TicketId} | {MovieName} | Price: {Price} EGP | Lounge: {(LoungeAccess ? "Yes" : "No")} | Service Fee: {ServiceFee} EGP | After Tax: {PriceAfterTax:F2} EGP | Status: {status}");

        }
        public override object Clone()
        {
            return (VIPTicket)this.MemberwiseClone();

        }

    }

    public class IMAXTicket : Ticket
    {
        public bool Is3D { get; set; }

        public IMAXTicket(int id, string movieName, bool is3D)
            : base(id, movieName)
        {
            Is3D = is3D;
        }
        
        public override void PrintTicket()
        {
            Console.WriteLine($"Ticket #{TicketId} | {MovieName} | Price: {Price} EGP |  IMAX 3D: {(Is3D ? "Yes" : "No")} | After Tax: {PriceAfterTax:F2} EGP | Status: {status}");

        }
        public override object Clone()
        {
            return (IMAXTicket)this.MemberwiseClone();

        }
    }

    public class Cinema
    {
        public Ticket[] tickets = new Ticket[10];
        private int count = 0;

        public void OpenCinema()
        {
            Console.WriteLine("========== Cinema Opened ==========");
            Console.WriteLine("Projector started.\n");
        }

        public void CloseCinema()
        {
            Console.WriteLine("\nProjector stopped.");
            Console.WriteLine("========== Cinema Closed ==========");
        }

        public void AddTicket(Ticket ticket)
        {
            if (count < tickets.Length)
            {
                tickets[count++] = ticket;
            }
        }
        
        public void PrintAllTickets()
        {
            Console.WriteLine("\n========== All Tickets ==========");
            for (int i = 0; i < count; i++)
            {
                tickets[i].PrintTicket();
            }
        }

        public static void ProcessTicket(ITicketTracker t)
        {
            Console.WriteLine("\n========== Process Single Ticket ==========");
            t.PrintTicket();
        }
    }

    public static class BookingHelper
    {
        private static int bookingCounter = 0;

        public static double CalcGroupDiscount(int numberOfTickets, double pricePerTicket)
        {
            double total = numberOfTickets * pricePerTicket;

            if (numberOfTickets >= 5)
                return total * 0.9; // 10% discount

            return total;
        }

        public static string GenerateBookingReference()
        {
            bookingCounter++;
            return $"BK-{bookingCounter}";
        }

        //utility method to print any array of IPrintable objects
        public static void PrintAll(IPrintable[] printables)
        {
            Console.WriteLine("\n========== BookingHelper.PrintAll ==========");
            foreach (var printable in printables)
            {
                printable.PrintTicket();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //a
            Cinema cinema = new Cinema();
            cinema.OpenCinema();
            //b
            StandardTicket t1 = new StandardTicket(1, "Inception", "A-5");
            VIPTicket t2 = new VIPTicket(2, "Avengers", true, 50);
            IMAXTicket t3 = new IMAXTicket(3, "Dune", false);

            ITicketTracker t = t1;
            t.TicketBook();
            t = t2;
            t.TicketBook();
            t = t3;
            t.TicketBook();

            cinema.AddTicket(t1);
            cinema.AddTicket(t2);
            cinema.AddTicket(t3);
            //c
            cinema.PrintAllTickets();
            //d
            Console.WriteLine("Clone Test");

            VIPTicket t4 = (VIPTicket)t2.Clone();
            t4.MovieName = "Tarzan";

            Console.WriteLine("\nOriginal VIP Ticket:");
            t3.PrintTicket();
            Console.WriteLine("Copy VIP Ticket:");
            t4.PrintTicket();
            //e
            Console.WriteLine("\nTicket #1 after Cancellation: ");
            t = t1;
            t.TicketCancel();
            t.PrintTicket();
            //f
            IPrintable[] printables = { t1, t2, t3, t4 };
            BookingHelper.PrintAll(printables);
            //g
            cinema.CloseCinema();
        }
    }
}
#endregion