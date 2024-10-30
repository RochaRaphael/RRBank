using Azure.Core;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RRBank.Application.Model;
using RRBank.Application.Model.ModelIn;
using RRBank.Application.Model.ModelOut;
using RRBank.Application.Services.Caching;
using RRBank.Domain.Database;
using RRBank.Infra;
using System.Linq;

namespace RRBank.Application.Services
{
    public class ClientService
    {
        private readonly DataContext context;
        private readonly ICachingService cache;
        private readonly IBus bus;
        public ClientService(DataContext context, ICachingService cache, IBus bus)
        {
            this.context = context;
            this.cache = cache; 
            this.bus = bus;
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
            catch (Exception)
            {
                return new ResultViewModel<List<Client>>("10X22 - Server failure");
            }
           
        }

        public async Task<ResultViewModel<ClientListPaginatedOut>> ClientListPaginatedAsync(ClientListPaginatedIn request)
        {
            try
            {
                var ret = new ClientListPaginatedOut();

                var queryAble = context.Clients
                    .Where(x => x.IsActive == true)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    queryAble = queryAble.Where(w =>
                        w.Name.Contains(request.Search) ||
                        w.LastName.Contains(request.Search));
                }

                if (request.LastClientId.HasValue) 
                {
                    queryAble = queryAble.Where(x => x.Id > request.LastClientId.Value);
                }

                ret.ClientList = await queryAble
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.Id)
                    .Take(request.PageSize)
                    .Select(x => new Client
                    {
                        Id = x.Id,
                        Name = x.Name,
                        LastName = x.LastName,
                        Age = x.Age,
                        Email = x.Email,
                        CelNumber = x.CelNumber
                    })
                    .AsNoTracking()
                    .ToListAsync();

                ret.PageSize = request.PageSize;
                ret.LastClientId = ret.ClientList.LastOrDefault()?.Id; 
                ret.Total = await queryAble.CountAsync(); 
                ret.TotalPages = (int)Math.Ceiling((double)ret.Total / request.PageSize);

                return new ResultViewModel<ClientListPaginatedOut>(ret);
            }
            catch (Exception)
            {
                return new ResultViewModel<ClientListPaginatedOut>("10X22 - Server failure");
            }
        }

        public async Task<ResultViewModel<Client>> AddClientAsync(AddClientIn newClient)
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
            }
        }

        public async Task<ResultViewModel<Client>> UpdateClientAsync(UpdateClientIn newClient, int id)
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
            catch (Exception)
            {
                return new ResultViewModel<Client>("10X57 - Server failure");
            }
        }

        public async Task<ResultViewModel<RequestCancellation>> RequestCancellationAsync(int id)
        {
            try
            {
                var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);

                if(client == null) return new ResultViewModel<RequestCancellation>("80X57 - Client not found");
               
                var request = new RequestCancellation
                {
                    Id = Guid.NewGuid(),
                    ClientId = id,
                    Name = client.Name,
                    Document = client.Document,
                    Status = 1
                };

                var eventRequest = new RequestCancellationEvent(request.Id, request.Name);
                await bus.Publish(eventRequest);

                await context.RequestCancellation.AddAsync(request);
                await context.SaveChangesAsync();

                return new ResultViewModel<RequestCancellation>(request);
            }
            catch (Exception )
            {
                return new ResultViewModel<RequestCancellation>("10X57 - Server failure");
            }
        }
    }
}
