using API.Models.Request.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IEntryRepo
    {
        List<string> GetProjects();
        List<string> GetPlatforms();
        List<dynamic> QueryBuilder(EntriesQueryBuilderRequest entriesQueryBuilderRequest);
        Task<bool> Insert(InsertEntriesRequest insertEntriesRequest);
    }
}
