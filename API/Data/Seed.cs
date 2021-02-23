using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<MtoUser>>(userData);
            
            if (users != null)
                foreach (var user in users)
                {
                    using var hmac = new HMACSHA512();

                    user.UserName = user.UserName.ToLower();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                    user.PasswordSalt = hmac.Key;

                    context.Users.Add(user);
                }

            await context.SaveChangesAsync();
        }

        public static async Task SeedEntries(DataContext context)
        {
            if (await context.Entries.AnyAsync()) return;

            var entryData = await File.ReadAllTextAsync("Data/EntrySeedData.json");
            var entries = JsonSerializer.Deserialize<List<MtoEntry>>(entryData);
            if (entries != null)
                foreach (var entry in entries)
                    context.Entries.Add(entry);

            await context.SaveChangesAsync();
        }
    }
}