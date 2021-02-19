using One_Link_and_QR_Generator_by_Nijat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace One_Link_and_QR_Generator_by_Nijat.Repository.IRepository
{
   public interface IEvents
   {
        URLS createNewUrl();
        URLS getUrls(string code);
        bool updateUrls(string Code,string Appstore, string GooglePlay, string Web);
    }
}
