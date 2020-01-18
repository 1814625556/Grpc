using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Customer.CustomerClient(channel);

            using (var call = client.GetNewCustomers(new NewCustomerRequest()))
            {
                while(await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.FirstNmae}{currentCustomer.LastNmae}:" +
                        $"{currentCustomer.EmailAddress}");
                }
            }


            Console.ReadLine();
        }

        static async void Request1()
        {
            var input = new HelloRequest { Name = "Tim" };

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(input);

            Console.WriteLine(reply.Message);
        }

        static async void Request2()
        {
            var input = new CustomerLookupModel { UserId = 1 };

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Customer.CustomerClient(channel);

            var reply = await client.GetCustomerInfoAsync(input);

            Console.WriteLine($"{reply.FirstNmae}{reply.LastNmae}");
        }
    }
}
