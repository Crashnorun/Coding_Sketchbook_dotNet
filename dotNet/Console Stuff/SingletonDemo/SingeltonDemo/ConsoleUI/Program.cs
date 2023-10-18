namespace ConsoleUI
{
    internal class Program
    {
        static TableServers host1List = TableServers.GetTableServers();
        static TableServers host2List = TableServers.GetTableServers();

        static void Main(string[] args)
        {
            TableServers servers = TableServers.GetTableServers();

            for (int i = 0; i < 5; i++)
            {
                //Console.WriteLine("The next server is: " + servers.GetNextServer());
                Host1GetNextServer();
                Host2GetNextServer();
            }

            Console.ReadLine();
        }



        private static void Host1GetNextServer()
        {
            Console.WriteLine(host1List.GetNextServer());
        }

        private static void Host2GetNextServer()
        {
            Console.WriteLine(host2List.GetNextServer());
        }
    }
}