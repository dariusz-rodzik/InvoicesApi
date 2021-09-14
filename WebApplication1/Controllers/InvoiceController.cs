using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public InvoiceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select buyer, supplier, invoice, invoice_date, invoice_due, currency, netto, brutto  from invoices";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Invoice inv)
        {
            string query = @"
                insert into invoices 
                (buyer, supplier, invoice, invoice_date, invoice_due, currency, netto, brutto) 
                values 
                (@buyer, @supplier, @invoice, @invoice_date, @invoice_due, @currency, @netto, @brutto)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@buyer", inv.buyer);
                    myCommand.Parameters.AddWithValue("@supplier", inv.supplier);
                    myCommand.Parameters.AddWithValue("@invoice", inv.invoice);
                    myCommand.Parameters.AddWithValue("@invoice_date", inv.invoice_date);
                    myCommand.Parameters.AddWithValue("@invoice_due", inv.invoice_due);
                    myCommand.Parameters.AddWithValue("@currency", inv.currency);
                    myCommand.Parameters.AddWithValue("@netto", inv.netto);
                    myCommand.Parameters.AddWithValue("@brutto", inv.brutto);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Succesfully");
        }
    }
}
