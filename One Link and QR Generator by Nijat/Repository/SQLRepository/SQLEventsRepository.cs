using One_Link_and_QR_Generator_by_Nijat.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using One_Link_and_QR_Generator_by_Nijat.Models;
using One_Link_and_QR_Generator_by_Nijat.Utilities;

namespace One_Link_and_QR_Generator_by_Nijat.Repository.SQLRepository
{
    public class SQLEventsRepository : IEvents
    {
        private readonly AppDbContext _context;
        public SQLEventsRepository(AppDbContext _context)
        {
            this._context = _context;
        }

        public URLS createNewUrl()
        {
            URLS url = new URLS()
            {
                Code = Methods.RandomString(6),
            };

            _context.uRLs.Add(url);
            _context.SaveChanges();

            return url;
        }

        public URLS getUrls(string code)
        {
            return _context.uRLs.FirstOrDefault(u => u.Code == code);
        }

        public bool updateUrls(string Code, string Appstore, string GooglePlay, string Web)
        {
            try
            {
                URLS uRLS = _context.uRLs.FirstOrDefault(u => u.Code == Code);

                uRLS.AppStore = Appstore;
                uRLS.GooglePlay = GooglePlay;
                uRLS.Web = Web;

                _context.uRLs.Update(uRLS);
                _context.SaveChanges();
                
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }
    }
}
