using DataLibrary.DataAccess;
using DataLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.BusinessLogic
{
    public interface IUserProcessor
    {
        int CreateUser(int organizationId, string firstName, string lastName, string emailAddress, string phoneNumber);
        List<UserModel> LoadUsers(int? organizationId);
        public bool DeleteUser(int userId);
        public bool UpdateUser(int userId, int organizationId, string firstName, string lastName, string emailAddress, string phoneNumber);
    }

    public class UserProcessor : IUserProcessor
    {
        SqlDataAccess dataAccess;
        public UserProcessor(IConfiguration config)
        {
            dataAccess = new SqlDataAccess(config.GetConnectionString("ForceDB"));
        }
        
        public int CreateUser(int organizationId, string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            UserModel data = new UserModel
            {
                OrganizationId = organizationId,
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber
            };

            string sql = @"insert into dbo.[User] (OrganizationId, FirstName, LastName, EmailAddress, PhoneNumber)
                           values (@OrganizationId, @FirstName, @LastName, @EmailAddress, @PhoneNumber);
                            SELECT SCOPE_IDENTITY()";

            return dataAccess.SaveData(sql, data);
        }

        public List<UserModel> LoadUsers(int? organizationId)
        {
            string sql = @"select Id, OrganizationId, FirstName, LastName, EmailAddress, PhoneNumber
                        from dbo.[User]" 
                        + (organizationId == null ? "" : (" where OrganizationID = " + organizationId));

            return dataAccess.LoadData<UserModel>(sql);
        }

        public bool DeleteUser(int userId)
        {
            UserModel data = new UserModel
            {
                Id = userId
            };
            string sql = @"Delete from dbo.[User] where Id = @Id";

            return dataAccess.SaveData<UserModel>(sql, data) == 1;
        }

        public bool UpdateUser(int userId, int organizationId, string firstName, string lastName, string emailAddress, string phoneNumber) {
            UserModel data = new UserModel
            {
                Id = userId,
                OrganizationId = organizationId,
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber
            };

            string sql = @"Update dbo.[User]
                           Set OrganizationId = @OrganizationId,
                            FirstName = @FirstName,
                            LastName = @LastName,
                            EmailAddress = @EmailAddress,
                            PhoneNumber = @PhoneNumber
                            where Id = @Id";

            return dataAccess.SaveData(sql, data) == 1;
        }
    }
}
