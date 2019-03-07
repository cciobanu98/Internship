using System;
using System.Collections.Generic;
namespace Factory
{
    interface ISender
    {
        void SendMessage();
    }
    class EmailSender:ISender
    {

        public void SendMessage()
        {
            Console.WriteLine("Send Message from Email");
        }
    }
    class SMSSender : ISender
    {
        public void SendMessage()
        {
            Console.WriteLine("Send Message from SMS");
        }
    }
    class MMSSender:ISender
    {
        public void SendMessage()
        {
            Console.WriteLine("Send Message from MMS");
        }
    }
    class MessegerSender:ISender
    {
        public void SendMessage()
        {
            Console.WriteLine("Send Message from Messenger");
        }
    }
    class SkypeSender:ISender
    {
        public void SendMessage()
        {
            Console.WriteLine("Send Message from Skype");
        }
    }
    static class SenderFactory
    {
        public static ISender GetEmailSender()
        {
            return new EmailSender();
        }
        public static ISender GetSMSSender()
        {
            return new SMSSender();
        }
        public static ISender GetMMSSender()
        {
            return new MMSSender();
        }
        public static ISender GetMessengerSender()
        {
            return new MessegerSender();
        }
        public static ISender GetSkypeSender()
        {
            return new SkypeSender();
        }
        public static ISender GetSender(string sender)
        {
            if (sender == "SMS")
                return GetSMSSender();
            else if (sender == "MMS")
                return GetMMSSender();
            else if (sender == "Skype")
                return GetSkypeSender();
            else if (sender == "Messenger")
                return GetMessengerSender();
            else if (sender == "Email")
                return GetEmailSender();
            return GetSMSSender();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<ISender> send = new List<ISender>();
            send.Add(SenderFactory.GetEmailSender());
            send.Add(SenderFactory.GetMMSSender());
            send.Add(SenderFactory.GetSender("MMS"));
            send.Add(SenderFactory.GetSender("Skype"));
            send.Add(SenderFactory.GetMessengerSender());
            foreach (var s in send)
                s.SendMessage();
            Console.ReadKey();
        }
    }
}
