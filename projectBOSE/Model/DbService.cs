using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace projectBOSE.Model
{
    public class DbService : IDbService
    {
        public ObservableCollection<string> GetAllNamesSevices()
        {
            using (communalEntities cE = new communalEntities())
            {   
                ObservableCollection<services> tempObs = new ObservableCollection<services>(cE.services);
                 return new ObservableCollection<string>( tempObs.Select(x=>x.name).ToList());
            }
        }
    }
}
