using PhotoShare.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Services.Contracts
{
   public interface ITownService
    {
        Town GetById(int id);

        Town GetByName(string townName);

        Town Create(string[] arguments);

    }
}
