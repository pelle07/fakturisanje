using Fakture___trial.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fakture___trial.Controllers
{
    public class StavkaController : Controller
    {
        string connStr = @"Data Source=desktop-5esovj9\sqlexpress;Initial Catalog=Faktura - test;Integrated Security=True";

        // GET: Stavka
        public ActionResult Index(int id)
        {
            double total=0;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "Select * from Stavka where faktura_id=@id;";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                da.Fill(dt);
                string query2 = "Select total_cost from Stavka where faktura_id=@id;";
                SqlDataAdapter da2 = new SqlDataAdapter(query2, conn);
                da2.SelectCommand.Parameters.AddWithValue("@id", id);
                da2.Fill(dt2);
                if(dt2.Rows.Count != 0){
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        total = total + Convert.ToDouble(dt2.Rows[i][0]); 
                    }
                    string query3 = "Update Faktura set total=@total where id=@id";
                    SqlCommand da3 = new SqlCommand(query3, conn);
                    da3.Parameters.AddWithValue("@id", id);
                    da3.Parameters.AddWithValue("@total",total);
                    da3.ExecuteNonQuery();
                }
                

            }
            return View("_Stavka",dt);
        }


        // GET: Stavka/Create
        public ActionResult Create(int id)
        {
            StavkaModel stavka = new StavkaModel();
            stavka.faktura_id = id;
            return View(stavka);
        }

        // POST: Stavka/Create
        [HttpPost]
        public ActionResult Create(StavkaModel stavka,int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = "INSERT INTO Stavka VALUES ( @faktura_id , @name , @unit_price , @quantity );";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@faktura_id", stavka.faktura_id);
                        cmd.Parameters.AddWithValue("@name", stavka.name);
                        cmd.Parameters.AddWithValue("@unit_price", stavka.unit_price);
                        cmd.Parameters.AddWithValue("@quantity", stavka.quantity);
                        cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index", "Stavka", stavka);
            }

            catch
            {
                return View();
            }
                
           
        }

        // GET: Stavka/Edit/5
        public ActionResult Edit(int id)
        {
            StavkaModel stavka = new StavkaModel();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "Select * from Stavka where id=@id;";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                da.Fill(dt);
            }
            if (dt.Rows.Count == 1)
            {
                stavka.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                stavka.faktura_id = Convert.ToInt32(dt.Rows[0][1].ToString());
                stavka.name = dt.Rows[0][2].ToString();
                stavka.unit_price = Convert.ToDouble(dt.Rows[0][3]);
                stavka.quantity = Convert.ToInt32(dt.Rows[0][4].ToString());
                return View("Create", stavka);
            }
            else { }

            return RedirectToAction("Index", "Stavka", stavka);

        }

        // POST: Stavka/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, StavkaModel stavka)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "Update Stavka set name=@name,unit_price=unit_price,quantity=@quantity where id=@id;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", stavka.name);
                    cmd.Parameters.AddWithValue("@unit_price", stavka.unit_price);
                    cmd.Parameters.AddWithValue("@quantity", stavka.quantity);
                    cmd.ExecuteNonQuery();
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index", "Stavka", stavka);
            }
            catch
            {
                return View();
            }
        }

        // GET: Stavka/Delete/5
        public ActionResult Delete(int id)
        {
            string url = this.Request.UrlReferrer.AbsolutePath;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "Delete from Stavka where id=@id;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                return Redirect(url);
            }
            catch
            {
                return View();
            }

            

        }

        
    }
}
