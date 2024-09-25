using Microsoft.EntityFrameworkCore;
using RRBank.Application.Services.Caching;
using RRBank.Application.ViewModel;
using RRBank.Domain.Database;
using RRBank.Infra;

namespace RRBank.Application.Services
{
    public class ClientService
    {
        private readonly DataContext context;
        private readonly ICachingService cache;
        public ClientService(DataContext context, ICachingService cache)
        {
            this.context = context;
            this.cache = cache; 
        }

        public async Task<ResultViewModel<Client>> GetClientByIdAsync(int id)
        {
            try
            {
                var clientCache = await cache.GetAsync<Client>(id.ToString());
                if (clientCache != null) return new ResultViewModel<Client>(clientCache);

                var ret = await context.Clients.FirstOrDefaultAsync(x => x.IsActive == true);
                if(ret == null) return new ResultViewModel<Client>(false, "Cliet not found.");

                await cache.SetAsync<Client>(id.ToString(), ret);
                return new ResultViewModel<Client>(ret);
            }
            catch (Exception ex)
            {
                return new ResultViewModel<Client>("10X22 - Server failure");
                throw;
            }

        }
        public async Task<ResultViewModel<List<Client>>> ClientListAsync()
        {
            try
            {
                var clientCache = await cache.GetListAsync<Client>("clients");
                if (clientCache.Count() != 0)
                    return new ResultViewModel<List<Client>>(clientCache);

                var ret = await context.Clients.Where(x => x.IsActive == true).ToListAsync();
                await cache.SetListAsync<Client>("clients", ret);

                return new ResultViewModel<List<Client>>(ret);
            }
            catch (Exception ex)
            {
                return new ResultViewModel<List<Client>>("10X22 - Server failure");
                throw;
            }
           
        }

        public async Task<ResultViewModel<Client>> AddClientAsync(NewClientViewModel newClient)
        {
            try
            {
                var manager = await context.Managers
                    .Where(x => x.Id == newClient.ManagerId)
                    .AnyAsync();

                if(manager == false)
                    return new ResultViewModel<Client>("Manager not found.");

                var client = new Client
                {
                    Name = newClient.Name,
                    LastName = newClient.LastName,
                    Document = newClient.Document,
                    Age = newClient.Age,
                    Email = newClient.Email,
                    Register = DateTime.Now,
                    CelNumber = newClient.CelNumber,
                    ManagerId = newClient.ManagerId,
                    IsActive = true
                };
                await context.Clients.AddAsync(client);
                await context.SaveChangesAsync();

                return new ResultViewModel<Client>(client);
            }
            catch (Exception ex)
            {
                return new ResultViewModel<Client>("00X53 - Server failure");
                throw;
            }
        }

        public async Task<ResultViewModel<Client>> UpdateClientAsync(UpdateClientViewModel newClient, int id)
        {
            try
            {
                var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
                client.Name = newClient.Name ?? client.Name;
                client.LastName = newClient.LastName ?? client.LastName;
                client.CelNumber = newClient.CelNumber ?? client.CelNumber;
                client.Email = newClient.Email ?? client.Email;

                context.Clients.Update(client);
                await context.SaveChangesAsync();

                return new ResultViewModel<Client>(client);
            }
            catch (Exception ex)
            {
                return new ResultViewModel<Client>("43X53 - Server failure");
                throw;
            }
        }

        public async Task<ResultViewModel<Client>> DeleteClientAsync(int id)
        {
            try
            {
                var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
                client.IsActive = false;

                context.Clients.Update(client);
                await context.SaveChangesAsync();

                return new ResultViewModel<Client>(client);
            }
            catch (Exception ex)
            {
                return new ResultViewModel<Client>("10X57 - Server failure");
                throw;
            }
        }
    }
}
