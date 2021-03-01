using API.Models.Request.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IEntryRepo
    {
        List<string> GetProjName();
        List<string> GetPlatName();
        List<string> GetStructType();
        List<string> GetStructArea();
        List<string> GetPlatArea();
        List<string> GetSubArea();
        List<string> GetMatType();
        List<string> GetMatVariant();
        List<string> GetProcMethod();
        List<string> GetMatGroup();
        List<dynamic> QueryBuilder(EntriesQueryBuilderRequest entriesQueryBuilderRequest);
        Task<bool> Insert(InsertEntriesRequest insertEntriesRequest);
    }
}
