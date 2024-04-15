using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class ClientManager
    {
        private List<Clients> clients = new List<Clients>();

        public void AddClient(Clients client)
        {
            clients.Add(client);
        }

        public IEnumerable<Clients> GetAllClients()
        {
            return clients;
        }

        public void RemoveAllClients()
        {
            clients.Clear();
        }

        public void RemoveClient(int accountNumber)
        {
            clients.RemoveAll(c => c.NumberAccount == accountNumber);
        }

        public IEnumerable<Clients> SearchByAccountNumber(int accountNumber)
        {
            return clients.Where(c => c.NumberAccount == accountNumber);
        }
    }
}
