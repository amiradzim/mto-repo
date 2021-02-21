// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using API.Data;
// using API.Entities;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace API.Test
// {
//     public class FetchData
//     {
//         private readonly DataContext _context;

//         public FetchData(DataContext context)
//         {
//             _context = context;
//         }

//         public async Task<ActionResult<List<MtoEntry>>> SelectAll()
//         {
//             return await _context.Entries.ToListAsync();
//         }

//         public async Task<ActionResult<List<MtoEntry>>> FilterSelect(List<MtoEntry> mtoEntry)
//         {
//             var results = await _context.Entries.AsQueryable()
//             .Where(_context.Entries.SubCostElement = "Jacket");
//         }
//     }

// }
//         //     // var filteredEntries = from entry in results
//         //     //                     .Where(entry => entry.SubCostElement == "Jacket")
//         //     //                     select entry;
            
//         //     // return results;
//         //     var inputs = _context.Entries.Select(async res => await _context.Entries.ToListAsync())
//         //            .Select(t => t.Result)
//         //            .Where(i => i != null)
//         //            .ToList();

//         // IQueryable<MtoEntry> queryabledata = _context.Entries.AsQueryable<MtoEntry>;

//         // mtoEntry (id, projname, subcostelement etc.)
//         //  kvpair i.e. [id] "PRIMARY" "SECONDARY"
//         // groupBy (mtoEntry)
//         // // sum (true/false)