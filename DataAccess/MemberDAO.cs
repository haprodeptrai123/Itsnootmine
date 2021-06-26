using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class MemberDAO
    {

        //Initialize member list 
        private static List<MemberObject> MemberList = new List<MemberObject>()
        {
            new MemberObject{MemberID=1, MemberName="Jack", Email="thuanldse151098@fpt.edu.vn",City="Ho Chi Minh", Country="Viet Nam", Password="12345678" },
            new MemberObject{MemberID=2, MemberName="John", Email="Johnny123@gmail.com",City="Ho Chi Minh", Country="United State", Password="123456789" },
            new MemberObject{MemberID=3, MemberName="Zack", Email="Zaky123@gmail.com",City="Ho Chi Minh", Country="Viet Nam", Password="12345678911" },
            new MemberObject{MemberID=4, MemberName="Joey", Email="Joey123@gmail.com",City="Ho Chi Minh", Country="United State", Password="12345678978" },

        };
        //--------------------------------------------------------------
        //Using Singleton Pattern
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        //----------------------------------------------------------------
        public List<MemberObject> GetMemberList => MemberList;
        //----------------------------------------------------------------
        public MemberObject GetMemberByID(int memberID)
        {
            //using LINQ to Object
            MemberObject member = MemberList.SingleOrDefault(pro => pro.MemberID == memberID);
            return member;
        }
       

        public MemberObject GetMemberByName(string memberName)
        {
            //using LINQ to Object
            MemberObject member = MemberList.SingleOrDefault(pro => pro.MemberName == memberName);
            return member;
        }
        //-----------------------------------------------------------------
        //Add a new member
        public void AddNew(MemberObject member)
        {
            MemberObject pro = GetMemberByID(member.MemberID);
            if (pro == null)
            {
                MemberList.Add(member);
            }
            else
            {
                throw new Exception("Member already exists");
            }
        }
        //Update a member
        public void Update(MemberObject member)
        {
            MemberObject c = GetMemberByID(member.MemberID);
            if (c != null)
            {
                var index = MemberList.IndexOf(c);
                MemberList[index] = member;
            }
            else
            {
                throw new Exception("Member does not exists.");
            }
        }
        //------------------------------------------------------------------
        //Remove a member
        public void Remove(int MemberID)
        {
            MemberObject p = GetMemberByID(MemberID);
            if (p != null)
            {
                MemberList.Remove(p);
            }
            else
            {
                throw new Exception("Member does not exists.");
            }
        }
        public List<MemberObject> GetMemberByCityAndCountry(string city, string country)
        {
            List<MemberObject> FList = new List<MemberObject>();
            for (int i = 1; i <= MemberList.Count; i++)
            {
                if (MemberList[i - 1].City == city && MemberList[i - 1].Country == country) { FList.Add(MemberList[i - 1]); }
            }
            return FList;
        }
    }
}
