using DataLibrary.DataAccess;
using DataLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;


namespace DataLibrary.BusinessLogic
{
    public interface IOrganizationProcessor {
        public int CreateOrganization(string name, string description);
        public List<OrganizationModel> LoadOrganizations();
    }
        public class OrganizationProcessor : IOrganizationProcessor
    {
        SqlDataAccess dataAccess;
        public OrganizationProcessor(IConfiguration config) {
            dataAccess = new SqlDataAccess(config.GetConnectionString("ForceDB"));
        }

        public int CreateOrganization(string name, string description)
        {
            OrganizationModel data = new OrganizationModel
            {
                //Id = organizationId,
                Name = name,
                Description = description
            };

            string sql = @"insert into dbo.Organization (Name, Description)
                           values (@Name, @Description);
                            SELECT SCOPE_IDENTITY()";

            return dataAccess.SaveData(sql, data);
        }

        public List<OrganizationModel> LoadOrganizations() 
        {
            string sql = @"select Id, Name, Description
                        from dbo.Organization";
            return dataAccess.LoadData<OrganizationModel>(sql);
        }
    }
}
