using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;

namespace projectBOSE.Model
{
    public class DbService : IDbService
    {
        /// <summary>
        /// Get all names of services
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> GetAllNamesSevices()
        {
            using (communalEntities cE = new communalEntities())
            {   
                ObservableCollection<services> tempObs = new ObservableCollection<services>(cE.services);
                 return new ObservableCollection<string>( tempObs.Select(x=>x.name).ToList());
            }
        }

        /// <summary>
        /// To select all history rows by account id
        /// </summary>
        /// <param name="accid">Account id (decimal)</param>
        /// <returns>null if accid is not found in database</returns>
        public ObservableCollection<historyres> GetHistoryByClient(decimal accid)
        {
            using (communalEntities cE = new communalEntities())
            {
                clients client = cE.clients.FirstOrDefault(x => x.accountid == accid);

                if (client != null)
                {
                    // so client founded
                    var entryPoint = (from ep in cE.history
                                      join e in cE.services on ep.service_id equals e.id
                                      join t in cE.clients on ep.client_id equals t.id
                                      where t.accountid == accid
                                      select new historyres
                                      {
                                          name = e.name,
                                          tariff = e.tariff,
                                          accountid = accid,
                                          firstname = t.firstname,
                                          lastname = t.lastname,
                                          age = t.age,
                                          by_date = ep.by_date,
                                          receiver = ep.receiver,
                                          paid = ep.paid
                                      }).ToList();

                    return new ObservableCollection<historyres>(entryPoint);
                }
            }

            return null;
        }

        /// <summary>
        /// Add payment to database
        /// Can throw exceptions.
        /// </summary>
        /// <param name="accid">Client account id</param>
        /// <param name="servicename">Service name</param>
        /// <param name="targetreciever">Receiver id</param>
        /// <param name="payvalue">value of payment</param>
        /// <returns></returns>
        public void AddPaymentToDb(decimal accid, string servicename, long targetreciever, double payvalue)
        {
            // First of all we need to find client in database if `accid` is valid
            if (accid <= 0)
            {
                throw new ArgumentException("Invalid account id of client");
            }

            if (targetreciever <= 0)
            {
                throw new ArgumentException("Receiver is invalid");
            }

            if (servicename == null)
            {
                throw new ArgumentNullException("Service name cannot be null");
            }

            if (payvalue < 0)
            {
                throw new ArgumentException("Payable value cannot be negative");
            }

            using (communalEntities cE = new communalEntities())
            {
                clients client = cE.clients.FirstOrDefault(x => x.accountid == accid);

                if (client == null)
                    throw new Exception("Client with id " + accid + " not found in system. Abort!");
                
                // now get service by service name
                services service = cE.services.FirstOrDefault(x => x.name == servicename);
                if (service == null)
                    throw new Exception("Service with name " + servicename + " not found in system. Abort!");

                // Add new row to database
                cE.history.Add(new history { client_id = client.id, service_id = service.id, paid = payvalue, receiver = targetreciever, by_date = DateTime.Today.ToShortDateString() });
                try
                {
                    cE.SaveChanges();
                    if (dbHistoryChanged != null)
                        dbHistoryChanged(this, new EventArgs());
                }
                catch (DbUpdateException)
                {
                    throw new Exception("Failed to add this payment to database. It is already exists.");
                }
            }
        }

        /// <summary>
        /// Remove client by account id from all tables
        /// </summary>
        /// <param name="accid">Account id</param>
        public void RemoveClientByAccount(decimal accid)
        {
            // First of all we need to find client in database if `accid` is valid
            if (accid <= 0)
            {
                throw new ArgumentException("Invalid account id of client");
            }

            using (communalEntities cE = new communalEntities())
            {
                clients client = cE.clients.FirstOrDefault(x => x.accountid == accid);

                if (client == null)
                    throw new Exception("Client with id " + accid + " not found in system. Abort!");

                var onremove = cE.history.Select(y => y).Where(y => y.client_id == client.id).ToList();
                cE.history.RemoveRange(onremove);

                cE.clients.Remove(client);
            }
        }

        /// <summary>
        /// Add new by account id from all tables
        /// </summary>
        /// <param name="accid">Account id</param>
        /// <param name="fname">First name</param>
        /// <param name="lname">Last name</param>
        /// <param name="age_">Age</param>
        public void AddClientByAccount(decimal accid, string fname, string lname, int age_)
        {
            // First of all we need to find client in database if `accid` is valid
            if (accid <= 0)
            {
                throw new ArgumentException("Invalid account id of client");
            }

            if (fname == null || lname == null)
            {
                throw new ArgumentNullException("Invalid user first name or last name");
            }

            if (age_ <= 0)
                throw new ArgumentException("Age cannot be less or equal then 0");

            using (communalEntities cE = new communalEntities())
            {
                clients client = cE.clients.FirstOrDefault(x => x.accountid == accid);

                if (client != null)
                    throw new Exception("Client with id " + accid + " already exits in database! Ignoring...");

                cE.clients.Add(new projectBOSE.clients { accountid = accid, firstname = fname, lastname = lname, age = age_ });
                try
                {
                    cE.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception("Failed to add this client to database");
                }
            }
        }
        
        public event EventHandler dbHistoryChanged;
    }
}
