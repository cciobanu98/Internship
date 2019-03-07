using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
     interface IOservable
     {
          void AddObserver(IObserver observer);
          void RemoveObserver(IObserver observer);
          void NotifyObservers(Produce p);
     }
     interface IObserver
     {
          void Update(Produce p);
     }
     class Produce
     {
          public Produce(string name, int id)
          {
               Name = name;
               ID = id;
          }
          public string Name { get; set; }
          public int ID { get; set; }
     }
     class Store : IOservable
     {
          public List<Produce> produces { get; set; }
          private List<IObserver> observers;
          public Store()
          {
               observers = new List<IObserver>();
               produces = new List<Produce>();
          }
          public void AddObserver(IObserver observer)
          {
               observers.Add(observer);    
          }

          public void NotifyObservers(Produce p)
          {
               foreach (var o in observers)
                    o.Update(p);
          }
          public void RemoveObserver(IObserver observer)
          {
               observers.Remove(observer);
          }
          public void Market(Produce p)
          {
               produces.Add(p);
               NotifyObservers(p);
          }
     }
     class Client : IObserver
     {
          private string ProduceName { get; set; }
          public string ClientName { get; set; }
          private Store store;
          public Client(string c, string  p, Store s)
          {
               ClientName = c;
               ProduceName = p;
               store = s;
               s.AddObserver(this);
          }
          public void Update(Produce p)
          {
               if (p.Name == ProduceName)
                    Console.WriteLine($"Send email about {ProduceName} to {ClientName}");
          }
          public void Unsubscribe()
          {
               store.RemoveObserver(this);
          }

     }
     class Program
     {
          static void Main(string[] args)
          {
               Store store = new Store();
               List<Client> clients = new List<Client>();
               clients.Add(new Client("Cristian","Book",  store));
               clients.Add(new Client("Angela", "Book", store));
               clients.Add(new Client("Oleg", "Dog", store));
               store.Market(new Produce("Book", 2));
               store.Market(new Produce("Dog", 3));
               clients[0].Unsubscribe();
               store.Market(new Produce("Book", 4));
               Console.ReadKey();
          }
     }
}
