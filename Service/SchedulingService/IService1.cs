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
        //user
        [OperationContract]
        User AddUser(User user);

        [OperationContract]
        List<User> SelectUser();

        [OperationContract]
        void UpdateUser(User user);

        [OperationContract]
        void DeleteUser(int id);

        [OperationContract]
        User FindByIDUser(int id);

        //order
        [OperationContract]
        List<OrderTable> OrderTable();

        [OperationContract]
        void DeleteOrder(int id);

        [OperationContract]
        Order AddOrder(Order order);

        [OperationContract]
        void UpdateOrder(Order order);

        [OperationContract]
        Order FindByIDOrder(int id);

        //room 
        [OperationContract]
        List<Room> SelectRoom();

        [OperationContract]
        Room AddRoom(Room room);

        [OperationContract]
        void UpdateRoom(Room room);

        [OperationContract]
        void DeleteRoom(int id);

        [OperationContract]
        Room FindByIDRoom(int id);

        //couple 
        [OperationContract]
        List<Couple> SelectCouple();

        [OperationContract]
        void AddCouple(Couple couple);

        [OperationContract]
        void UpdateCouple(Couple couple);

        [OperationContract]
        void DeleteCouple(int id);

        [OperationContract]
        Couple FindByIDCouple(int id);

        //group
        [OperationContract]
        List<Group> SelectGroup();

        [OperationContract]
        Group AddGroup(Group group);

        [OperationContract]
        void UpdateGroup(Group group);

        [OperationContract]
        void DeleteGroup(int id);

        [OperationContract]
        Group FindByIDGroup(int id);

        //shedule
        [OperationContract]
        List<SheduleTable> SheduleTable();              

        [OperationContract]
        Shedule AddShedule(Shedule shedule);

        [OperationContract]
        void UpdateShedule(Shedule shedule);

        [OperationContract]
        void DeleteShedule(int id);

        [OperationContract]
        Shedule FindByIDShedule(int id);

        //subject
        [OperationContract]
        List<Subject> SelectSubject();

        [OperationContract]
        Subject AddSubject(Subject subject);

        [OperationContract]
        void UpdateSubject(Subject subject);

        [OperationContract]
        void DeleteSubject(int id);

        [OperationContract]
        Subject FindByIDSubject(int id);

    }


    
}
