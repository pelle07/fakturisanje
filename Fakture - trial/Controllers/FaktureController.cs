using Fakture___trial.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Fakture___trial.Controllers
{
    public class FaktureController : Controller
    {
        string connStr = @"Data Source=desktop-5esovj9\sqlexpress;Initial Catalog=Faktura - test;Integrated Security=True";
        // GET: Fakture
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "Select * from Faktura;";
                SqlDataAdapter da = new SqlDataAdapter(query,conn);
                da.Fill(dt);
            }
                return View(dt);
        }

        // GET: Fakture/Create
        public ActionResult Create()
        {
            
            return View(new FaktureModel());
        }

        // POST: Fakture/Create
        [HttpPost]
        public ActionResult Create(FaktureModel faktura)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "Insert Into Faktura Values (@number,@date_created,@total);";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@number", faktura.number);
                    cmd.Parameters.AddWithValue("@date_created", faktura.date_created);
                    cmd.Parameters.AddWithValue("@total", faktura.total);
                    cmd.ExecuteNonQuery();
                }
                    // TODO: Add insert logic here

                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Fakture/Edit/5
        public ActionResult Edit(int id)
        {
            FaktureModel faktura = new FaktureModel();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "Select * from Faktura where id=@id;";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                da.Fill(dt);
            }
            if(dt.Rows.Count == 1)
            {
                faktura.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                faktura.number = Convert.ToInt32(dt.Rows[0][1].ToString());
                faktura.date_created = Convert.ToDateTime(dt.Rows[0][2].ToString());
                faktura.total = Convert.ToDouble(dt.Rows[0][0].ToString());
                return View("Create",faktura);
            }
            else { }

            return RedirectToAction("Index");

        }

        // POST: Fakture/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FaktureModel faktura)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "Update Faktura set number=@number,date_created=@date_created,total=@total where id=@id;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@number", faktura.number);
                    cmd.Parameters.AddWithValue("@date_created", faktura.date_created);
                    cmd.Parameters.AddWithValue("@total", faktura.total);
                    cmd.ExecuteNonQuery();
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Fakture/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "Delete from Faktura where id=@id;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

       
    }
}
