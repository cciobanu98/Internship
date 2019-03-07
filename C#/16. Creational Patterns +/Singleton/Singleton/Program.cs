using System;
using System.Collections.Generic;
namespace Singleton
{
    class Configuration
    {
        private static Configuration instance;
        private List<string> config;
        private Configuration()
        {
            config = new List<string>();
        }
        public static Configuration Instance
        {
            get
            {
                if (instance == null)
                    instance = new Configuration();
                return instance;
            }
        }
        public void AddConfig(string config)
        {
            this.config.Add(config);
        }
        public void DisplayConfig()
        {
            foreach (var c in config)
                Console.WriteLine(c);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Configuration conf = Configuration.Instance;
            conf.AddConfig("Ip address");
            conf.AddConfig("Save File");
            Configuration conf2 = Configuration.Instance;
            conf2.AddConfig("MAC Address");
            conf.DisplayConfig();
            Console.ReadKey();

        }
    }
}
