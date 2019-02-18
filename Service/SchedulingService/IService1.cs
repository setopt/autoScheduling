using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SchedulingService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void AddUser(User user);

        [OperationContract]
        List<User> SelectUser();

        [OperationContract]
        void UpdateUser(User user);

        [OperationContract]
        void DeleteUser(int id);
        
    }


    
}
