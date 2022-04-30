using NetTrack.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetTrack.Services
{
    public  interface IUserStore<T>
    {

        Task<bool> AddUser(T item);
        User GetUser(bool forceRefresh = false);
    }
}
