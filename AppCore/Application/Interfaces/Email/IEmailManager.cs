using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Application.Interfaces.Email
{
    public interface IEmailManager
    { 
        Task<bool> SendConfirmationEmail(UserModel model);
    }
}
