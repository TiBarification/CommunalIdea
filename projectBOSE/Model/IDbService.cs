using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace projectBOSE.Model
{
    public interface IDbService
    {
        ObservableCollection<string> GetAllNamesSevices();
        //ObservableCollection<mark> GetAllMarks();
        //ObservableCollection<Event_Db> GetAllEvents();
        //int GetTotalErdsInDb();
        //int GetTotalEventsInDb();
        //DateTime? GetFirstDbEventDate();
        //DateTime? GetLastDbEventDate();
        //void RemoveMarks(IEnumerable<mark> marks);
        //void SaveMarksToDb(IEnumerable<mark> marks);
        //void RemovePRS(IEnumerable<erd> erds);
        //void SavePRSToDb(IEnumerable<erd> erds);
        //void SaveEventsToDb(IEnumerable<Event_Db> events);
    }
}
