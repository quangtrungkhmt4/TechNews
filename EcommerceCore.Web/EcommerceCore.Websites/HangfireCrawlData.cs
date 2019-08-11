using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EcommerceCore.Domain;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("HangfireStartup", typeof(EcommerceCore.Websites.HangfireCrawlData))]

namespace EcommerceCore.Websites
{
    public class HangfireCrawlData
    {
       
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(System.Configuration.ConfigurationManager
                .ConnectionStrings["EcommerceDbContext"].ConnectionString);

            //BackgroundJob.Enqueue(() => FireAndForgot());

            RecurringJob.AddOrUpdate(() => CrawlData(), Cron.Minutely);

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }

        public void CrawlData()
        {
            System.Diagnostics.Debug.WriteLine("RecurringJob: " + System.DateTime.Now);
            //String filepath = "D:\\test.txt";// đường dẫn của file muốn tạo
            //FileStream fs = new FileStream(filepath, FileMode.Create);//Tạo file mới tên là test.txt            
            //StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);//fs là 1 FileStream 

            //string output = "";
            //for(int i=0; i< GetAllThematics().Count; i++)
            //{
            //    output += GetAllThematics()[i] + "\n";
            //}

            //sWriter.WriteLine(output);
            //sWriter.WriteLine("\n");
            //// Ghi và đóng file
            //sWriter.Flush();
            //fs.Close();
        }

        private List<string> GetAllThematics()
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = System.Configuration.ConfigurationManager
                .ConnectionStrings["EcommerceDbContext"].ConnectionString;
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            string query = "select * from Thematics";
            SqlCommand sqlCommand = new SqlCommand(query, cnn);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            List<string> thematics = new List<string>();
            while (dataReader.Read())
            {
                thematics.Add(dataReader.GetValue(1)+"");
            }
            return thematics;
        }
    }
}
