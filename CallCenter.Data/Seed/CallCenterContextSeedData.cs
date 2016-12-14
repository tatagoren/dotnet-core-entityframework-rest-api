using CallCenter.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenter.Data.Seed
{
    public class CallCenterContextSeedData
    {
        private CallCenterContext _context;

        public CallCenterContextSeedData(CallCenterContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {



            if (!_context.Customers.Any())
            {
                var customer1 = new Customer()
                {
                    Name = "Tan",
                    Surname = "Atagoren",
                    Phone = "5333762983",
                    Email = "tan@gmail.com",
                    
                };

                var customer2 = new Customer()
                {
                    Name = "Arda",
                    Surname = "Turan",
                    Phone = "5333762983",
                    Email = "arda@gmail.com"
                };

                var customer3 = new Customer()
                {
                    Name = "Veli",
                    Surname = "Kavlak",
                    Phone = "5333762983",
                    Email = "kavlak@gmail.com"
                };

                _context.Customers.Add(customer1);
                _context.Customers.Add(customer2);
                _context.Customers.Add(customer3);
            }

            if (!_context.Campaigns.Any())
            {
                var campaign = new Campaign()
                {
                    CreationDate = DateTime.Now,
                    Description = "Description 1",
                    ExpirationDate = DateTime.Now.AddDays(60),
                    Name = "Campaign 1"
                };

                var campaign2 = new Campaign()
                {
                    CreationDate = DateTime.Now,
                    Description = "Description 2",
                    ExpirationDate = DateTime.Now.AddDays(90),
                    Name = "Campaign 2"
                };

                _context.Campaigns.Add(campaign);
                _context.Campaigns.Add(campaign2);

            }
            await _context.SaveChangesAsync();
            if (!_context.Calls.Any())
            {
                var call1 = new Call()
                {
                    CampaignId = 1,
                    CustomerId = 1,
                    Note = "Call 1 Note",
                    Time = DateTime.Now,
                };

                var call2 = new Call()
                {
                    CampaignId = 1,
                    CustomerId = 2,
                    Note = "Call 2 Note",
                    Time = DateTime.Now,
                };

                var call3 = new Call()
                {
                    CampaignId = 1,
                    CustomerId = 2,
                    Note = "Call 3 Note",
                    Time = DateTime.Now,
                };

                _context.Calls.Add(call1);
                _context.Calls.Add(call2);
                _context.Calls.Add(call3);
            }
            await _context.SaveChangesAsync();



        }
    }
}
