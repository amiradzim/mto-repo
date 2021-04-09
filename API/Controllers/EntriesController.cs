using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Models.Request.Entries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class EntriesController : BaseApiController
    {
        private readonly DataContext _context;
        readonly IEntryRepo _entryRepo;

        public EntriesController(DataContext context, IEntryRepo entryRepo)
        {
            _context = context;
            _entryRepo = entryRepo;
        }

        //[HttpPost("upload"), DisableRequestSizeLimit]
        //public async Task<IActionResult> Upload()
        //{
        //    try
        //    {
        //        var formCollection = await Request.ReadFormAsync();
        //        var file = formCollection.Files.First();
        //        var folderName = Path.Combine("Resources", "Uploads");   
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //        if (file.Length > 0)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);
        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }

        //            string excelConnectionString = string.Format("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties = Excel 8.0", fullPath);
        //            OleDbConnection connection = new OleDbConnection();
        //            connection.ConnectionString = excelConnectionString;
        //            OleDbCommand command = new OleDbCommand("select * from[Sheet1$]", connection);
        //            connection.Open();
        //            DbDataReader dr = command.ExecuteReader();
        //            string sqlConnectionString = @"Data source=mtorepository.db";
        //            SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);
        //            bulkInsert.DestinationTableName = "Entries";
        //            bulkInsert.WriteToServer(dr);

        //            connection.Close();


        //            return Ok(new { dbPath });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }
        //}

        [HttpPost("insert")]
        public async Task<ActionResult<bool>> insert(InsertEntriesRequest insertEntriesRequest)
        {
            try
            {
                return await _entryRepo.Insert(insertEntriesRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<EntryDto>>> GetEntries()
        {
            return await _context.Entries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EntryDto>> GetEntry(int id)
        {
            return await _context.Entries.FindAsync(id);
        }

        [HttpGet("projname-list")]
        public ActionResult<List<string>> GetProjName()
        {
            return _entryRepo.GetProjName();
        }

        [HttpGet("platname-list")]
        public ActionResult<List<string>> GetPlatName()
        {
            return _entryRepo.GetPlatName();
        }

        [HttpGet("structtype-list")]
        public ActionResult<List<string>> GetStructType()
        {
            return _entryRepo.GetStructType();
        }

        [HttpGet("structarea-list")]
        public ActionResult<List<string>> GetStructArea()
        {
            return _entryRepo.GetStructArea();
        }

        [HttpGet("platarea-list")]
        public ActionResult<List<string>> GetPlatArea()
        {
            return _entryRepo.GetPlatArea();
        }

        [HttpGet("subrea-list")]
        public ActionResult<List<string>> GetSubArea()
        {
            return _entryRepo.GetSubArea();
        }

        [HttpGet("mattype-list")]
        public ActionResult<List<string>> GetMatType()
        {
            return _entryRepo.GetMatType();
        }

        [HttpGet("matvariant-list")]
        public ActionResult<List<string>> GetMatVariant()
        {
            return _entryRepo.GetMatVariant();
        }

        [HttpGet("procmethod-list")]
        public ActionResult<List<string>> GetProcMethod()
        {
            return _entryRepo.GetProcMethod();
        }

        [HttpGet("matgroup-list")]
        public ActionResult<List<string>> GetMatGroup()
        {
            return _entryRepo.GetMatGroup();
        }

        [HttpPost("queryBuilder")]
        public ActionResult<List<dynamic>> QueryBuilder(EntriesQueryBuilderRequest entriesQueryBuilderRequest)
        {
            return _entryRepo.QueryBuilder(entriesQueryBuilderRequest);
        }

        // [HttpGet("filter-select")]
        // public async Task<List<MtoEntry>> FilterSelect()
        // {
        //     IQueryable<MtoEntry> qry = from entry in _context.Entries
        //                             where entry.SubCostElement == "JACKET"
        //                             select new MtoEntry
        //                             {
        //                                 Id = entry.Id,
        //                                 CostElement = entry.CostElement,
        //                                 SubCostElement = entry.SubCostElement,
        //                                 Diameter = entry.Diameter
        //                             };

        //     return await qry.ToListAsync();
        // }
    }
}