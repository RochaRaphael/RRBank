using Microsoft.EntityFrameworkCore;
using RRBank.Application.Model;
using RRBank.Application.Model.ModelIn;
using RRBank.Application.Services.Caching;
using RRBank.Domain.Database;
using RRBank.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRBank.Application.Services
{
    public class ManagerService
    {
        private readonly DataContext context;
        private readonly ICachingService cache;
        public ManagerService(DataContext context, ICachingService cache)
        {
            this.context = context;
            this.cache = cache;
        }

        public async Task<ResultViewModel<Manager>> GetManagerByIdAsync(int id)
        {
            try
            {
                var managerCache = await cache.GetAsync<Manager>(id.ToString());
                if (managerCache != null) return new ResultViewModel<Manager>(managerCache);

                var ret = await context.Managers.FirstOrDefaultAsync(x => x.IsActive == true);
                if (ret == null) return new ResultViewModel<Manager>(false, "Manager not found.");

                await cache.SetAsync<Manager>(id.ToString(), ret);
                return new ResultViewModel<Manager>(ret);
            }
            catch (Exception ex)
            {
                return new ResultViewModel<Manager>("18X29 - Server failure");
                throw;
            }

        }
        public async Task<ResultViewModel<List<Manager>>> ManagerListAsync()
        {
            try
            {
                var clientCache = await cache.GetListAsync<Manager>("managers");
                if (clientCache.Count() != 0)
                    return new ResultViewModel<List<Manager>>(clientCache);

                var ret = await context.Managers.Where(x => x.IsActive == true).ToListAsync();
                await cache.SetListAsync<Manager>("managers", ret);

                return new ResultViewModel<List<Manager>>(ret);
            }
            catch (Exception ex)
            {
                return new ResultViewModel<List<Manager>>("44X23 - Server failure");
                throw;
            }

        }

        public async Task<ResultViewModel<Manager>> AddManagerAsync(AddManagerIn newManager)
        {
            try
            {
                var manager = new Manager
                {
                    Name = newManager.Name,
                    LastName = newManager.LastName,
                    Document = newManager.Document,
                    Age = newManager.Age,
                    Email = newManager.Email,
                    Register = DateTime.Now,
                    IsActive = true
                };
                await context.Managers.AddAsync(manager);
                await context.SaveChangesAsync();

                return new ResultViewModel<Manager>(manager);
            }
            catch (Exception ex)
            {
                return new ResultViewModel<Manager>("00X53 - Server failure");
                throw;
            }
        }

        public async Task<ResultViewModel<Manager>> UpdateManagerAsync(UpdateManagerIn newManager, int id)
        {
            try
            {
                var manager = await context.Managers.FirstOrDefaultAsync(x => x.Id == id);
                manager.Name = newManager.Name ?? manager.Name;
                manager.LastName = newManager.LastName ?? manager.LastName;
                manager.Email = newManager.Email ?? manager.Email;

                context.Managers.Update(manager);
                await context.SaveChangesAsync();

                return new ResultViewModel<Manager>(manager);
            }
            catch (Exception ex)
            {
                return new ResultViewModel<Manager>("43X53 - Server failure");
                throw;
            }
        }

        public async Task<ResultViewModel<Manager>> DeleteManagerAsync(int id)
        {
            try
            {
                var manager = await context.Managers.FirstOrDefaultAsync(x => x.Id == id);
                manager.IsActive = false;

                context.Managers.Update(manager);
                await context.SaveChangesAsync();

                return new ResultViewModel<Manager>(manager);
            }
            catch (Exception ex)
            {
                return new ResultViewModel<Manager>("10X57 - Server failure");
                throw;
            }
        }

        public async Task<ResultViewModel<Account>> CreateAccountAsync(int ClientId)
        {
            try
            {
                var client = await context.Clients.Where(x => x.Id == ClientId).AnyAsync();
                if(client == false)
                    return new ResultViewModel<Account>("Client not found.");

                var account = new Account
                {
                    ClientId = ClientId,
                    Number = new Random().Next(100000000, 1000000000).ToString(),
                    Balance = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    IsActive = true,
                };
                context.Accounts.Update(account);
                await context.SaveChangesAsync();

                return new ResultViewModel<Account>(account);
            }
            catch (Exception ex)
            {
                return new ResultViewModel<Account>("34X51 - Server failure");
                throw;
            }
        }
    }
}
