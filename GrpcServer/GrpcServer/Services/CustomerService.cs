using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        public ILogger<CustomerService> Logger { get; }
        public CustomerService(ILogger<CustomerService> logger)
        {
            Logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            var output = new CustomerModel();
            if (request.UserId == 1)
            {
                output.FirstNmae = "zhang";
                output.LastNmae = "san";
            }
            else if (request.UserId == 2)
            {
                output.FirstNmae = "li";
                output.LastNmae = "si";
            }
            else
            {
                output.FirstNmae = "wang";
                output.LastNmae = "wu";
            }
            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, 
            IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            var customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstNmae = "Tim",
                    LastNmae = "Corey",
                    EmailAddress = "tim@iamtimcorey.com",
                    Age= 27,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstNmae = "Sue",
                    LastNmae = "Store",
                    EmailAddress = "sue@stormy.com",
                    Age= 27,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstNmae = "Bilbo",
                    LastNmae = "Baggins",
                    EmailAddress = "bilbo@baggins.com",
                    Age= 27,
                    IsAlive = true
                }
            };

            foreach (var cust in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
