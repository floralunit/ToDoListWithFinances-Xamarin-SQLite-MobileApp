using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FloralMobileApp.Services
{
    public interface IMessageService
    {
        Task ShowAsync(string message);
    }
}
